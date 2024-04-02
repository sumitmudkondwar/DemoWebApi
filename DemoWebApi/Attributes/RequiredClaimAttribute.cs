namespace DemoWebApi.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class RequiredClaimAttribute(string claimType, string claimValue) : Attribute
    {
        public string ClaimType { get; } = claimType;
        public string ClaimValue { get; } = claimValue;
    }
}
