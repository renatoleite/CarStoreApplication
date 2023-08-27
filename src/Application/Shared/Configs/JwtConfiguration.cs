using System.Diagnostics.CodeAnalysis;

namespace Application.Shared.Configs
{
    [ExcludeFromCodeCoverage]
    public class JwtConfiguration
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
    }
}
