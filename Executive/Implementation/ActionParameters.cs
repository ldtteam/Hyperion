using System.Collections.Generic;
using Executive.API;

namespace Executive.Implementation
{
    public class ActionParameters
    {
        public ActionParameters(IExecutiveInteractionHandler handler, IEnumerable<string> parameters)
        {
            Handler = handler;
            Parameters = parameters;
        }

        public IExecutiveInteractionHandler Handler { get; }

        public IEnumerable<string> Parameters { get; }
    }
}