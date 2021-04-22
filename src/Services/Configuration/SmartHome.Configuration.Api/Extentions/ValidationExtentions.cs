using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace SmartHome.Configuration.Api.Extentions
{
    /// <summary>
    /// Determines Validation Extentions.
    /// </summary>
    public static class ValidationExtentions
    {
        /// <summary>
        /// Adds a model validation for the site SmartHome Configuration.
        /// </summary>
        /// <param name="mvcBuilder"></param>
        /// <returns></returns>
        public static IMvcBuilder AddSmartHomeConfigurationValidation(this IMvcBuilder mvcBuilder)
        {
            throw new InvalidOperationException("Add a first Validator before use AddSmartHomeConfigurationValidation.");
            // Uncomment when add Validator.
            //mvcBuilder.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ConfigurationRequestValidator>());

            mvcBuilder.AddJsonOptions(opts =>
            {
                opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                opts.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });

            mvcBuilder.ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var (key, value) =
                        context.ModelState.First(e => e.Value.ValidationState == ModelValidationState.Invalid);

                    var error = value.Errors[0].ErrorMessage;

                    #region ForPrimitiveType
                    // Needed if the controller gets a primitive type like string or int etc.
                    if (error.Contains("''"))
                    {
                        error = error.Replace("''", $"'{key}'");
                    }
                    #endregion

                    return new BadRequestObjectResult(error);
                };
            });

            return mvcBuilder;
        }

        /// <summary>
        /// Uses the validation with english language.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseSmartHomeConfigurationValidation(this IApplicationBuilder app, ILogger logger)
        {
            // Force the default English messages to be used.
            ValidatorOptions.Global.LanguageManager.Enabled = false;
            logger.LogInformation("ValidatorOptions.Global.LanguageManager.Enabled: {value}. Force the default {language} messages to be used.", false, "English");
            return app;
        }
    }
}
