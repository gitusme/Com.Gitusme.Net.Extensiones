using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Gitusme.Net.Extensiones.Demos
{
    internal abstract class IExtensionesDemo
    {
        public string Name
        {
            get
            {
                return GetType().Name;
            }
        }

        public virtual void Execute()
        {
            System.Console.WriteLine("\r\n========== " + Name + " ==========");
        }
    }
}
