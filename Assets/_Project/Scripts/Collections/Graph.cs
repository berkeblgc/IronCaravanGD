using System.Collections.Generic;
namespace Game.Collections
{
    public sealed class Graph<T>
    {
        private readonly Dictionary<T, List<T>> _adjacencyList;
        public Graph()
        {
            _adjacencyList = new Dictionary<T, List<T>>();
        }
        public void AddNode(T node)
        {
            if (!_adjacencyList.ContainsKey(node))
            {
                _adjacencyList[node] = new List<T>();
            }
        }
        public void AddEdge(T fromNode, T toNode, bool isDirected = false)
        {
            if (!_adjacencyList.ContainsKey(fromNode)) AddNode(fromNode);
            if (!_adjacencyList.ContainsKey(toNode)) AddNode(toNode);
            _adjacencyList[fromNode].Add(toNode);
            if (!isDirected)
            {
                _adjacencyList[toNode].Add(fromNode);
            }
        }
        public List<T> GetNeighbors(T node)
        {
            if (_adjacencyList.ContainsKey(node))
            {
                return _adjacencyList[node];
            }
            return new List<T>();
        }
    }
}