using Microsoft.Extensions.Options;

namespace SmartHome.Data.Infrastructure.MongoDB.Validators
{
    using Configuration;
    using SmartHome.Common.Extentions;

    public class MongoDbConfigurationValidation : IValidateOptions<MongoDbConfiguration>
    {
        public ValidateOptionsResult Validate(string name, MongoDbConfiguration options)
        {
            if (options.ConnectionString.IsNullOrEmptyOrWhiteSpace())
            {
                return ValidateOptionsResult.Fail("The ConnectionString must be defined.");
            }

            if (options.Database.IsNullOrEmptyOrWhiteSpace())
            {
                return ValidateOptionsResult.Fail("The Database must be defined.");
            }

            return ValidateOptionsResult.Success;
        }
    }
}
