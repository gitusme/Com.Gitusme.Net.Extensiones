using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Gitusme.Net.Extensiones.Core;

namespace Com.Gitusme.Net.Extensiones.Demos
{
    internal class LoggerDemo : IExtensionesDemo
    {
        public override void Execute()
        {
            base.Execute();

            var server = new MyServer();
            server.Start("arg1", "arg2");
            server.Stop();
        }
    }

    class MyServer
    {
        public void Start(string arg1, string arg2)
        {
            this.Logi("MyServer", "Start:args={0},{1}", arg1, arg2);
        }

        public void Stop()
        {
            this.Logi("Stop.....");
        }
    }
}
