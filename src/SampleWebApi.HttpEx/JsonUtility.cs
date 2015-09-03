using System.Collections.Generic;
using HttpEx.REST;
using Microsoft.Framework.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace HttpEx
{
    public static class JsonUtility
    {
        public static JsonSerializerSettings DefaultSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Converters = new List<JsonConverter>
                    {
                        new HyperlinkConverter(),
                        new ResourceConverter(),
                        new StringEnumConverter { CamelCaseText = true }
                    }
        };

        public static void ConfigureDefaults(IServiceCollection services)
        {
            JsonConvert.DefaultSettings = () => DefaultSettings;
			services.ConfigureMvcJson((j) =>
			{
				j.SerializerSettings.Formatting = DefaultSettings.Formatting;
				j.SerializerSettings.ContractResolver = DefaultSettings.ContractResolver;
				j.SerializerSettings.Converters = DefaultSettings.Converters;
			});
		}
    }
}
