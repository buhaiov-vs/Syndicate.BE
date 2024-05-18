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

        public static string Single(string paramName) => Base + "/" + paramName;

        public static string Draft => Base + "/draft";
    }

    public static class Categories
    {
        public static string Base => "categories";
    }
}