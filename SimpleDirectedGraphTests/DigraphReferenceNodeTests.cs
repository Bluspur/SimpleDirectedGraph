using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Bluspur.Graphs.Tests
{
    /// <summary>
    /// Tests Digraph methods when using a reference type as Node type.
    /// </summary>
    [TestClass]
    public class DigraphReferenceNodeTests
    {
        [TestMethod()]
        public void AddEdge_ShouldThrowExceptionWhenEdgeIsNull()
        {
            ReferenceNode originNode = new("origin", 0);
            ReferenceNode destinationNode = null;
            Edge edge = new(originNode, destinationNode);
            Digraph<ReferenceNode, Edge> graph = new();

            Assert.ThrowsException<ArgumentException>(() => graph.AddEdge(edge));
        }

        [TestMethod()]
        public void AddEdge_ShouldThrowExceptionWhenEdgePropertyIsNull()
        {
            Edge edge = null;
            Digraph<ReferenceNode, Edge> graph = new();

            Assert.ThrowsException<ArgumentNullException>(() => graph.AddEdge(edge));
        }

        [TestMethod()]
        public void AddEdgeTest()
        {
            ReferenceNode originNode = new("origin", 0);
            ReferenceNode destinationNode = new("destination", 1);
            Edge edge = new(originNode, destinationNode);
            Digraph<ReferenceNode, Edge> digraph = new();
            digraph.AddEdge(edge);
            Assert.IsTrue(digraph.ContainsEdge(originNode, destinationNode));
        }

        [TestMethod()]
        public void AddNodeTest_ShouldThrowExceptionWhenNodeIsNull()
        {
            ReferenceNode nullNode = null;
            Digraph<ReferenceNode, Edge> graph = new();
            Assert.ThrowsException<ArgumentNullException>(() => graph.AddNode(nullNode));
        }

        [TestMethod()]
        public void AddNodeTest()
        {
            ReferenceNode newNode = new("newNode", 0);
            Digraph<ReferenceNode, Edge> graph = new();

            graph.AddNode(newNode);

            Assert.IsTrue(graph.ContainsNode(newNode));
        }

        [TestMethod()]
        public void GetNodesTest()
        {
            ReferenceNode nodeOne = new("nodeOne", 0);
            ReferenceNode nodeTwo = new("nodeTwo", 1);
            ReferenceNode nodeThree = new("nodeThree", 2);
            Digraph<ReferenceNode, Edge> digraph = new();
            digraph.AddNode(nodeOne);
            digraph.AddNode(nodeTwo);
            digraph.AddNode(nodeThree);

            var nodes = digraph.GetNodes().ToList();

            CollectionAssert.Contains(nodes, nodeOne);
            CollectionAssert.Contains(nodes, nodeTwo);
            CollectionAssert.Contains(nodes, nodeThree);
        }

        [TestMethod()]
        public void GetOutgoingEdgesTest()
        {
            ReferenceNode nodeOne = new("nodeOne", 0);
            ReferenceNode nodeTwo = new("nodeTwo", 1);
            ReferenceNode nodeThree = new("nodeThree", 2);
            ReferenceNode nodeFour = new("nodeFour", 3);
            Edge edgeOne = new(nodeOne, nodeTwo);
            Edge edgeTwo = new(nodeOne, nodeThree);
            Edge edgeThree = new(nodeOne, nodeFour);
            Digraph<ReferenceNode, Edge> digraph = new();
            digraph.AddEdge(edgeOne);
            digraph.AddEdge(edgeTwo);
            digraph.AddEdge(edgeThree);

            var edges = digraph.GetOutgoingEdges(nodeOne).ToList();

            CollectionAssert.Contains(edges, edgeOne);
            CollectionAssert.Contains(edges, edgeTwo);
            CollectionAssert.Contains(edges, edgeThree);
        }

        [TestMethod()]
        public void TryRemoveEdgeTest_ShouldThrowExceptionWhenEdgeIsNull()
        {
            Edge edge = null;
            Digraph<ReferenceNode, Edge> graph = new();

            Assert.ThrowsException<ArgumentNullException>(() => graph.TryRemoveEdge(edge));
        }

        [TestMethod()]
        public void TryRemoveEdgeTest_ShouldThrowExceptionWhenEdgePropertyIsNull()
        {
            ReferenceNode originNode = new("origin", 0);
            ReferenceNode destinationNode = null;
            Edge edge = new(originNode, destinationNode);
            Digraph<ReferenceNode, Edge> graph = new();

            Assert.ThrowsException<ArgumentException>(() => graph.TryRemoveEdge(edge));
        }

        [TestMethod()]
        public void TryRemoveEdgeTest()
        {
            ReferenceNode nodeOne = new("nodeOne", 0);
            ReferenceNode nodeTwo = new("nodeTwo", 1);
            Edge edge = new(nodeOne, nodeTwo);
            Digraph<ReferenceNode, Edge> digraph = new();
            digraph.AddEdge(edge);

            digraph.TryRemoveEdge(edge);

            var outgoingEdges = digraph.GetOutgoingEdges(nodeOne).ToList();
            CollectionAssert.DoesNotContain(outgoingEdges, edge);
        }

        [TestMethod()]
        public void TryRemoveNodeTest_ShouldThrowExceptionWhenNodeIsNull()
        {
            ReferenceNode nullNode = null;
            Digraph<ReferenceNode, Edge> graph = new();
            Assert.ThrowsException<ArgumentNullException>(() => graph.TryRemoveNode(nullNode));
        }

        [TestMethod()]
        public void TryRemoveNodeTest()
        {
            ReferenceNode nodeOne = new("nodeOne", 0);
            Digraph<ReferenceNode, Edge> digraph = new();
            digraph.AddNode(nodeOne);

            digraph.TryRemoveNode(nodeOne);

            Assert.IsFalse(digraph.ContainsNode(nodeOne));
        }

        [TestMethod()]
        public void ContainsEdgeTest()
        {
            ReferenceNode nodeOne = new("nodeOne", 0);
            ReferenceNode nodeTwo = new("nodeTwo", 1);
            Edge edge = new(nodeOne, nodeTwo);
            Digraph<ReferenceNode, Edge> digraph = new();
            digraph.AddEdge(edge);

            Assert.IsTrue(digraph.ContainsEdge(nodeOne, nodeTwo));
        }

        public class Edge : IEdge<ReferenceNode>
        {
            private ReferenceNode origin, destination;

            public ReferenceNode Origin => origin;

            public ReferenceNode Destination => destination;

            public Edge(ReferenceNode origin, ReferenceNode destination)
            {
                this.origin = origin;
                this.destination = destination;
            }
        }

        public class ReferenceNode
        {
            public string Name { get; }
            public int Number { get; }

            public ReferenceNode(string name, int number)
            {
                Name = name;
                Number = number;
            }
        }
    }
}