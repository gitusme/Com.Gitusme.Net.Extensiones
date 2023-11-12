using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Gitusme.Net.Extensiones.Core
{
    public class CommandFilter
    {
        private CommandFactory _commandFactory;

        public CommandFilter(CommandFactory commandFactory)
        {
            this._commandFactory = commandFactory;
        }

        public ICommand Filter(string command)
        {
            return this._commandFactory.Create().Find((it) =>
            {
                return it.GetCommand().Equals(command);
            });
        }
    }
}
