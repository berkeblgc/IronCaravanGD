using UnityEngine;
using Game.Collections;

namespace Game.Runtime
{
    [System.Serializable]
    public struct RailConnection
    {
        public Transform nodeA;
        public Transform nodeB;
    }
    public class RailNetwork : MonoBehaviour
    {
        [SerializeField] private Transform[] stations;
        [SerializeField] private RailConnection[] connections;
        private Graph<Transform> _trackGraph;
        private void Awake()
        {
            _trackGraph = new Graph<Transform>();
            foreach (var station in stations)
            {
                if (station != null)
                    _trackGraph.AddNode(station);
            }
            foreach (var connection in connections)
            {
                if (connection.nodeA != null && connection.nodeB != null)
                {
                    _trackGraph.AddEdge(connection.nodeA, connection.nodeB);
                }
            }
        }
        private void OnDrawGizmos()
        {
            if (connections == null) return;
            Gizmos.color = Color.cyan;
            foreach (var connection in connections)
            {
                if (connection.nodeA != null && connection.nodeB != null)
                {
                    Gizmos.DrawLine(connection.nodeA.position, connection.nodeB.position);
                    Gizmos.DrawSphere(connection.nodeA.position, 0.3f);
                    Gizmos.DrawSphere(connection.nodeB.position, 0.3f);
                }
            }
        }        
        public System.Collections.Generic.List<Transform> GetAvailableRoutes(Transform currentStation)
        {
            return _trackGraph.GetNeighbors(currentStation);
        }
    }
}