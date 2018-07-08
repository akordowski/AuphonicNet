using AuphonicNet.Data;
using NUnit.Framework;
using System;

namespace AuphonicNet.Tests.Data
{
    [TestFixture]
    public class MusicSpeechTest : TestBase<MusicSpeech>
    {
        #region Constructor
        public MusicSpeechTest()
            : base("music_speech.json")
        {
        }
        #endregion

        #region Tests
        [Test, Order(1)]
        public void Deserialize_Returns_Valid_Result()
        {
            Deserialize();

            Assert.Multiple(() =>
            {
                Assert.That(Item.Label, Is.EqualTo("label"), "Label");
                Assert.That(Item.Start, Is.EqualTo("start"), "Start");
                Assert.That(Item.StartSec, Is.EqualTo(123), "StartSec");
                Assert.That(Item.Stop, Is.EqualTo("stop"), "Stop");
                Assert.That(Item.StopSec, Is.EqualTo(456), "StopSec");
            });
        }

        [Test, Order(2)]
        public void Serialize_Returns_Valid_Result()
        {
            var json = Serialize();

            Assert.That(String.IsNullOrWhiteSpace(json), Is.False);
            Assert.Multiple(() =>
            {
                Assert.That(json.Contains("\"label\":"), Is.True, "label");
                Assert.That(json.Contains("\"start\":"), Is.True, "start");
                Assert.That(json.Contains("\"start_sec\":"), Is.True, "start_sec");
                Assert.That(json.Contains("\"stop\":"), Is.True, "stop");
                Assert.That(json.Contains("\"stop_sec\":"), Is.True, "stop_sec");
            });
        }
        [Test]
        public void Initialize_Constructor()
        {
            MusicSpeech musicSpeech = null;

            Assert.That(() => musicSpeech = new MusicSpeech(), Throws.Nothing);
            Assert.Multiple(() =>
            {
                Assert.That(musicSpeech.Label, Is.Null, "Label");
                Assert.That(musicSpeech.Start, Is.Null, "Start");
                Assert.That(musicSpeech.StartSec, Is.EqualTo(0), "StartSec");
                Assert.That(musicSpeech.Stop, Is.Null, "Stop");
                Assert.That(musicSpeech.StopSec, Is.EqualTo(0), "StopSec");
            });
        }

        [Test]
        public void Invoke_ToString_Returns_Valid_Result()
        {
            var musicSpeech = new MusicSpeech
            {
                Label = "Label",
                Start = "Start",
                Stop = "Stop"
            };

            Assert.That(musicSpeech.ToString(), Is.EqualTo($"{musicSpeech.Label} (Start = {musicSpeech.Start}; Stop = {musicSpeech.Stop})"));
        }
        #endregion
    }
}