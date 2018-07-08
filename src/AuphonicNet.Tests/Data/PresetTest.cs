using AuphonicNet.Data;
using NUnit.Framework;
using System;

namespace AuphonicNet.Tests.Data
{
    [TestFixture]
    public class PresetTest : TestBase<Preset>
    {
        #region Constructor
        public PresetTest()
            : base("preset.json")
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
                Assert.That(Item.Algorithms, Is.Not.Null, "Algorithms");
                Assert.That(Item.CreationTime, Is.EqualTo(new DateTime(2018, 1, 1, 12, 34, 56)), "CreationTime");
                Assert.That(Item.Image, Is.EqualTo("image"), "Image");
                Assert.That(Item.IsMultitrack, Is.True, "IsMultitrack");
                Assert.That(Item.Metadata, Is.Not.Null, "Metadata");
                Assert.That(Item.MultiInputFiles.Count, Is.EqualTo(0), "MultiInputFiles");
                Assert.That(Item.OutgoingServices.Count, Is.EqualTo(0), "OutgoingServices");
                Assert.That(Item.OutputBasename, Is.EqualTo("output_basename"), "OutputBasename");
                Assert.That(Item.OutputFiles.Count, Is.EqualTo(0), "OutputFiles");
                Assert.That(Item.PresetName, Is.EqualTo("preset_name"), "PresetName");
                Assert.That(Item.SpeechRecognition, Is.Null, "SpeechRecognition");
                Assert.That(Item.Thumbnail, Is.EqualTo("thumbnail"), "Thumbnail");
                Assert.That(Item.Uuid, Is.EqualTo("uuid"), "Uuid");
                Assert.That(Item.Webhook, Is.EqualTo("webhook"), "Webhook");
            });
        }

        [Test, Order(2)]
        public void Serialize_Returns_Valid_Result()
        {
            var json = Serialize();

            Assert.Multiple(() =>
            {
                Assert.That(String.IsNullOrWhiteSpace(json), Is.False);
                Assert.That(json.Contains("\"algorithms\":"), Is.True, "algorithms");
                Assert.That(json.Contains("\"creation_time\":"), Is.False, "creation_time");
                Assert.That(json.Contains("\"image\":"), Is.True, "image");
                Assert.That(json.Contains("\"is_multitrack\":"), Is.True, "is_multitrack");
                Assert.That(json.Contains("\"metadata\":"), Is.True, "metadata");
                Assert.That(json.Contains("\"multi_input_files\":"), Is.True, "multi_input_files");
                Assert.That(json.Contains("\"outgoing_services\":"), Is.True, "outgoing_services");
                Assert.That(json.Contains("\"output_basename\":"), Is.True, "output_basename");
                Assert.That(json.Contains("\"output_files\":"), Is.True, "output_files");
                Assert.That(json.Contains("\"speech_recognition\":"), Is.True, "speech_recognition");
                Assert.That(json.Contains("\"thumbnail\":"), Is.True, "thumbnail");
                Assert.That(json.Contains("\"uuid\":"), Is.True, "uuid");
                Assert.That(json.Contains("\"webhook\":"), Is.True, "Webhook");
            });
        }

        [Test]
        public void Initialize_Constructor_1()
        {
            Preset preset = null;

            Assert.That(() => preset = new Preset(), Throws.Nothing);
            Assert.Multiple(() =>
            {
                Assert.That(preset.Algorithms, Is.Null, "Algorithms");
                Assert.That(preset.CreationTime, Is.EqualTo(DateTime.MinValue), "CreationTime");
                Assert.That(preset.Image, Is.Null, "Image");
                Assert.That(preset.IsMultitrack, Is.False, "IsMultitrack");
                Assert.That(preset.Metadata, Is.Null, "Metadata");
                Assert.That(preset.MultiInputFiles, Is.Null, "MultiInputFiles");
                Assert.That(preset.OutgoingServices, Is.Null, "OutgoingServices");
                Assert.That(preset.OutputBasename, Is.Null, "OutputBasename");
                Assert.That(preset.OutputFiles, Is.Null, "OutputFiles");
                Assert.That(preset.PresetName, Is.Null, "PresetName");
                Assert.That(preset.SpeechRecognition, Is.Null, "SpeechRecognition");
                Assert.That(preset.Thumbnail, Is.Null, "Thumbnail");
                Assert.That(preset.Uuid, Is.Null, "Uuid");
                Assert.That(preset.Webhook, Is.Null, "Webhook");
            });
        }

        [Test]
        public void Initialize_Constructor_2(
            [Values(null, "", "  ", "presetName")] string presetName)
        {
            Type expectedException = null;
            string expectedParamName = null;

            if (String.IsNullOrWhiteSpace(presetName))
            {
                expectedException = presetName == null ? typeof(ArgumentNullException) : typeof(ArgumentException);
                expectedParamName = nameof(presetName);
            }

            if (expectedException == null)
            {
                Preset preset = null;

                Assert.That(() => preset = new Preset(presetName), Throws.Nothing);
                Assert.Multiple(() =>
                {
                    Assert.That(preset.Algorithms, Is.Null, "Algorithms");
                    Assert.That(preset.CreationTime, Is.EqualTo(DateTime.MinValue), "CreationTime");
                    Assert.That(preset.Image, Is.Null, "Image");
                    Assert.That(preset.IsMultitrack, Is.False, "IsMultitrack");
                    Assert.That(preset.Metadata, Is.Null, "Metadata");
                    Assert.That(preset.MultiInputFiles, Is.Null, "MultiInputFiles");
                    Assert.That(preset.OutgoingServices, Is.Null, "OutgoingServices");
                    Assert.That(preset.OutputBasename, Is.Null, "OutputBasename");
                    Assert.That(preset.OutputFiles, Is.Null, "OutputFiles");
                    Assert.That(preset.PresetName, Is.EqualTo(presetName), "PresetName");
                    Assert.That(preset.SpeechRecognition, Is.Null, "SpeechRecognition");
                    Assert.That(preset.Thumbnail, Is.Null, "Thumbnail");
                    Assert.That(preset.Uuid, Is.Null, "Uuid");
                    Assert.That(preset.Webhook, Is.Null, "Webhook");
                });
            }
            else
            {
                Assert.That(() => new Preset(presetName), Throws
                    .Exception.TypeOf(expectedException)
                    .With.Property("ParamName").EqualTo(expectedParamName));
            }
        }

        [Test]
        public void Invoke_ToString_Returns_Valid_Result()
        {
            var preset = new Preset("PresetName");

            Assert.That(preset.ToString(), Is.EqualTo(preset.PresetName));
        }
        #endregion
    }
}