﻿
namespace Domain.Core.Exceptions;

public static class ExpMessage
{
    public static string InvalidPassword { get; } = "رمز عبور نادرست";
    public static string RegisterdUser { get; } = "قبلا با شماره تلفن همراه {0} ثبت نام انجام شده است";

    public static string NotFoundUser { get; } = "کاربری با {0} {1} پیدا نشد";
    public static string InsuccessRegister { get; } = "ثبت نام ناموفق";
    public static string NotExistRole { get; } = "عدم وجود نقش برای کاربر جاری";
    public static string NotAllowAddProduct { get; } = "قابلیت افزودن محصول به دسته بندی انتخابی ممکن نیست. دسته بندی های پایین تر را انتخاب نمایید";
    public static string NotFoundCategory { get; } = "دسته بندی مدنظر یافت نشد";
    public static string HaveNotCustomAttribute { get; } = "دسته بندی انتخابی مشخصات خاصی ندارد";

    public static string NotChangedProduct { get; } = "محصول {0} نشد";

    public static string InsufficientProduct { get; } = "موجودی ناکافی";
    public static string NotExistRecord { get; } = "{0}محصول پیدا نشد";
    public static string HaveNotProduct { get; } = "محصولی وجود ندارد";
    public static string HaveNotComment { get; } = "نظری ثبت نشده است";
    public static string NotFoundUserId { get; } = "کاربر یافت نشد";
    public static string NotExistCart { get; } = "سبد خرید مدنظر یافت نشد";
    public static string NotExistCarts { get; } = "سبد خریدی برای کاربر یافت نشد";
    public static string NotChanged { get; } = "تغییرات انجام نشد";
    public static string Unauthorized { get; } = "عدم دسترسی به محتوای جاری";
  
    public static string NotFoundBooth { get; } = "غرفه ای پیدا نشد";
    public static string FoundBooth { get; } = "شما قبلا یک غرفه ایجاد کرده اید";
    public static string NotFoundRole { get; } = "چنین نقشی پیدا نشد";
    public static string LowPrice { get; } = "قیمت از حداقل قیمت قابل پیشنهاد پایین تر است";
    public static string WrongCartStatus { get; } = "وضعیت سبد خرید انتخابی قابل تغییر نیست";
    public static string NotFoundWage { get; } = "کارمزدی  پیدا نشد";
    public static string NotHaveOrder { get; } = "سبد خرید خالی است";
    public static string HasNotBuyed { get; } = "شمااین محصول را قبلا خریداری نکرده اید و قادر به ثبت نظر نیستید";
    public static string NoAuctionOrder { get; } = "هیچ پیشنهادی برای این مزایده ثبت نشده است";

}