﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Stripe.Infrastructure;

namespace Stripe
{
    public class StripeRefund : StripeEntityWithId
    {
        [JsonProperty("object")]
        public string Object { get; set; }

        [JsonProperty("amount")]
        public int Amount { get; set; }

        #region Expandable Balance Transaction
        public string BalanceTransactionId { get; set; }

        [JsonIgnore]
        public StripeBalanceTransaction BalanceTransaction { get; set; }

        [JsonProperty("balance_transaction")]
        internal object InternalBalanceTransaction
        {
            set
            {
                StringOrObject<StripeBalanceTransaction>.Map(value, s => BalanceTransactionId = s, o => BalanceTransaction = o);
            }
        }
        #endregion

        #region Expandable Charge
        public string ChargeId { get; set; }

        [JsonIgnore]
        public StripeCharge Charge { get; set; }

        [JsonProperty("charge")]
        internal object InternalCharge
        {
            set
            {
                StringOrObject<StripeCharge>.Map(value, s => ChargeId = s, o => Charge = o);
            }
        }
        #endregion

        [JsonProperty("created")]
        [JsonConverter(typeof(StripeDateTimeConverter))]
        public DateTime Created { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        #region Expandable Failure Balance Transaction
        public string FailureBalanceTransactionId { get; set; }

        [JsonIgnore]
        public StripeBalanceTransaction FailureBalanceTransaction { get; set; }

        [JsonProperty("failure_balance_transaction")]
        internal object InternalFailureBalanceTransaction
        {
            set
            {
                StringOrObject<StripeBalanceTransaction>.Map(value, s => FailureBalanceTransactionId = s, o => FailureBalanceTransaction = o);
            }
        }
        #endregion

        [JsonProperty("failure_reason")]
        public string FailureReason { get; set; }

        [JsonProperty("metadata")]
        public Dictionary<string, string> Metadata { get; set; }

        [JsonProperty("reason")]
        public string Reason { get; set; }

        [JsonProperty("receipt_number")]
        public string ReceiptNumber { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}