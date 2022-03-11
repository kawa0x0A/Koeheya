using CoreTweet;

namespace Koeheya
{
    public static class Twitter
    {
        public const string ConsumerKeyName = "Authentication:Twitter:ConsumerAPIKey";
        public const string ConsumerKeySecretName = "Authentication:Twitter:ConsumerSecret";
        public const string AccessTokenName = "Authentication:Twitter:AccessToken";
        public const string AccessTokenSecretName = "Authentication:Twitter:AccessTokenSecret";

        public static readonly Tokens Token;

        static Twitter()
        {
            string consumerKey = "";
            string consumerKeySecret = "";
            string accessToken = "";
            string accessTokenSecret = "";

            switch (Data.ApplicationDbContext.ConnectionAddressString)
            {
                case Data.ApplicationDbContext.ConnectDatabaseLocal:
                    var builder = WebApplication.CreateBuilder();

                    consumerKey = builder.Configuration[ConsumerKeyName];
                    consumerKeySecret = builder.Configuration[ConsumerKeySecretName];
                    accessToken = builder.Configuration[AccessTokenName];
                    accessTokenSecret = builder.Configuration[AccessTokenSecretName];
                    break;

                case Data.ApplicationDbContext.ConnectDatabaseStaging:
                case Data.ApplicationDbContext.ConnectDatabaseProcution:
                    consumerKey = Environment.GetEnvironmentVariable(ConsumerKeyName)!;
                    consumerKeySecret = Environment.GetEnvironmentVariable(ConsumerKeySecretName)!;
                    accessToken = Environment.GetEnvironmentVariable(AccessTokenName)!;
                    accessTokenSecret = Environment.GetEnvironmentVariable(AccessTokenSecretName)!;
                    break;
            }
            
            Token = Tokens.Create(consumerKey, consumerKeySecret, accessToken, accessTokenSecret);
        }

        public static async Task<string> GetProfileImageUrlAsync(long userId)
        {
            var userResponse = await Token.Users.ShowAsync(userId);

            return userResponse.ProfileImageUrl;
        }
    }
}
