using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Gitusme.Net.Extensiones.Core
{
    public class DefaultCommandFactory : CommandFactory
    {
        public DefaultCommandFactory()
            : base()
        {
        }

        protected override void Init()
        {
            AddCommand(_defaults, new ACK());
        }
    }
}
