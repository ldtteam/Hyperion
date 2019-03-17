using System.Collections.Generic;
using System.Linq;

namespace Executive.Implementation.Input
{
    public class InputDefinition
    {
        public InputDefinition()
        {
            Components = new List<IInputComponent>();
        }

        public InputDefinition(IEnumerable<IInputComponent> components)
        {
            Components = components;
        }

        public IEnumerable<IInputComponent> Components { get; set; }

        public IEnumerable<string> Parse(IEnumerable<string> messageComponents)
        {
            var forwardsResolvers = this.Components.TakeWhile(c => !c.TakesRemainder);
            var reverseResolvers = this.Components.Reverse().TakeWhile(c => !c.TakesRemainder);
            var takesRemainder = this.Components.FirstOrDefault(c => c.TakesRemainder);
            
            var parameters = forwardsResolvers.Select(forwardsResolver => forwardsResolver.Parse(ref messageComponents)).ToList();

            var reverseRemainingMessageComponents = messageComponents.Reverse();
            parameters.AddRange(reverseResolvers.Select(reverseResolver => reverseResolver.Parse(ref reverseRemainingMessageComponents)));
            
            if (takesRemainder != null)
                parameters.Add(takesRemainder.Parse(ref messageComponents));

            return parameters;
        }
    }
}