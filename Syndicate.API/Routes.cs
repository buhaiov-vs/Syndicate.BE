using Microsoft.EntityFrameworkCore.Storage;

namespace Syndicate.API;

public static class Routes
{
    public static class Identity
    {
        public static string Base => "identity";

        public static string SignIn => $"{Base}/signIn";

        public static string Signup => $"{Base}/signup";
    }

    public static class Services
    {
        public static string Base => "services";

        public static string Exact(string paramName) => Base + RouteParam.Get(paramName);

        public static string Draft => Base + "/draft";

        public static string Publish(string paramName) => Exact(paramName) + "/publish";

        public static string Deactivate(string paramName) => Exact(paramName) + "/deactivate";

        public static class Folders
        {
            public static string Base => Services.Base + "/folders";

            public static string Exact(string paramName) => Base + RouteParam.Get(paramName);
        }
    }

    public static class Categories
    {
        public static string Base => "categories";
    }

    public static class RouteParam
    {
        public static string Get(string? paramName)
        {
            return String.IsNullOrWhiteSpace(paramName) ? "" : "/{" + paramName + "}";
        }
    }
}