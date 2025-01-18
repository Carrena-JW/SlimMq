using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimMq
{
    internal static class Publiser
    {
        //namming
        internal static Task PublishAsync<T>(string tempFolderPath, string folderPath, object body)
        {
            var messageTypeName = typeof(T).Name;



            return Task.CompletedTask;
        }
    }
}
