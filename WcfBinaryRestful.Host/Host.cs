using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace WcfBinaryRestful.Host
{
    class Host
    {
        static void Main(string[] args)
        {
            Console.WriteLine(System.Environment.CurrentDirectory);
            using (ServiceHost host = new ServiceHost(typeof(Service.Service)))
            {
                
                host.Open();
                Console.WriteLine("Service already started...");
                Console.ReadKey();
            }
        }
    }
}
