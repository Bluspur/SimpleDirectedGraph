using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Bluspur.Graphs.Tests
{
    /// <summary>
    /// Tests Digraph methods when using a value type (int) as the node type
    /// </summary>
    [TestClass]
    public class DigraphValueNodeTests
    {
        [TestMethod()]
        public void AddEdgeTest()
        {
            Edge edge = new(0, 1);
            Digraph<int, Edge> digraph = new();
            digraph.AddEdge(edge);
            Assert.IsTrue(digraph.ContainsEdge(0, 1));
        }

        [TestMethod()]
        public void AddNodeTest()
        {
            int newNode = 0;
            Digraph<int, Edge> digraph = new();
            digraph.AddNode(newNode);
            Assert.IsTrue(digraph.ContainsNode(newNode));
        }

        [TestMethod()]
        public void GetNodesTest()
        {
            int nodeOne = 0;
            int nodeTwo = 1;
            int nodeThree = 2;
            Digraph<int, Edge> digraph = new();
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
            int node = 0;
            Edge edgeOne = new(node, 1);
            Edge edgeTwo = new(node, 2);
            Edge edgeThree = new(node, 3);
            Digraph<int, Edge> digraph = new();
            digraph.AddEdge(edgeOne);
            digraph.AddEdge(edgeTwo);
            digraph.AddEdge(edgeThree);

            var edges = digraph.GetOutgoingEdges(node).ToList();

            CollectionAssert.Contains(edges, edgeOne);
            CollectionAssert.Contains(edges, edgeTwo);
            CollectionAssert.Contains(edges, edgeThree);
        }

        [TestMethod()]
        public void TryRemoveEdgeTest()
        {
            int node = 0;
            Edge edge = new(node, 1);
            Digraph<int, Edge> digraph = new();
            digraph.AddEdge(edge);

            digraph.TryRemoveEdge(edge);

            var outgoingEdges = digraph.GetOutgoingEdges(node).ToList();
            CollectionAssert.DoesNotContain(outgoingEdges, edge);
        }

        [TestMethod()]
        public void TryRemoveNodeTest()
        {
            int node = 0;
            Digraph<int, Edge> digraph = new();
            digraph.AddNode(node);

            digraph.TryRemoveNode(node);

            Assert.IsFalse(digraph.ContainsNode(node));
        }

        [TestMethod()]
        public void ContainsEdgeTest()
        {
            int origin = 0;
            int destination = 1;
            Edge edge = new(origin, destination);
            Digraph<int, Edge> digraph = new();
            digraph.AddEdge(edge);

            Assert.IsTrue(digraph.ContainsEdge(origin, destination));
        }

        public class Edge : IEdge<int>
        {
            private int origin, destination;

            public int Origin => origin;

            public int Destination => destination;

            public Edge(int origin, int destination)
            {
                this.origin = origin;
                this.destination = destination;
            }
        }

    }
}