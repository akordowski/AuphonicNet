using AuphonicNet.Data;
using NUnit.Framework;
using System;

namespace AuphonicNet.Tests.Data
{
    [TestFixture]
    public class ProductionTest : TestBase<Production>
    {
        #region Constructor
        public ProductionTest()
            : base("production.json")
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
                Assert.That(Item.Bitrate, Is.EqualTo(320), "Bitrate");
                Assert.That(Item.ChangeAllowed, Is.True, "ChangeAllowed");
                Assert.That(Item.ChangeTime, Is.EqualTo(new DateTime(2018, 2, 2, 12, 34, 56)), "ChangeTime");
                Assert.That(Item.Channels, Is.EqualTo(2), "Channels");
                Assert.That(Item.Chapters.Count, Is.EqualTo(0), "Chapters");
                Assert.That(Item.CreationTime, Is.EqualTo(new DateTime(2018, 1, 1, 0, 12, 34)), "CreationTime");
                Assert.That(Item.CutEnd, Is.EqualTo(34), "CutEnd");
                Assert.That(Item.CutStart, Is.EqualTo(12), "CutStart");
                Assert.That(Item.EditPage, Is.EqualTo("edit_page"), "EditPage");
                Assert.That(Item.ErrorMessage, Is.EqualTo("error_message"), "ErrorMessage");
                Assert.That(Item.ErrorStatus, Is.EqualTo("error_status"), "ErrorStatus");
                Assert.That(Item.Format, Is.EqualTo("format"), "Format");
                Assert.That(Item.HasVideo, Is.True, "HasVideo");
                Assert.That(Item.Image, Is.EqualTo("image"), "Image");
                Assert.That(Item.InputFile, Is.EqualTo("input_file"), "InputFile");
                Assert.That(Item.IsMultitrack, Is.True, "IsMultitrack");
                Assert.That(Item.Length, Is.EqualTo(123), "Length");
                Assert.That(Item.LengthTimestring, Is.EqualTo("length_timestring"), "LengthTimestring");
                Assert.That(Item.Metadata, Is.Not.Null, "Metadata");
                Assert.That(Item.MultiInputFiles.Count, Is.EqualTo(0), "MultiInputFiles");
                Assert.That(Item.OutgoingServices.Count, Is.EqualTo(0), "OutgoingServices");
                Assert.That(Item.OutputBasename, Is.EqualTo("output_basename"), "OutputBasename");
                Assert.That(Item.OutputFiles.Count, Is.EqualTo(0), "OutputFiles");
                Assert.That(Item.Preset, Is.Null, "Preset");
                Assert.That(Item.Samplerate, Is.EqualTo(44100), "Samplerate");
                Assert.That(Item.Service, Is.EqualTo("service"), "Service");
                Assert.That(Item.SpeechRecognition, Is.Null, "SpeechRecognition");
                Assert.That(Item.StartAllowed, Is.True, "StartAllowed");
                Assert.That(Item.Statistics, Is.Not.Null, "Statistics");
                Assert.That(Item.Status, Is.EqualTo(ProductionStatus.SpeechRecognition), "Status");
                Assert.That(Item.StatusPage, Is.EqualTo("status_page"), "StatusPage");
                Assert.That(Item.StatusString, Is.EqualTo("status_string"), "StatusString");
                Assert.That(Item.Thumbnail, Is.EqualTo("thumbnail"), "Thumbnail");
                Assert.That(Item.UsedCredits, Is.Not.Null, "UsedCredits");
                Assert.That(Item.Uuid, Is.EqualTo("uuid"), "Uuid");
                Assert.That(Item.WarningMessage, Is.EqualTo("warning_message"), "WarningMessage");
                Assert.That(Item.WarningStatus, Is.EqualTo("warning_status"), "WarningStatus");
                Assert.That(Item.WaveformImage, Is.EqualTo("waveform_image"), "WaveformImage");
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
                Assert.That(json.Contains("\"bitrate\":"), Is.False, "bitrate");
                Assert.That(json.Contains("\"change_allowed\":"), Is.True, "change_allowed");
                Assert.That(json.Contains("\"change_time\":"), Is.False, "change_time");
                Assert.That(json.Contains("\"channels\":"), Is.False, "channels");
                Assert.That(json.Contains("\"chapters\":"), Is.True, "chapters");
                Assert.That(json.Contains("\"creation_time\":"), Is.False, "creation_time");
                Assert.That(json.Contains("\"cut_end\":"), Is.True, "cut_end");
                Assert.That(json.Contains("\"cut_start\":"), Is.True, "cut_start");
                Assert.That(json.Contains("\"edit_page\":"), Is.False, "edit_page");
                Assert.That(json.Contains("\"error_message\":"), Is.False, "error_message");
                Assert.That(json.Contains("\"error_status\":"), Is.False, "error_status");
                Assert.That(json.Contains("\"format\":"), Is.False, "format");
                Assert.That(json.Contains("\"has_video\":"), Is.True, "has_video");
                Assert.That(json.Contains("\"image\":"), Is.True, "image");
                Assert.That(json.Contains("\"input_file\":"), Is.True, "input_file");
                Assert.That(json.Contains("\"is_multitrack\":"), Is.True, "is_multitrack");
                Assert.That(json.Contains("\"length\":"), Is.False, "length");
                Assert.That(json.Contains("\"length_timestring\":"), Is.False, "length_timestring");
                Assert.That(json.Contains("\"metadata\":"), Is.True, "metadata");
                Assert.That(json.Contains("\"multi_input_files\":"), Is.True, "multi_input_files");
                Assert.That(json.Contains("\"outgoing_services\":"), Is.True, "outgoing_services");
                Assert.That(json.Contains("\"output_basename\":"), Is.True, "output_basename");
                Assert.That(json.Contains("\"output_files\":"), Is.True, "output_files");
                Assert.That(json.Contains("\"samplerate\":"), Is.False, "samplerate");
                Assert.That(json.Contains("\"service\":"), Is.True, "service");
                Assert.That(json.Contains("\"speech_recognition\":"), Is.True, "speech_recognition");
                Assert.That(json.Contains("\"start_allowed\":"), Is.True, "start_allowed");
                Assert.That(json.Contains("\"statistics\":"), Is.False, "statistics");
                Assert.That(json.Contains("\"status\":"), Is.False, "status");
                Assert.That(json.Contains("\"status_page\":"), Is.False, "status_page");
                Assert.That(json.Contains("\"status_string\":"), Is.False, "status_string");
                Assert.That(json.Contains("\"thumbnail\":"), Is.True, "thumbnail");
                Assert.That(json.Contains("\"used_credits\":"), Is.False, "used_credits");
                Assert.That(json.Contains("\"uuid\":"), Is.False, "uuid");
                Assert.That(json.Contains("\"warning_message\":"), Is.False, "warning_message");
                Assert.That(json.Contains("\"warning_status\":"), Is.False, "warning_status");
                Assert.That(json.Contains("\"waveform_image\":"), Is.False, "waveform_image");
                Assert.That(json.Contains("\"webhook\":"), Is.True, "webhook");
            });
        }

        [Test]
        public void Initialize_Constructor_1()
        {
            Production production = null;

            Assert.That(() => production = new Production(), Throws.Nothing);
            Assert.Multiple(() =>
            {
                Assert.That(production.Algorithms, Is.Null, "Algorithms");
                Assert.That(production.Bitrate, Is.Null, "Bitrate");
                Assert.That(production.ChangeAllowed, Is.False, "ChangeAllowed");
                Assert.That(production.ChangeTime, Is.EqualTo(DateTime.MinValue), "ChangeTime");
                Assert.That(production.Channels, Is.Null, "Channels");
                Assert.That(production.Chapters, Is.Null, "Chapters");
                Assert.That(production.CreationTime, Is.EqualTo(DateTime.MinValue), "CreationTime");
                Assert.That(production.CutEnd, Is.EqualTo(0), "CutEnd");
                Assert.That(production.CutStart, Is.EqualTo(0), "CutStart");
                Assert.That(production.EditPage, Is.Null, "EditPage");
                Assert.That(production.ErrorMessage, Is.Null, "ErrorMessage");
                Assert.That(production.ErrorStatus, Is.Null, "ErrorStatus");
                Assert.That(production.Format, Is.Null, "Format");
                Assert.That(production.HasVideo, Is.False, "HasVideo");
                Assert.That(production.Image, Is.Null, "Image");
                Assert.That(production.InputFile, Is.Null, "InputFile");
                Assert.That(production.IsMultitrack, Is.False, "IsMultitrack");
                Assert.That(production.Length, Is.Null, "Length");
                Assert.That(production.LengthTimestring, Is.Null, "LengthTimestring");
                Assert.That(production.Metadata, Is.Null, "Metadata");
                Assert.That(production.MultiInputFiles, Is.Null, "MultiInputFiles");
                Assert.That(production.OutgoingServices, Is.Null, "OutgoingServices");
                Assert.That(production.OutputBasename, Is.Null, "OutputBasename");
                Assert.That(production.OutputFiles, Is.Null, "OutputFiles");
                Assert.That(production.Preset, Is.Null, "Preset");
                Assert.That(production.Samplerate, Is.Null, "Samplerate");
                Assert.That(production.Service, Is.Null, "Service");
                Assert.That(production.SpeechRecognition, Is.Null, "SpeechRecognition");
                Assert.That(production.StartAllowed, Is.False, "StartAllowed");
                Assert.That(production.Statistics, Is.Null, "Statistics");
                Assert.That(production.Status, Is.EqualTo(ProductionStatus.FileUpload), "Status");
                Assert.That(production.StatusPage, Is.Null, "StatusPage");
                Assert.That(production.StatusString, Is.Null, "StatusString");
                Assert.That(production.Thumbnail, Is.Null, "Thumbnail");
                Assert.That(production.UsedCredits, Is.Null, "UsedCredits");
                Assert.That(production.Uuid, Is.Null, "Uuid");
                Assert.That(production.WarningMessage, Is.Null, "WarningMessage");
                Assert.That(production.WarningStatus, Is.Null, "WarningStatus");
                Assert.That(production.WaveformImage, Is.Null, "WaveformImage");
                Assert.That(production.Webhook, Is.Null, "Webhook");
            });
        }

        [Test]
        public void Initialize_Constructor_2(
            [Values(null, "", "  ", "presetUuid")] string presetUuid)
        {
            Type expectedException = null;
            string expectedParamName = null;

            if (String.IsNullOrWhiteSpace(presetUuid))
            {
                expectedException = presetUuid == null ? typeof(ArgumentNullException) : typeof(ArgumentException);
                expectedParamName = nameof(presetUuid);
            }

            if (expectedException == null)
            {
                Production production = null;

                Assert.That(() => production = new Production(presetUuid), Throws.Nothing);
                Assert.Multiple(() =>
                {
                    Assert.That(production.Algorithms, Is.Null, "Algorithms");
                    Assert.That(production.Bitrate, Is.Null, "Bitrate");
                    Assert.That(production.ChangeAllowed, Is.False, "ChangeAllowed");
                    Assert.That(production.ChangeTime, Is.EqualTo(DateTime.MinValue), "ChangeTime");
                    Assert.That(production.Channels, Is.Null, "Channels");
                    Assert.That(production.Chapters, Is.Null, "Chapters");
                    Assert.That(production.CreationTime, Is.EqualTo(DateTime.MinValue), "CreationTime");
                    Assert.That(production.CutEnd, Is.EqualTo(0), "CutEnd");
                    Assert.That(production.CutStart, Is.EqualTo(0), "CutStart");
                    Assert.That(production.EditPage, Is.Null, "EditPage");
                    Assert.That(production.ErrorMessage, Is.Null, "ErrorMessage");
                    Assert.That(production.ErrorStatus, Is.Null, "ErrorStatus");
                    Assert.That(production.Format, Is.Null, "Format");
                    Assert.That(production.HasVideo, Is.False, "HasVideo");
                    Assert.That(production.Image, Is.Null, "Image");
                    Assert.That(production.InputFile, Is.Null, "InputFile");
                    Assert.That(production.IsMultitrack, Is.False, "IsMultitrack");
                    Assert.That(production.Length, Is.Null, "Length");
                    Assert.That(production.LengthTimestring, Is.Null, "LengthTimestring");
                    Assert.That(production.Metadata, Is.Null, "Metadata");
                    Assert.That(production.MultiInputFiles, Is.Null, "MultiInputFiles");
                    Assert.That(production.OutgoingServices, Is.Null, "OutgoingServices");
                    Assert.That(production.OutputBasename, Is.Null, "OutputBasename");
                    Assert.That(production.OutputFiles, Is.Null, "OutputFiles");
                    Assert.That(production.Preset, Is.EqualTo(presetUuid), "Preset");
                    Assert.That(production.Samplerate, Is.Null, "Samplerate");
                    Assert.That(production.Service, Is.Null, "Service");
                    Assert.That(production.SpeechRecognition, Is.Null, "SpeechRecognition");
                    Assert.That(production.StartAllowed, Is.False, "StartAllowed");
                    Assert.That(production.Statistics, Is.Null, "Statistics");
                    Assert.That(production.Status, Is.EqualTo(ProductionStatus.FileUpload), "Status");
                    Assert.That(production.StatusPage, Is.Null, "StatusPage");
                    Assert.That(production.StatusString, Is.Null, "StatusString");
                    Assert.That(production.Thumbnail, Is.Null, "Thumbnail");
                    Assert.That(production.UsedCredits, Is.Null, "UsedCredits");
                    Assert.That(production.Uuid, Is.Null, "Uuid");
                    Assert.That(production.WarningMessage, Is.Null, "WarningMessage");
                    Assert.That(production.WarningStatus, Is.Null, "WarningStatus");
                    Assert.That(production.WaveformImage, Is.Null, "WaveformImage");
                    Assert.That(production.Webhook, Is.Null, "Webhook");
                });
            }
            else
            {
                Assert.That(() => new Production(presetUuid), Throws
                    .Exception.TypeOf(expectedException)
                    .With.Property("ParamName").EqualTo(expectedParamName));
            }
        }
        #endregion
    }
}