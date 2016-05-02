﻿////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// <copyright>Copyright 2012-2016 Lawo AG (http://www.lawo.com).</copyright>
// Distributed under the Boost Software License, Version 1.0.
// (See accompanying file LICENSE_1_0.txt or copy at http://www.boost.org/LICENSE_1_0.txt)
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Lawo.GlowAnalyzerProxy.Main
{
    using System;
    using System.Net.Sockets;

    internal sealed class ListenFailedEventArgs : EventArgs
    {
        internal ListenFailedEventArgs(SocketException exception)
        {
            this.exception = exception;
        }

        internal SocketException Exception
        {
            get { return this.exception; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private readonly SocketException exception;
    }
}
