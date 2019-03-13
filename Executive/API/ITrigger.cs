namespace Executive.API
{
    /// <summary>
    /// Represents a trigger that triggers a given action.
    /// </summary>
    public interface ITrigger
    {
        /// <summary>
        /// The action that a given trigger triggers.
        /// </summary>
        IAction Action { get; }
    }
}