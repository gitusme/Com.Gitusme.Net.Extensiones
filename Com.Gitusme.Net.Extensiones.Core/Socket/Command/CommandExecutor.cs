/*********************************************************
 * Copyright (c) 2023-2023 gitusme, All rights reserved.
 *********************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Gitusme.Net.Extensiones.Core
{
    public class CommandExecutor
    {
        private ISocketHandler _socketHandler;
        public CommandExecutor(ISocketHandler socketHandler)
        {
            this._socketHandler = socketHandler;
        }

        public ICommandResult Execute(ICommand command)
        {
            return command.Send(this._socketHandler);
        }
    }
}
