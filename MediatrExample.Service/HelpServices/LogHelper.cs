﻿using MediatrExample.Core.Interfaces.Service;
using MediatrExample.Shared.CustomMethod;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace MediatrExample.Service.HelpServices
{
    public class LogHelper<T> : ILogHelper<T> where T : class
    {
        private readonly ILogger<T> _logger;
        private readonly IHttpContextAccessor _httpContext;
        public LogHelper(ILoggerFactory logger, IHttpContextAccessor httpContext)
        {
            _logger = logger.CreateLogger<T>();
            _httpContext = httpContext;
        }

        private string GetCorrelationId()
        {
            return _httpContext.HttpContext.TraceIdentifier;
        }

        public void LogError(Exception exception)
        {
            string correlationId = GetCorrelationId();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"CorrelationId: {correlationId}");
            stringBuilder.AppendLine($"Message: {exception.Message}");
            stringBuilder.AppendLine($"Exception: {exception.ExpectExceptionMessage()}");

            _logger.LogError(stringBuilder.ToString());
        }

        public void LogError(string message, Exception exception)
        {
            string correlationId = GetCorrelationId();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"CorrelationId: {correlationId}");
            stringBuilder.AppendLine($"Message: {message} - {exception.Message}");
            stringBuilder.AppendLine($"Exception: {exception.ExpectExceptionMessage()}");

            _logger.LogError(stringBuilder.ToString());
        }

        public void LogInfo(string message)
        {
            string correlationId = GetCorrelationId();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"CorrelationId: {correlationId}");
            stringBuilder.AppendLine($"Message: {message}");

            _logger.LogInformation(stringBuilder.ToString());
        }

        public void LogInfo<TObj>(string message, TObj obje) where TObj : class, new()
        {
            string correlationId = GetCorrelationId();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"CorrelationId: {correlationId}");
            stringBuilder.AppendLine($"Message: {message}");
            stringBuilder.AppendLine($"Object: {JsonSerializer.Serialize(SetMaskSensitiveProp2(obje))}");

            _logger.LogInformation(stringBuilder.ToString());
        }

        private TObj SetMaskSensitiveProp<TObj>(TObj obj) where TObj : class, new()
        {
            var newObj = obj.CreateDeepCopy();
            var properties = newObj.GetType().GetProperties();
            foreach (var property in properties)
            {
                if (property.IsBasicProp())
                {
                    if (property.IsSensitiveProp())
                    {
                        property.SetValue(newObj, "********");
                    }
                }
                else
                {
                    var value = property.GetValue(newObj);
                    property.SetValue(newObj, SetMaskSensitiveProp(value));
                }
            }
            return newObj;
        }

        private TObj SetMaskSensitiveProp2<TObj>(TObj obj) where TObj : class, new()
        {
            var newObj = new TObj();
            var properties = obj.GetType().GetProperties();
            foreach (var property in properties)
            {
                if (property.IsBasicProp())
                {
                    if (property.IsSensitiveProp())
                    {
                        property.SetValue(newObj, "********");
                    }
                }
                else
                {
                    var value = property.GetValue(obj);
                    property.SetValue(newObj, SetMaskSensitiveProp(value));
                }
            }
            return newObj;
        }
    }
}
