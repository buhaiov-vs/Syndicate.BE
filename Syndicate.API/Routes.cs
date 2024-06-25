using Microsoft.EntityFrameworkCore.Storage;

namespace Syndicate.API;

public static class Routes
{
    public static class Identity
    {
        public static string Base => "identity";

        public static string Signin => $"{Base}/signin";

        public static string Signup => $"{Base}/signup";
    }

    public static class Services
    {
        public static string Base => "services";

        public static string Exact(string paramName) => Base + "/{" + paramName + "}";

        public static string Draft => Base + "/draft";

        public static string Publish(string paramName) => Exact(paramName) + "/publish";

        public static string Deactivate(string paramName) => Exact(paramName) + "/deactivate";
    }

    public static class Categories
    {
        public static string Base => "categories";
    }
}