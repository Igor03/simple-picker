﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace JIgor.Projects.SimplePicker.Engine.Exceptions
{
    public class EmptyListException : Exception
    {
        public EmptyListException()
        {
        }

        public EmptyListException([NotNull] string message)
            : base(message)
        {
        }

        public EmptyListException([NotNull] string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
