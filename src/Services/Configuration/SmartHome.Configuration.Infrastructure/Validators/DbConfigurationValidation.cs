using Microsoft.Extensions.Options;
using SmartHome.Common.Extentions;
using System;

namespace SmartHome.Configuration.Infrastructure.Validators
{
    public class DbConfigurationValidation : IValidateOptions<DbConfiguration>
    {
        private ValidateOptionsResult _result = ValidateOptionsResult.Success;
        private string _connectionString;

        public ValidateOptionsResult Validate(string name, DbConfiguration options)
        {
            ValidateConnectionString(options.ConnectionString, new Tuple<string, string>[]
            {
                //new("database", "The ConnectionString must be contain a Database."),
                //new("server", "The ConnectionString must be contain a Server."),
                new("=", "The ConnectionString is not valid.")
            });

            return _result;
        }

        private void ValidateConnectionString(string connectionString, params Tuple<string, string>[] tuples)
        {
            if (connectionString.IsNullOrWhiteSpace())
            {
                _result = ValidateOptionsResult.Fail("The ConnectionString must be defined.");
                return;
            }

            _connectionString = connectionString.ToLowerInvariant();

            foreach (var (value, message) in tuples)
            {
                if (_connectionString.Contains(value))
                {
                    continue;
                }

                _result = ValidateOptionsResult.Fail(message);
                return;
            }
        }
    }
}
