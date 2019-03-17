using System;
using System.Collections.Generic;

namespace Executive.Implementation.Input
{
    public interface IInputComponent
    {
        string Parse(ref IEnumerable<string> message);

        bool TakesRemainder { get; }
    }
}