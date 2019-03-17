using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Executive.API;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Action = Executive.Implementation.Action;

namespace Executive.Implementation
{
    /// <summary>
    /// The class that actually does the heavy lifting sets up the global execution environment and executes te actions
    /// sourcecode.
    /// </summary>
    public class ActionExecutor
    {

        /// <summary>
        /// The assemblies referenced by this project.
        /// No other assemblies can be accessed.
        /// </summary>
        private readonly IEnumerable<Assembly> _referencedAssemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(assembly => !assembly.IsDynamic)
            .Where(assembly => assembly.Location != null && assembly.Location.Any())
            .ToList().AsEnumerable();

        public async Task Execute(Action action, IExecutiveInteractionHandler handler, IEnumerable<string> parameters, CancellationToken token)
        {
            //TODO: Add default imports.
            var options = ScriptOptions.Default
                .AddReferences(this._referencedAssemblies);

            var script = CSharpScript.Create(action.Source, options, typeof(ActionParameters));
            var errors = script.Compile();

            //TODO: Log this somehow.It should not happen since the sourcecode is checked on upload.
            if (errors.Any())
                return;

            await script.RunAsync(new ActionParameters(handler, parameters), (ex) =>
            {
                handler.Reply("Failed to process command: " + ex.Message);
                return true;
            }, token);
        }
    }
}