using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimMq.Host.Console.Tasks
{
    internal class TestTask1
    {
        internal Task RunAync()
        {
            return Task.CompletedTask;
        }
    }
}
