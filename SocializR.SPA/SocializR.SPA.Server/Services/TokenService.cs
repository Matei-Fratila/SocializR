namespace SocializR.SPA.Server.Services;
public class TokenService(IOptionsMonitor<JwtSettings> _jwtSettings)
{
    public string GenerateToken(User user, IList<string> roles)
    {
        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.CurrentValue.SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var token = new JwtSecurityToken(
            _jwtSettings.CurrentValue.Issuer,
            _jwtSettings.CurrentValue.Audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_jwtSettings.CurrentValue.DurationInMinutes)),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
