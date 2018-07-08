using System;

namespace Newtonsoft.Json
{
    internal class NoiseHumReductionConverter : JsonConverter
    {
        #region Public Properties
        public override bool CanWrite => false;
        #endregion

        #region Constructor
        public NoiseHumReductionConverter()
        {
        }
        #endregion

        #region Public Override Methods
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            var value = serializer.Deserialize(reader);
            var valueType = value.GetType();

            long? result = (valueType == typeof(Int64)) ? (long?)value : null;

            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotSupportedException();
        }
        #endregion
    }
}