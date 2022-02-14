using System;
using System.Collections.Generic;

namespace Bluspur.Graphs
{
    /// <summary>
    /// Data representation of a simple directional graph.
    /// </summary>
    /// <typeparam name="TNode">Generic type to be used as the graph nodes.</typeparam>
    /// <typeparam name="TEdge">Generic type that represents an edge in the graph. Must inherit from IEdge.</typeparam>
    public class Digraph<TNode, TEdge> where TNode : notnull where TEdge : IEdge<TNode>
    {
        private Dictionary<TNode, List<IEdge<TNode>>> _adjacencyLists = new();

        /// <summary>
        /// Returns a count of all the nodes present in the graph.
        /// </summary>
        public int NodeCount
            => _adjacencyLists.Count;

        /// <summary>
        /// Returns a count of all edges from all nodes in the graph.
        /// </summary>
        public int EdgeCount
        {
            get
            {
                int count = 0;

                foreach (KeyValuePair<TNode, List<IEdge<TNode>>> kvp in _adjacencyLists)
                {
                    count += kvp.Value.Count;
                }

                return count;
            }
        }

        /// <summary>
        /// Adds a Node with no outgoing edges.
        /// </summary>
        public virtual void AddNode(TNode node)
        {
            if (node is null) throw new ArgumentNullException(nameof(node));
            _adjacencyLists.TryAdd(node, new List<IEdge<TNode>>());
        }

        /// <summary>
        /// Adds an Edge to the graph that connects two Nodes.
        /// Any Nodes not already part of the graph are added automatically.
        /// </summary>
        public virtual void AddEdge(IEdge<TNode> edge)
        {
            // TODO: Implement Exception handling for when either the Origin or Destination of an Edge is false.
            // TODO: Implement Exception handling for when the Origin and Destination of an Edge are the same.
            if (edge is null) throw new ArgumentNullException(nameof(edge));
            // Check if an adjacency list already exists for the Origin.
            _adjacencyLists.TryGetValue(edge.Origin, out var outgoingEdges);
            // If the list exists, then just append the new edge.
            if (outgoingEdges is not null)
                outgoingEdges.Add(edge);
            // If is doesn't exist, then make a new list containing the new edge and assign it to the relevant key.
            else
            {
                outgoingEdges = new List<IEdge<TNode>> { edge };
                _adjacencyLists[edge.Origin] = outgoingEdges;
            }
            // If the destination Node is not already present in the graph, then add it.
            if (!_adjacencyLists.ContainsKey(edge.Destination))
                AddNode(edge.Destination);
        }

        /// <summary>
        /// Removes a node object from the graph and any related edges.
        /// </summary>
        public virtual void TryRemoveNode(TNode node)
        {
            if (node is null) throw new ArgumentNullException(nameof(node));
            // Remove the node from the adjacencyLists.
            bool nodePresent = _adjacencyLists.Remove(node);
            // If nodePresent is false it was probably never in the graph and we can return.
            if (!nodePresent) return;
            // We need to check all the outgoing edges of other nodes and then remove edges leading to this node.
            // Probably quite slow, do some performance tests.
            foreach (KeyValuePair<TNode, List<IEdge<TNode>>> kvp in _adjacencyLists)
            {
                kvp.Value.RemoveAll(x => x.Destination.Equals(node));
            }
        }

        /// <summary>
        /// Removes an Edge from the graph based on its Origin and Destination properties.
        /// </summary>
        public virtual void TryRemoveEdge(IEdge<TNode> edge)
        {
            if (edge is null) throw new ArgumentNullException(nameof(edge));
            _adjacencyLists.TryGetValue(edge.Origin, out var outgoingEdges);
            if (outgoingEdges is not null)
            {
                foreach (IEdge<TNode> existingEdge in outgoingEdges)
                {
                    if (existingEdge.Equals(edge))
                    {
                        outgoingEdges.Remove(existingEdge);
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Checks if the input Node is a part of the graph.
        /// </summary>
        public virtual bool ContainsNode(TNode node)
            => _adjacencyLists.ContainsKey(node);

        /// <summary>
        /// Checks if an edge exists from the Origin node to the Destination node.
        /// </summary>
        public virtual bool ContainsEdge(TNode origin, TNode destination)
        {
            if (origin is null) throw new ArgumentNullException(nameof(origin));
            if (destination is null) throw new ArgumentNullException(nameof(destination));
            _adjacencyLists.TryGetValue(origin, out var outgoingEdges);
            if (outgoingEdges is not null)
            {
                foreach (var edge in outgoingEdges)
                {
                    if (edge.Destination.Equals(destination))
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Returns an Enumerable collection of all the Nodes in the graph.
        /// </summary>
        public virtual IEnumerable<TNode> GetNodes()
            => _adjacencyLists.Keys;

        /// <summary>
        /// Returns an Enumerable collection of all the edges leading out from the input node.
        /// </summary>
        public virtual IEnumerable<IEdge<TNode>> GetOutgoingEdges(TNode node)
        {
            if (node is null) throw new ArgumentNullException(nameof(node));
            _adjacencyLists.TryGetValue(node, out var outgoingEdges);
            if (outgoingEdges is null)
                yield break;
            foreach (IEdge<TNode> edge in outgoingEdges)
                yield return edge;
        }
    }
}
