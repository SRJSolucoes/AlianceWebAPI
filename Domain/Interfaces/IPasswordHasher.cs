namespace Domain.Interfaces
{
    public interface IPasswordHasher
    {
        byte[] Hash(string password, byte[] salt);
    }
}
