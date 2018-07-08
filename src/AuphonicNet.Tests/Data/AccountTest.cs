using AuphonicNet.Data;
using NUnit.Framework;
using System;

namespace AuphonicNet.Tests.Data
{
    [TestFixture]
    public class AccountTest : TestBase<Account>
    {
        #region Constructor
        public AccountTest()
            : base("account.json")
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
                Assert.That(Item.Credits, Is.EqualTo(12.34), "Credits");
                Assert.That(Item.DateJoined, Is.EqualTo(new DateTime(2018, 1, 1, 0, 1, 23)), "DateJoined");
                Assert.That(Item.Email, Is.EqualTo("email"), "Email");
                Assert.That(Item.ErrorEmail, Is.True, "ErrorEmail");
                Assert.That(Item.LowCreditsEmail, Is.True, "LowCreditsEmail");
                Assert.That(Item.LowCreditsThreshold, Is.EqualTo(1.23), "LowCreditsThreshold");
                Assert.That(Item.NotificationEmail, Is.True, "NotificationEmail");
                Assert.That(Item.OnetimeCredits, Is.EqualTo(2.34), "OnetimeCredits");
                Assert.That(Item.RechargeDate, Is.EqualTo(new DateTime(2018, 2, 2, 0, 12, 34)), "RechargeDate");
                Assert.That(Item.RechargeRecurringCredits, Is.EqualTo(3.45), "RechargeRecurringCredits");
                Assert.That(Item.RecurringCredits, Is.EqualTo(4.56), "RecurringCredits");
                Assert.That(Item.UserId, Is.EqualTo("user_id"), "UserId");
                Assert.That(Item.Username, Is.EqualTo("username"), "Username");
                Assert.That(Item.WarningEmail, Is.True, "WarningEmail");
            });
        }

        [Test, Order(2)]
        public void Serialize_Returns_Valid_Result()
        {
            var json = Serialize();

            Assert.That(String.IsNullOrWhiteSpace(json), Is.False);
            Assert.Multiple(() =>
            {
                Assert.That(json.Contains("\"credits\":"), Is.True, "credits");
                Assert.That(json.Contains("\"date_joined\":"), Is.True, "date_joined");
                Assert.That(json.Contains("\"email\":"), Is.True, "email");
                Assert.That(json.Contains("\"error_email\":"), Is.True, "error_email");
                Assert.That(json.Contains("\"low_credits_email\":"), Is.True, "low_credits_email");
                Assert.That(json.Contains("\"low_credits_threshold\":"), Is.True, "low_credits_threshold");
                Assert.That(json.Contains("\"notification_email\":"), Is.True, "notification_email");
                Assert.That(json.Contains("\"onetime_credits\":"), Is.True, "onetime_credits");
                Assert.That(json.Contains("\"recharge_date\":"), Is.True, "recharge_date");
                Assert.That(json.Contains("\"recharge_recurring_credits\":"), Is.True, "recharge_recurring_credits");
                Assert.That(json.Contains("\"recurring_credits\":"), Is.True, "recurring_credits");
                Assert.That(json.Contains("\"user_id\":"), Is.True, "user_id");
                Assert.That(json.Contains("\"username\":"), Is.True, "username");
                Assert.That(json.Contains("\"warning_email\":"), Is.True, "warning_email");
            });
        }

        [Test]
        public void Initialize_Constructor()
        {
            Account account = null;

            Assert.That(() => account = new Account(), Throws.Nothing);
            Assert.Multiple(() =>
            {
                Assert.That(account.Credits, Is.EqualTo(0), "Credits");
                Assert.That(account.DateJoined, Is.EqualTo(DateTime.MinValue), "DateJoined");
                Assert.That(account.Email, Is.Null, "Email");
                Assert.That(account.ErrorEmail, Is.False, "ErrorEmail");
                Assert.That(account.LowCreditsEmail, Is.False, "LowCreditsEmail");
                Assert.That(account.LowCreditsThreshold, Is.EqualTo(0), "LowCreditsThreshold");
                Assert.That(account.NotificationEmail, Is.False, "NotificationEmail");
                Assert.That(account.OnetimeCredits, Is.EqualTo(0), "OnetimeCredits");
                Assert.That(account.RechargeDate, Is.EqualTo(DateTime.MinValue), "RechargeDate");
                Assert.That(account.RechargeRecurringCredits, Is.EqualTo(0), "RechargeRecurringCredits");
                Assert.That(account.RecurringCredits, Is.EqualTo(0), "RecurringCredits");
                Assert.That(account.UserId, Is.Null, "UserId");
                Assert.That(account.Username, Is.Null, "Username");
                Assert.That(account.WarningEmail, Is.False, "WarningEmail");
            });
        }

        [Test]
        public void Invoke_ToString_Returns_Valid_Result()
        {
            var account = new Account
            {
                Username = "Username"
            };

            Assert.That(account.ToString(), Is.EqualTo(account.Username));
        }
        #endregion
    }
}