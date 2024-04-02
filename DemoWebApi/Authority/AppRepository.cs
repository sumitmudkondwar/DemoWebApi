namespace DemoWebApi.Authority
{
    public static class AppRepository
    {
        private static List<Application> _applications = new List<Application>()
        {
            new Application()
            {
                ApplicationId = 1,
                ApplicationName = "MVCWebApp",
                ClientId = "61333E0C-9E2F-4120-9EF6-DCF12706D37B",
                Secret = "1AB98808-BCE2-42CC-9C58-428AE821DB21",
                Scopes = "read,write"
            }
        };

        public static bool Authenticate(string clientId, string secret)
        { 
            return _applications.Any(x => x.ClientId == clientId && x.Secret == secret);
        }

        public static Application? GetApplicationByClientId(string clientId)
        {
            return _applications.FirstOrDefault(x => x.ClientId == clientId);
        }
    }
}
