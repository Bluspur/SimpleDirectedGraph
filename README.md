# SimpleDirectedGraph
.NET generic representation of a Simple Directed Graph

This is a C# representation of a Directed Simple Graph.
It uses a Dictionary collection, where a generic TNode type is the key and the value is a List of TEdge objects where TEdge implements the IEdge interface.
The graph is directed, so edges are not inherently bidirectional and will need to be added to both nodes if that behaviour is desired.
~~The graph is simple, so a node cannot contain edges that lead to itself~~ This isn't implemented yet.

Possible improvements:
  Change the AdjacencyLists from a collection of List<> to a collection of LinkedList<> if performance demands it.
