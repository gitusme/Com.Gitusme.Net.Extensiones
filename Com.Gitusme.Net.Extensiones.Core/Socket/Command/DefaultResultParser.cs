/*********************************************************
 * Copyright (c) 2023-2024 gitusme, All rights reserved.
 *********************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Gitusme.Net.Extensiones.Core
{
    public class DefaultResultParser : AbstractResultParser
    {
        public override ICommandResult Parse(byte[] bytes)
        {
            return new DefaultCommandResult(bytes);
        }
    }
}
