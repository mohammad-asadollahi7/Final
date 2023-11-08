using Domain.Core.Contracts.Repos;
using Domain.Core.Contracts.Services;
using Domain.Core.Entities;
using Domain.Core.Enums;
using Domain.Core.Exceptions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Domain.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IAdminService _adminService;
    private readonly ICustomerService _customerService;
    private readonly ISellerService _sellerService;
    private readonly ICustomerRepository _customerRepository;
    private readonly IOptionsSnapshot<JWTConfiguration> _JWTConfigs;

    public AccountService(IAccountRepository accountRepository,
                          IAdminService adminService,
                          ICustomerService customerService,
                          ISellerService sellerService,
                          ICustomerRepository customerRepository,
                          IOptionsSnapshot<JWTConfiguration> JWTConfigs)
    {
        _accountRepository = accountRepository;
        _adminService = adminService;
        _customerService = customerService;
        _sellerService = sellerService;
        _customerRepository = customerRepository;
        _JWTConfigs = JWTConfigs;
    }

    public async Task EnsurePassword(ApplicationUser user,
                                     string password)
    {
        var isCorrect = await _accountRepository.CheckPassword(user, password);
        if (!isCorrect)
            throw new AppException(ExpMessage.InvalidPassword,
                                   ExpStatusCode.Unauthorized);

    }

    public async Task EnsureUniquePhoneNumber(string phoneNumber,
                                              CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetCustomerByPhoneNumber(phoneNumber, cancellationToken);
                                                
        if (customer is not null)
            throw new AppException(string.Format(ExpMessage.RegisterdUser, phoneNumber),
                                   ExpStatusCode.Conflict);
    }

    public async Task<ApplicationUser> GetApplicationUserByEmail(string email)
    {
        var applicationUser = await _accountRepository.GetApplicationUserByEmail(email);

        if (applicationUser is null)
            throw new AppException(string.Format(ExpMessage.NotFoundUserId, "ایمیل", email),
                                   ExpStatusCode.NotFound);

        return applicationUser;
    }

    public async Task<ApplicationUser?> GetApplicationUserByUsername(string username)
    {
        var user = await _accountRepository.GetApplicationUserByUsername(username);
        if (user is null)
            throw new AppException(string.Format(ExpMessage.NotFoundUserId, "نام کاربری", username),
                                   ExpStatusCode.NotFound);

        return user;
    }



    public async Task Register(ApplicationUser user,
                                      string password,
                                      Role role,
                                      CancellationToken cancellationToken)
    {
        var identityResult = await _accountRepository.Register(user, password, role, cancellationToken);
        
        
        if (!identityResult.Succeeded)
            throw new AppException(ExpMessage.InsuccessRegister,
                                   ExpStatusCode.InternalServerError);
    }


    public async Task CreateRoleByUserId(int userId,
                                         Role role,
                                         CancellationToken cancellationToken)
    {
        if (role.ToString().ToLower() == "admin")
            await _adminService.AddByApplicationUserId(userId, cancellationToken);

        else if (role.ToString().ToLower() == "customer")
            await _customerService.AddByApplicationUserId(userId, cancellationToken);

        else if(role.ToString().ToLower() == "seller")
            await _sellerService.AddByApplicationUserId(userId, cancellationToken);

    }


    public async Task<string> GenerateJWTToken(ApplicationUser user,
                                               string role,
                                               CancellationToken cancellationToken)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Role, role),
        };


        if (role.ToLower() == "admin")
        {
            var adminId = await _adminService.GetAdminIdByUserId(user.Id, cancellationToken);
            claims.Add(new Claim(ClaimTypes.NameIdentifier, adminId.ToString()));
        }
        else if (role.ToLower() == "customer")
        {
            var customerId = await _customerService.GetCustomerIdByUserId(user.Id, cancellationToken);
            claims.Add(new Claim(ClaimTypes.NameIdentifier, customerId.ToString()));
        }
        else if (role.ToLower() == "seller")
        {
            var sellerId = await _sellerService.GetSellerIdByUserId(user.Id, cancellationToken);
            claims.Add(new Claim(ClaimTypes.NameIdentifier, sellerId.ToString()));
        }


        var key = new SymmetricSecurityKey(Encoding.UTF8.
                                           GetBytes(_JWTConfigs.Value.IssuerSigningKey));

        var token = new JwtSecurityToken(
                    issuer: _JWTConfigs.Value.Issuer,
                    audience: _JWTConfigs.Value.Audience,
                    claims: claims,
                    expires: DateTime.Now.AddHours(3),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                    );

        var tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);
        return tokenAsString;
    }


    public async Task<List<string>> GetRoleNamesByUser(ApplicationUser user)
    {
        var roleNames = await _accountRepository.GetRoleNamesByUser(user);

        if (roleNames.Count() == 0)
            throw new AppException(ExpMessage.NotExistRole,
                                   ExpStatusCode.NotFound);

        return roleNames.ToList();
    }


    public void EnsureRoleValidity(string currentUserRole,
                                   string databaseRole)
    {
        if (currentUserRole.ToLower() != databaseRole.ToLower())
            throw new AppException(string.Format(ExpMessage.NotFoundUserId, "نقش", currentUserRole),
                                   ExpStatusCode.NotFound);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _accountRepository.SaveChangesAsync(cancellationToken);
    }


    public void EnsureRoleExist(Role role)
    {
        if (role != Role.Customer && role != Role.Admin && role != Role.Seller)
            throw new AppException(ExpMessage.NotFoundRole, ExpStatusCode.NotFound);
    }
}
