using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SlimMq.Host.BatchTasks
{
    internal class TestTask1
    {
        internal Task RunAync()
        {


            //var _watcher = new FileSystemWatcher(Application.PickupPath);
            //_watcher.Created += FileCreated.EventHandler;
            //_watcher.Renamed += FileChanged.EventHandler;
            //_watcher.Filter = "*.pickup";
            //_watcher.EnableRaisingEvents = true;



            return Task.CompletedTask;
        }
    }
}
