# SimpleDirectedGraph
This is a C# representation of a Simple Directed Graph.

It uses a Dictionary collection, where a generic TNode type is the key and the value is a List of TEdge objects where TEdge implements the IEdge interface.

The graph is directed, so edges must have an origin and a destination. An edge might travel from one node to another, but have no equivalent edge back to the original node.

In order to insert new edges into the graph, a new Edge object that implements IEdge must be created. The IEdge interface guarantees that any edge must have two getters. One which gets the origin of the edge, and one which gets the destination of the edge. Currently IEdge doesn't implement any sort of data, like an edge weight, but this can be managed in the implementation of the interface.

This is an ongoing project, and I'll probably be adding some search algorithms next, so expect changes to the model as I work my way through things.

### ToDo:
  - [ ] Implement logic that forbids a node from holding edges that connect to itself.

### Possible improvements:
  * Change the AdjacencyLists from a collection of List<> to a collection of LinkedList<> if performance demands it.
