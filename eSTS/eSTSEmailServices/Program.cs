using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSTSEmailServices
{
    class Program
    {
        static void Main(string[] args)
        {
            SendEmailService sendEmailBL = new SendEmailService();
            Console.WriteLine("Sending Whatsapp..");
            sendEmailBL.execSTSSendEmail();
            Environment.Exit(0);
        }
    }
}
