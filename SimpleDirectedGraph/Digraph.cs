using System;
using System.Collections.Generic;
using System.Linq;

namespace Bluspur.Graphs
{
    /// <summary>
    /// Data representation of a simple directional graph.
    /// </summary>
    /// <typeparam name="TNode">Generic type to be used as the graph nodes.</typeparam>
    /// <typeparam name="TEdge">Generic type that represents an edge in the graph. Must inherit from IEdge.</typeparam>
    public class Digraph<TNode, TEdge> where TEdge : IEdge<TNode>
    {
        private Dictionary<TNode, List<TEdge>> _adjacencyLists = new();

        /// <summary>
        /// Returns a count of all the nodes present in the graph.
        /// </summary>
        public int NodeCount
            => _adjacencyLists.Count;

        /// <summary>
        /// Returns a count of all edges from all nodes in the graph.
        /// </summary>
        public int EdgeCount
            => _adjacencyLists.Values.Sum(x => x.Count);

        /// <summary>
        /// Adds a Node with no outgoing edges.
        /// </summary>
        public virtual void AddNode(TNode node)
        {
            if (node is null) throw new ArgumentNullException(nameof(node));
            _adjacencyLists.TryAdd(node, new List<TEdge>());
        }

        /// <summary>
        /// Adds an Edge to the graph that connects two Nodes.
        /// Any Nodes not already part of the graph are added automatically.
        /// </summary>
        public virtual void AddEdge(TEdge edge)
        {
            CheckEdgeForNullValues(edge);
            // Check if an adjacency list already exists for the Origin.
            _adjacencyLists.TryGetValue(edge.Origin, out var outgoingEdges);
            // If the list exists, then just append the new edge.
            if (outgoingEdges is not null)
                outgoingEdges.Add(edge);
            // If is doesn't exist, then make a new list containing the new edge and assign it to the relevant key.
            else
            {
                outgoingEdges = new List<TEdge> { edge };
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
            foreach (KeyValuePair<TNode, List<TEdge>> kvp in _adjacencyLists)
            {
                kvp.Value.RemoveAll(x => x.Destination.Equals(node));
            }
        }

        /// <summary>
        /// Removes an Edge from the graph based on its Origin and Destination properties.
        /// </summary>
        public virtual void TryRemoveEdge(TEdge edge)
        {
            CheckEdgeForNullValues(edge);
            _adjacencyLists.TryGetValue(edge.Origin, out var outgoingEdges);
            if (outgoingEdges is not null)
            {
                foreach (TEdge existingEdge in outgoingEdges)
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
            return outgoingEdges?.Any(edge => edge.Destination.Equals(destination)) ?? false;
        }

        /// <summary>
        /// Returns an Enumerable collection of all the Nodes in the graph.
        /// </summary>
        public virtual IEnumerable<TNode> GetNodes()
            => _adjacencyLists.Keys;

        /// <summary>
        /// Returns an Enumerable collection of all the edges leading out from the input node.
        /// </summary>
        public virtual IEnumerable<TEdge> GetOutgoingEdges(TNode node)
        {
            if (node is null) throw new ArgumentNullException(nameof(node));
            _adjacencyLists.TryGetValue(node, out var outgoingEdges);
            if (outgoingEdges is null)
                yield break;
            foreach (TEdge edge in outgoingEdges)
                yield return edge;
        }

        /// <summary>
        /// Takes an edge and checks that it and it's essential properties are NOT null. If they are throw an exception.
        /// </summary>
        protected virtual void CheckEdgeForNullValues(TEdge edge)
        {
            if (edge is null) throw new ArgumentNullException(nameof(edge));
            if (edge.Origin is null) throw new ArgumentException($"{nameof(edge.Origin)} must not be Null");
            if (edge.Destination is null) throw new ArgumentException($"{nameof(edge.Destination)} must not be Null");
        }
    }
}