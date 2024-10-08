﻿using FalzoniNetFCSharp.Utils.Keys;
using Microsoft.AspNet.Identity;
using System.Diagnostics;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace FalzoniNetFCSharp.Service.Senders.Sms
{
    internal class SmsTwilioMessageService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            TwilioClient.Init(TwilioKeys.SMSAccountIdentification, TwilioKeys.SMSAccountPassword);

            var smsMessage = await Task.FromResult(MessageResource.Create(
                    body: message.Body,
                    from: new Twilio.Types.PhoneNumber(TwilioKeys.SMSAccountFrom),
                    to: new Twilio.Types.PhoneNumber(message.Destination)
                ));

            Trace.TraceInformation(smsMessage.Sid);
        }
    }
}
