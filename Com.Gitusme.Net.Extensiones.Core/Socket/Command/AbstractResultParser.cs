/*********************************************************
 * Copyright (c) 2023-2024 gitusme, All rights reserved.
 *********************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Gitusme.Net.Extensiones.Core
{
    public abstract class AbstractResultParser : IResultParser
    {
        public abstract ICommandResult Parse(byte[] result);
    }
}
