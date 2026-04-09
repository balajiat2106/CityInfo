using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Services
{
    public class MailService:IMailService
    {
        private readonly string fromAddress;
        private readonly string toAddress;

        public MailService(Microsoft.Extensions.Configuration.IConfiguration config)
        {
            fromAddress = config["mailsettings:mailfrom"];
            toAddress = config["mailsettings:mailto"];
        }

        public void SendMail(string subject,string message)
        {
            Debug.WriteLine( $"This is the subject from {fromAddress}");
            Debug.WriteLine( $"This is the subject to {toAddress}");
        }
    }
}
