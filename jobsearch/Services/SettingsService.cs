using System.Text;

namespace JobSearch.Services;

internal static class Settings
{
    private static readonly string SecretKey = "6ceccd7405ef4b00b2630009be568cfa";
    
    internal static byte[] GenerateSecretByte() => 
        Encoding.ASCII.GetBytes(SecretKey);
}