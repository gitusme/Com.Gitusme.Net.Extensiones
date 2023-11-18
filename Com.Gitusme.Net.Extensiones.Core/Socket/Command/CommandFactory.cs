/*********************************************************
 * Copyright (c) 2019-2023 gitusme, All rights reserved.
 *********************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Gitusme.Net.Extensiones.Core
{
    /// <summary>
    /// 抽象命令工厂
    /// </summary>
    public abstract class CommandFactory
    {
        /// <summary>
        /// 内置命令集
        /// </summary>
        protected Dictionary<string, ICommand> _defaults = new Dictionary<string, ICommand>();
        /// <summary>
        /// 自定义命令集
        /// </summary>
        protected Dictionary<string, ICommand> _users = new Dictionary<string, ICommand>();

        public CommandFactory()
        {
            Init();
        }

        /// <summary>
        /// 初始化工厂
        /// </summary>
        protected virtual void Init() { }

        /// <summary>
        /// 添加自定义命令
        /// </summary>
        /// <param name="command"></param>
        public void AddUserCommand(ICommand command)
        {
            AddCommand(_users, command);
        }

        protected void AddCommand(Dictionary<string, ICommand> commands, ICommand command)
        {
            commands.Add(command.GetCommand(), command);
        }

        /// <summary>
        /// 生成命令集
        /// </summary>
        /// <returns></returns>
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
