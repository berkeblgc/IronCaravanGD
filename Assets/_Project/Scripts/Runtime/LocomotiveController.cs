using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Game.Runtime
{
    public class LocomotiveController : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;
        [SerializeField] private RailNetwork network;
        [SerializeField] private Transform startingStation;

        private Transform _previousStation;
        private Transform _currentStation;
        private Transform _targetStation;

        private int _preferredDirection = 0;

        private void Start()
        {
            _currentStation = startingStation;
            transform.position = _currentStation.position;
            FindNextStation();
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                _preferredDirection = -1;
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                _preferredDirection = 1;
            if (_targetStation == null) return;

            transform.position = Vector3.MoveTowards(transform.position, _targetStation.position, speed * Time.deltaTime);

            Vector3 direction = (_targetStation.position - transform.position).normalized;

            if (direction != Vector3.zero) transform.forward = direction;

            if (Vector3.Distance(transform.position, _targetStation.position) < 0.1f)
            {
                _previousStation = _currentStation;
                _currentStation = _targetStation;
                FindNextStation();
            }
        }
        private void FindNextStation()
        {
            List<Transform> allRoutes = network.GetAvailableRoutes(_currentStation);
            List<Transform> validRoutes = new List<Transform>();

            foreach (var route in allRoutes)
            {
                if (route != _previousStation) validRoutes.Add(route);
            }

            if (validRoutes.Count == 0)
            {
                if (allRoutes.Count > 0) _targetStation = allRoutes[0];
                else _targetStation = null;
                return;
            }
            if (validRoutes.Count == 1)
            {
                _targetStation = validRoutes[0];
                _preferredDirection = 0;
                return;
            }
            Transform chosenRoute = validRoutes[0];
            float bestAngle = 0f;

            foreach (var route in validRoutes)
            {
                Vector3 dirToRoute = (route.position - _currentStation.position).normalized;
                float angle = Vector3.SignedAngle(transform.forward, dirToRoute, Vector3.up);

                if (_preferredDirection == -1 && angle < bestAngle)
                {
                    bestAngle = angle;
                    chosenRoute = route;
                }
                else if (_preferredDirection == 1 && angle > bestAngle)
                {
                    bestAngle = angle;
                    chosenRoute = route;
                }
            }
            _targetStation = chosenRoute;
            _preferredDirection = 0;
        }
    }
}