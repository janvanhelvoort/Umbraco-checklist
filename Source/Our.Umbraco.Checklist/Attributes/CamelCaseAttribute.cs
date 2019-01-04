﻿namespace Our.Umbraco.Checklist.Attributes
{
    using System;
    using System.Linq;
    using System.Net.Http.Formatting;
    using System.Web.Http.Controllers;

    using Newtonsoft.Json.Serialization;

    public class CamelCaseAttribute : Attribute, IControllerConfiguration
    {
        public void Initialize(HttpControllerSettings currentConfiguration, HttpControllerDescriptor currentDescriptor)
        {
            var currentFormatter = currentConfiguration.Formatters.OfType<JsonMediaTypeFormatter>().Single();
            
            currentConfiguration.Formatters.Remove(currentFormatter);

            var camelFormatter = new JsonMediaTypeFormatter
            {
                SerializerSettings = { ContractResolver = new CamelCasePropertyNamesContractResolver() }
            };
            
            currentConfiguration.Formatters.Add(camelFormatter);
        }
    }
}
