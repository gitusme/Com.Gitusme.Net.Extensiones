/*********************************************************
 * Copyright (c) 2019-2023 gitusme, All rights reserved.
 *********************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Gitusme.Net.Extensiones.Core
{
    /// <summary>
    /// 命令过滤器
    /// </summary>
    public class CommandFilter
    {
        private CommandFactory _commandFactory;

        public CommandFilter(CommandFactory commandFactory)
        {
            this._commandFactory = commandFactory;
        }

        /// <summary>
        /// 从命令工厂中找出指定的命令
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public ICommand Filter(string command)
        {
            return this._commandFactory.Create().Find((it) =>
            {
                return it.GetCommand().Equals(command);
            });
        }
    }
}
