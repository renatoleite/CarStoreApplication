using System.Diagnostics.CodeAnalysis;

namespace WebApi.Consts
{
    [ExcludeFromCodeCoverage]
    public static class Roles
    {
        public const string Create = "create";
        public const string Read = "read";
        public const string Delete = "delete";
        public const string Update = "update";
    }
}
