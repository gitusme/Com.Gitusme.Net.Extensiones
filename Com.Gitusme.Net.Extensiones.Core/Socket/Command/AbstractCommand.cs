/*********************************************************
 * Copyright (c) 2023-2023 gitusme, All rights reserved.
 *********************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Gitusme.Net.Extensiones.Core
{
    /// <summary>
    /// 抽象命令
    /// </summary>
    public abstract class AbstractCommand : ICommand
    {
        public ICommandResult Send(ISocketHandler socketHandler)
        {
            return socketHandler.Send(this);
        }

        public abstract string GetCommand();

        public virtual IResultParser GetResultParser()
        {
            return new DefaultResultParser();
        }
    }
}
