﻿using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;

namespace SmartHome.Data.Api.Validators
{
    using Extentions;
    using Models;
    using SmartHome.Configuration.Abstractions;
    using SmartHome.Configuration.Abstractions.Models;

    /// <summary>
    /// Determines rules SensorDataRequest validation.
    /// </summary>
    public class DataRequestValidator : AbstractValidator<SensorDataRequest>
    {
        private readonly ISensorConfiguration _sensorConfiguration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<DataRequestValidator> _logger;
        private Sensor _sensor;
        private const string StructuredData = "Data: {@SensorDataRequest}.";
        private const string StructuredDataAndConfig = "Data: {@SensorDataRequest}. Configuration: {@Sensor}.";

        /// <summary>
        /// Creates DataRequestValidator.
        /// </summary>
        public DataRequestValidator(ISensorConfiguration sensorConfiguration, IHttpContextAccessor httpContextAccessor, ILogger<DataRequestValidator> logger)
        {
            _sensorConfiguration = sensorConfiguration ?? throw new ArgumentNullException(nameof(sensorConfiguration));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            CascadeMode = CascadeMode.Stop;

            RuleSet("UpdateSensorData", () =>
            {
                RuleFor(x => x.Timestamp)
                    .NotEmpty()
                    .Must(x => (DateTime.UtcNow - x.ToUniversalTime()) <= TimeSpan.FromDays(2) && x.ToUniversalTime() <= DateTime.UtcNow)
                    .WithMessage(x => Error("The Timestamp no musts be great than 2 days.", StructuredData, x));

                RuleFor(x => x.Value)
                    .NotNull()
                    .Must(x => x <= _sensor.MaxValue)
                    .WithMessage(x => Error("The Value musts be less than MaxValue or equals MaxValue.", StructuredDataAndConfig, x, _sensor))
                    .Must(x => x >= _sensor.MinValue)
                    .WithMessage(x => Error("The Value musts be great than MinValue or equals MinValue.", StructuredDataAndConfig, x, _sensor));
            });
        }

        /// <summary>
        /// The method should return true if validation should continue, or false to immediately abort.
        /// Any modifications that you made to the ValidationResult will be returned to the user.
        /// Checks the sensor is not null and it configuration is exists and the sensor enabled.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        protected override bool PreValidate(ValidationContext<SensorDataRequest> context, ValidationResult result)
        {
            var sensorDataRequest = context.InstanceToValidate;

            if (sensorDataRequest.IsNullOrEmpty())
            {
                return Error($"Please ensure a {nameof(SensorDataRequest)} was supplied.", null, ref context, ref result);
            }

            #region Check The Sensor Configuration
            if (!_sensorConfiguration.TryGetSensorConfiguration(context.InstanceToValidate.SensorTypeId, out var sensor))
            {
                return Error("No one configuration found.", StructuredData, ref context, ref result, sensorDataRequest);
            }

            if (!sensor.IsEnabled)
            {
                return Error("The sensor is not enabled.", StructuredDataAndConfig, ref context, ref result, sensorDataRequest, sensor);
            }
            #endregion

            _sensor = sensor;

            return true;
        }

        private string Error(string message, string additionalStructuredInfo, params object[] args)
        {
            Log(ref message, ref additionalStructuredInfo, args);
            return message;
        }

        private bool Error(string message, string additionalStructuredInfo, ref ValidationContext<SensorDataRequest> context, ref ValidationResult result, params object[] args)
        {
            result.Errors.Add(new ValidationFailure("", message));
            Log(ref message, ref additionalStructuredInfo, args);
            return false;
        }

        private void Log(ref string message, ref string additionalStructuredInfo, params object[] args)
        {
            var request = _httpContextAccessor.HttpContext?.Request;

            if (request == null)
            {
                if (args == null)
                {
                    _logger.LogWarning($"{message} {additionalStructuredInfo}");
                }
                else
                {
                    _logger.LogWarning($"{message} {additionalStructuredInfo}", args);
                }
            }
            else
            {
                if (args == null)
                {
                    _logger.LogWarning($"{message} {additionalStructuredInfo}" + " Request method: {RequestMethod}. Full URL: {RequestPath}{RequestQueryString}.", request.Method, request.Path, request.QueryString);
                }
                else
                {
                    _logger.LogWarning($"{message} {additionalStructuredInfo}" +
                                       " Request method: {RequestMethod}. Full URL: {RequestPath}{RequestQueryString}.",
                        args.ObjectAdd(request.Method, request.Path, request.QueryString));
                }
            }
        }
    }
}