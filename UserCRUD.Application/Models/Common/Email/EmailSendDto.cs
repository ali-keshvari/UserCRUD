namespace UserCRUD.Application.Models.Common.Email;

public class EmailSendDto
{
    public string? Subject { get; set; }
    public string? Body { get; set; }
    public required string To { get; set; }
    public List<EmailAttachmentDto>? Attachments { get; set; }
}

public class EmailAttachmentDto
{
    public required string Name { get; set; }
    public required Stream StreamContent { get; set; }
    public required string ContentType { get; set; }
}