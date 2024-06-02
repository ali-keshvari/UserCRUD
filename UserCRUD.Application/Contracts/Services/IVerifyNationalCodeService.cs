namespace UserCRUD.Application.Contracts.Services
{
    public interface IVerifyNationalCodeService
    {
        Task<bool> Verify(string national);
    }
}
