namespace Bluspur.Graphs
{
    /// <summary>
    /// Interface that guarantees an Edge object has both an Origin and a Destination node.
    /// </summary>
    /// <typeparam name="TNode">The node type of the edge's parent graph.</typeparam>
    public interface IEdge<TNode>
    {
        /// <summary>
        /// The Node from which an edge starts.
        /// </summary>
        public TNode Origin { get; }
        /// <summary>
        /// The Node at which the edge ends.
        /// </summary>
        public TNode Destination { get; }
    }
}
