using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimMq.Host.Console.BusinessService
{
    internal class TestBusiness1
    {
        internal Task StartAync()
        {
            var numbers = Enumerable.Range(1, 10000);


            foreach (var number in numbers)
            {
                Console.WriteLine(number);
            }


            return Task.CompletedTask
        }
    }
}
