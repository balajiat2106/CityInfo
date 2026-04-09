using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Services
{
    public class CloudMailService:IMailService
    {
        private readonly string fromAddress;
        private readonly string toAddress;

        public CloudMailService(Microsoft.Extensions.Configuration.IConfiguration config)
        {
            fromAddress = config["mailsettings:mailfrom"];
            toAddress = config["mailsettings:mailto"];
        }

        public void SendMail(string subject, string message)
        {
            Debug.WriteLine($"This is the Cloud subject {subject}");
            Debug.WriteLine($"This is the Cloud subject {message}");
        }
    }
}
