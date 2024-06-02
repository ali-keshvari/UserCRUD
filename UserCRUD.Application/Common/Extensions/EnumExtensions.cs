using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using UserCRUD.Application.Common.Utils.Attributes;

namespace UserCRUD.Application.Common.Extensions;

public static class EnumExtensions
{
    public static string ToEnumFarsi(this object? obj)
    {
        if (obj == null)
        {
            return "";
        }

        try
        {
            var enumType = obj.GetType();
            var fi = enumType.GetField(obj.ToString()!);
            if (fi == null)
            {
                return "";
            }

            var attributeDescription = fi.GetCustomAttributes(typeof(DescriptionAttribute), true).FirstOrDefault();
            var attributeDisplay = fi.GetCustomAttributes(typeof(DisplayAttribute), true).FirstOrDefault();

            if (attributeDescription != null)
            {
                return ((DescriptionAttribute)attributeDescription).Description;
            }

            if (attributeDisplay != null)
            {
                return ((DisplayAttribute)attributeDisplay).Name!;
            }

            return fi.Name;
        }
        catch (Exception)
        {
            return "";
        }
    }

    public static Dictionary<string, string>? ToResponseMessages(this object? obj)
    {
        if (obj == null) return null;

        try
        {
            var enumType = obj.GetType();

            var fi = enumType.GetField(obj.ToString()!);

            if (fi == null) return null;
                
            var responseMessagesAttribute = fi
                .GetCustomAttributes(typeof(ResponseMessagesAttribute), true)
                .FirstOrDefault();

            if (responseMessagesAttribute == null) return null;

            return ((ResponseMessagesAttribute)responseMessagesAttribute).Messages;
        }
        catch
        {
            return null;
        }
    }
}