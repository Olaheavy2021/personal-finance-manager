namespace PersonalFinanceManager.Application.Constants;

public static class ApiEndpoints
{
    public static class Auth
    {
        public const string Base = "/auth/";

        public const string Register = $"{Base}register";

        public const string Login = $"{Base}login";

        public const string RefreshToken = $"{Base}token/refresh";

        public const string RevokeToken = $"{Base}token/revoke";
    }
}
