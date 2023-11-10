
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Endpoint.MVC.ExtensionMethods;
public static class GetDisplayNameExtension
{
    public static string GetDisplayName(this Enum enumValue)
    {
        return enumValue
                  .GetType()
                  .GetMember(enumValue.ToString())
                  .First()?
                  .GetCustomAttribute<DisplayAttribute>()?
                  .Name;
    }
}
