using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serializers;

namespace AuphonicNet.Json
{
    internal class NewtonsoftJsonSerializer : ISerializer, IDeserializer
    {
        #region Public Static Properties
        public static NewtonsoftJsonSerializer Default => new NewtonsoftJsonSerializer();
        #endregion

        #region Public Properties
        public string ContentType { get; set; }
        public string DateFormat { get; set; }
        public string Namespace { get; set; }
        public string RootElement { get; set; }
        #endregion

        #region Private Fields
        private JsonSerializerSettings _deserializationSettings;
        private JsonSerializerSettings _serializationSettings;
        #endregion

        #region Constructor
        public NewtonsoftJsonSerializer()
        {
            ContentType = "application/json";

            _deserializationSettings = new JsonSerializerSettings()
            {
                ContractResolver = new DeserializeContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Utc
            };

            _serializationSettings = new JsonSerializerSettings()
            {
                ContractResolver = new SerializeContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Utc
            };
        }
        #endregion

        #region Public Methods
        public T Deserialize<T>(string content)
        {
            return JsonConvert.DeserializeObject<T>(content, _deserializationSettings);
        }

        public T Deserialize<T>(IRestResponse response)
        {
            return JsonConvert.DeserializeObject<T>(response.Content, _deserializationSettings);
        }

        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented, _serializationSettings);
        }
        #endregion
    }
}