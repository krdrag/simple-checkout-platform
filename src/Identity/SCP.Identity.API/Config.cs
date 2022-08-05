using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using SCP.Common.Constants;
using System.Security.Claims;

namespace Identity.API
{
    public static class Config
    {
        public static IEnumerable<Client> Clients =>
            new[]
            {
                new Client
                {
                    ClientId = "SCP POS",
                    ClientName = "Simple Checkout Platform POS Application",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = {new Secret(EnvironmentVariables.POSClientPasswordEnvVar.Sha256())},
                    AllowedScopes = {"SCP.read"}
                },
                new Client
                {
                    ClientId = "Postman",
                    ClientName = "Postman client for testing",
                    AllowOfflineAccess = true,
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets = {new Secret(EnvironmentVariables.PostmanClientPasswordEnvVar.Sha256())},
                    AllowedScopes = {"openid", "profile", "SCP.read", "SCP.write" }
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new[]
            {
                new ApiScope("SCP.read"),
                new ApiScope("SCP.write"),
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource
                {
                    Name = "role",
                    UserClaims = new List<string> {"role"}
                }
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new[]
            {
                new ApiResource("SCP.Transaction")
                {
                    Scopes = new List<string> {"SCP.read", "SCP.write"},
                    ApiSecrets = new List<Secret> {new Secret(EnvironmentVariables.TransactionResourceEnvVar.Sha256())},
                    UserClaims = new List<string> {"role"}
                },
                new ApiResource("SCP.Session")
                {
                    Scopes = new List<string> {"SCP.read", "SCP.write"},
                    ApiSecrets = new List<Secret> {new Secret(EnvironmentVariables.SessionResourceEnvVar.Sha256())},
                    UserClaims = new List<string> {"role"}
                }
            };

        public static List<TestUser> Users =>
            new()
            {
                new TestUser()
                {
                    SubjectId = "818727",
                    Username = "StoreManager",
                    Password = "1234",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.GivenName, "John"),
                        new Claim(JwtClaimTypes.FamilyName, "Doe"),
                        new Claim(JwtClaimTypes.Role, "store_manager"),
                    }
                },
                new TestUser()
                {
                    SubjectId = "88421113",
                    Username = "Cashier",
                    Password = "1234",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.GivenName, "Jerry"),
                        new Claim(JwtClaimTypes.FamilyName, "Doe"),
                        new Claim(JwtClaimTypes.Role, "cashier"),
                    }
                }
            };
    }
}
