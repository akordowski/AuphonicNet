using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Newtonsoft.Json.Serialization
{
    internal class SerializeContractResolver : DefaultContractResolver
    {
        #region Constructor
        public SerializeContractResolver()
        {
            NamingStrategy = new SnakeCaseNamingStrategy();
        }
        #endregion

        #region Protected Override Properties
        protected override List<MemberInfo> GetSerializableMembers(Type objectType)
        {
            return objectType
                .GetProperties()
                .Where(pi => !Attribute.IsDefined(pi, typeof(JsonIgnoreSerializationAttribute)))
                .ToList<MemberInfo>();
        }
        #endregion
    }
}