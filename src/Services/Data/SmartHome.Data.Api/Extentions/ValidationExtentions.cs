using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Text.Json.Serialization;

namespace SmartHome.Data.Api.Extentions
{
    using Validators;

    /// <summary>
    /// Determines Validation Extentions.
    /// </summary>
    public static class ValidationExtentions
    {
        /// <summary>
        /// Adds a model validation for the site SmartHome Data.
        /// </summary>
        /// <param name="mvcBuilder"></param>
        /// <returns></returns>
        public static IMvcBuilder AddSmartHomeDataValidation(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<DataRequestValidator>());
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

                    /* Uncomment when need log all messages of invalid state.
                    if (!context.ModelState.IsValid)
                    {
                        LogAutomaticBadRequest(context);
                    }
                    */

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
        public static IApplicationBuilder UseSmartHomeDataValidation(this IApplicationBuilder app, ILogger logger)
        {
            // Force the default English messages to be used.
            ValidatorOptions.Global.LanguageManager.Enabled = false;
            logger.LogInformation("ValidatorOptions.Global.LanguageManager.Enabled: {value}. Force the default {language} messages to be used.", false, "English");
            return app;
        }

        /* Uncomment when need log all messages of invalid state.
        private static void LogAutomaticBadRequest(ActionContext context)
        {
            // Setup logger from DI - as explained in https://github.com/dotnet/AspNetCore.Docs/issues/12157
            var loggerFactory = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger(context.ActionDescriptor.DisplayName);

            // Get error messages
            var errorMessages = string.Join(" | ", context.ModelState.Values
                .SelectMany(x => x.Errors)
                .Select(x => x.ErrorMessage));

            var request = context.HttpContext.Request;

            logger.LogWarning(
                "Validation error. Request method: {requestMethod}. Full URL: {requestPath}{requestQueryString}. Error(s): {errorMessages}",
                 request.Method, request.Path, request.QueryString, errorMessages);
        }
        */
    }
}
