using System;
using System.Collections;
using System.Collections.Generic;

namespace Newtonsoft.Json
{
    internal class DictionaryEnumKeyConverter<T> : JsonConverter
        where T : struct
    {
        #region Public Properties
        public override bool CanWrite => false;
        #endregion

        #region Constructor
        public DictionaryEnumKeyConverter()
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type.");
            }
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

            var valueType = objectType.GetGenericArguments()[1];
            var intermediateDictionaryType = typeof(Dictionary<,>).MakeGenericType(typeof(string), valueType);
            var intermediateDictionary = (IDictionary)Activator.CreateInstance(intermediateDictionaryType);

            serializer.Populate(reader, intermediateDictionary);

            var dict = (IDictionary)Activator.CreateInstance(objectType);

            foreach (DictionaryEntry pair in intermediateDictionary)
            {
                dict.Add(Enum.Parse(typeof(T), pair.Key.ToString()), pair.Value);
            }

            return dict;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotSupportedException();
        }
        #endregion
    }
}