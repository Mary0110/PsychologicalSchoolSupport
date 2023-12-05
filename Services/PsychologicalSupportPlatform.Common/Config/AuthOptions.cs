using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace PsychologicalSupportPlatform.Common.Config;

public class AuthOptions
{
    public string Issuer { get; set; } = string.Empty;
    
    public string Audience { get; set; } = string.Empty;
    
    public string Secret { get; set; } = string.Empty;
    
    public int TokenLifeTime { get; set; } //minutes

    public SymmetricSecurityKey GetSymmetricSecurityKey() => 
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret));
}
