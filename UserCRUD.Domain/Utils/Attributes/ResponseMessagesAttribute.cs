namespace UserCRUD.Application.Common.Utils.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public class ResponseMessagesAttribute : Attribute
{
    public Dictionary<string, string> Messages { get; set; } = new();

    public ResponseMessagesAttribute(params string[] messages)
    {
        foreach (var messageFormat in messages)
        {
            var keyValuePair = messageFormat.Split(":");
            Messages.Add(keyValuePair[0], keyValuePair[1]);
        }
    }
}