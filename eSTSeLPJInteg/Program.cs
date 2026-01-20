using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSTSeLPJInteg
{
    class Program
    {
        static void Main(string[] args)
        {
           
            Integration integ = new Integration();

            Console.WriteLine("Integration Process..");
            integ.execIntegration();
            Environment.Exit(0);
        }
    }
}
