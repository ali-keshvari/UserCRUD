namespace UserCRUD.Application.Common.Constants;

public static class ValidationMessages
{
    public const string NotNull = "این فیلد الزامی می باشد";
    public static string GetNotNull(string name)
    {
        return $"فیلد {name} الزامی می باشد";
    }

    public const string NotEmpty = "این فیلد نمی تواند خالی باشد";
    public static string GetNotEmpty(string name)
    {
        return $"فیلد {name} نمی تواند خالی باشد";
    }

    public const string Pattern = "فرمت ورودی معتبر نمی باشد";

    public static string GetMaxLength(string name, int length)
    {
        return $"{name} نمی تواند بیشتر از {length} کاراکتر باشد";
    }

    public static string GetMinLength(string name, int length)
    {
        return $"{name} نمی تواند کمتر از {length} کاراکتر باشد";
    }

    public static string GetEqual(string main, string input)
    {
        return $"{input} با {main} مغایرت دارد";
    }
}
