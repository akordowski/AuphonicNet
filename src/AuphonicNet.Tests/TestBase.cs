using AuphonicNet.Json;
using NUnit.Framework;
using System;
using System.IO;
using System.Reflection;

namespace AuphonicNet.Tests
{
    public class TestBase<T>
    {
        #region Protected Properties
        public string Json { get; private set; }
        public T Item { get; set; }
        #endregion

        #region Private Fields
        private NewtonsoftJsonSerializer _serializer = new NewtonsoftJsonSerializer();
        private string _jsonFile;
        #endregion

        #region Constructor
        public TestBase(string jsonFile)
        {
            _jsonFile = jsonFile;
        }
        #endregion

        #region Public Methods
        public void Deserialize()
        {
            Item = _serializer.Deserialize<T>(Json);
        }

        public string Serialize()
        {
            return _serializer.Serialize(Item);
        }
        #endregion

        #region SetUp
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var location = new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase);
            var dir = new FileInfo(location.AbsolutePath).Directory.FullName;
            var file = Path.Combine(dir, $"json/{_jsonFile}");
            Json = File.ReadAllText(file);
        }
        #endregion
    }
}