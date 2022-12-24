namespace SchoolServer.Helpers;

public static class HashPasswordHelper
{
    public static string GetHash(string value)
    {
        var hashedBytes = value.GetHashCode();
        return hashedBytes.GetHashCode().ToString();
    }
}