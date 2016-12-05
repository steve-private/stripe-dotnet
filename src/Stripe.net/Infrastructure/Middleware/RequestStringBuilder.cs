﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using Newtonsoft.Json;

namespace Stripe.Infrastructure.Middleware
{
    public static class RequestStringBuilder
    {
        private static IEnumerable<IParserPlugin> ParserPlugins { get; set; }

        static RequestStringBuilder()
        {
            if (ParserPlugins != null) return;

            // use reflection so this works on the bin directory later for additional plugin processing tools

            ParserPlugins = new List<IParserPlugin>
            {
                new AdditionalOwnerPlugin(),
                new DictionaryPlugin(),
                new DateFilterPlugin()
            };
        }

        internal static void ProcessPlugins(ref string requestString, JsonPropertyAttribute attribute, PropertyInfo property, object propertyValue, object propertyParent)
        {
            var parsedParameter = false;

            foreach (var plugin in ParserPlugins)
            {
                if(!parsedParameter)
                    parsedParameter = plugin.Parse(ref requestString, attribute, property, propertyValue, propertyParent);
            }

            if (!parsedParameter)
                ApplyParameterToRequestString(ref requestString, attribute.PropertyName, propertyValue.ToString());
        }

        public static void ApplyParameterToRequestString(ref string requestString, string argument, string value)
        {
            var token = "&";

            if (!requestString.Contains("?"))
                token = "?";

            requestString = $"{requestString}{token}{argument}={WebUtility.UrlEncode(value)}";
        }
    }
}
