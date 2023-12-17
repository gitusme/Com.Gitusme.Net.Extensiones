/*********************************************************
 * Copyright (c) 2023-2024 gitusme, All rights reserved.
 *********************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Gitusme.Net.Extensiones.Core
{
    public class DefaultCommandResult : ICommandResult
    {
        private byte[] _bytes;

        public DefaultCommandResult(byte[] bytes)
        {
            this._bytes = bytes;
        }

        public byte[] Get()
        {
            return this._bytes;
        }
    }
}
