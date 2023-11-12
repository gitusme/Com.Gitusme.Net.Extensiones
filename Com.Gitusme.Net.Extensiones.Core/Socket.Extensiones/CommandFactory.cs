using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Gitusme.Net.Extensiones.Core
{
    public abstract class CommandFactory
    {
        protected Dictionary<string, ICommand> _defaults = new Dictionary<string, ICommand>();
        protected Dictionary<string, ICommand> _users = new Dictionary<string, ICommand>();

        public CommandFactory()
        {
            Init();
        }

        protected virtual void Init() { }

        public void AddCommand(ICommand command)
        {
            AddCommand(_users, command);
        }

        protected void AddCommand(Dictionary<string, ICommand> commands, ICommand command)
        {
            commands.Add(command.GetCommand(), command);
        }

        public List<ICommand> Create()
        {
            Dictionary<string, ICommand> commands = new Dictionary<string, ICommand>(_users);
            foreach (var command in _defaults.Keys)
            {
                if (!commands.ContainsKey(command))
                {
                    commands.Add(command, _defaults[command]);
                }
            }
            return new List<ICommand>(commands.Values);
        }
    }
}
