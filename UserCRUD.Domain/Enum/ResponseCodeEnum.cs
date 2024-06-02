using UserCRUD.Application.Common.Utils.Attributes;

namespace UserCRUD.Domain.Enum;

public enum ResponseCodeEnum
{
    [ResponseMessages("fa:عملیات با موفقیت انجام شد")]
    Ok = 0,
    [ResponseMessages("fa:شما به این بخش دسترسی ندارید")]
    AccessDenied = 1,
    [ResponseMessages("fa:خطای ناشناخته ای رخ داده است")]
    Unknown = 1000,

    [ResponseMessages("fa:هیچ موردی یافت نشد")]
    RecordNotFound = 10,
    [ResponseMessages("fa:خطایی در ذخیره اطلاعات به وجود آمده است")]
    CreateFailed = 11,
    [ResponseMessages("fa:خطایی در ویرایش اطلاعات به وجود آمده است")]
    UpdateFailed = 12,
    [ResponseMessages("fa:خطایی در حذف رکورد به وجود آمده است")]
    DeleteFailed = 13,
    
    [ResponseMessages("fa:درخواست شما رد شد")]
    YourRequestIsNotAcceptable = 14,

    [ResponseMessages("fa:کد ملی وارد شده معتبر نمیباشد")]
    UnValidNationalCode = 1001,

    [ResponseMessages("fa:کاربری با این کد ملی از قبل در سامانه وارد شده است")]
    DuplicatedUserWithNationalCode = 1002,
    [ResponseMessages("fa:کاربری با این کد پرسنلی از قبل در سامانه وارد شده است")]
    DuplicatedUserWithPersonalCode = 1003,
    [ResponseMessages("fa:کاربری با این کد ملی و کد پرسنلی از قبل در سامانه وارد شده است")]
    DuplicatedUserWithNationalCodeAndPersonalCode = 1004,
    [ResponseMessages("fa:فایل های وارد شده معتبر نمی باشند حجم هر فایل می بایست کمتر از 500 کیلو بایت باشد و پسوندهای قابل قبول .txt و .pdf می باشد")]
    UnValidFiles = 1005,
    [ResponseMessages("fa:تمام فایل ها به درستی ذخیره نشده است لطفا فایل را بررسی نماییید")]
    AllFileNotUploaded = 1006,


}
