﻿using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace YuzuDelivery.TemplateEngines.Handlebars.Helpers;

public static class EnumResolver
{
    public static object Convert(object obj)
    {
        if (obj != null && obj.GetType().IsEnum)
        {
            var jsonSettings = new JsonSerializerSettings
            {
                Converters = new List<JsonConverter> { new StringEnumConverter() }
            };

            var enumValue = JsonConvert.SerializeObject(obj, jsonSettings);
            if (enumValue != null)
                enumValue = enumValue.Replace("\"", "");
            else
                enumValue = string.Empty;

            if (enumValue.StartsWith("_"))
                enumValue = enumValue.Substring(1);

            return enumValue;
        }
        return obj;
    }

}
