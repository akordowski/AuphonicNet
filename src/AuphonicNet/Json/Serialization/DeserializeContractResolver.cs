namespace Newtonsoft.Json.Serialization
{
    internal class DeserializeContractResolver : DefaultContractResolver
    {
        #region Constructor
        public DeserializeContractResolver()
        {
            NamingStrategy = new SnakeCaseNamingStrategy();
        }
        #endregion
    }
}