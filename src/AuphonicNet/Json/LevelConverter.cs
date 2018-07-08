using AuphonicNet.Data;
using Newtonsoft.Json.Linq;
using System;

namespace Newtonsoft.Json
{
    internal class LevelConverter : JsonConverter
    {
        #region Public Properties
        public override bool CanWrite => false;
        #endregion

        #region Constructor
        public LevelConverter()
        {
        }
        #endregion

        #region Public Override Methods
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Level);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            var arr = serializer.Deserialize<JArray>(reader);
            var level = new Level();

            if (arr.Count == 2)
            {
                level.Value = Convert.ToDecimal(arr[0]);
                level.Unit = Convert.ToString(arr[1]);
            }

            return level;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotSupportedException();
        }
        #endregion
    }
}