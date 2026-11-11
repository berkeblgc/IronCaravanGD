using UnityEngine;
using Game.Collections;

namespace Game.RunTime
{
    public class TrainManager : MonoBehaviour
    {
        [SerializeField] private float spacing = 2f;
        [SerializeField] private Transform locomotive;
        [SerializeField] private GameObject wagonPrefab;
        
        private DoublyLinkedList<Transform> _trainChain;
        private void Awake()
        {
            _trainChain = new DoublyLinkedList<Transform>();
            _trainChain.AddLast(locomotive);
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                AddNewWagon();
            }
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                RemoveLastWagon();
            }
        }
        private void AddNewWagon()
        {
            GameObject newWagon = Instantiate(wagonPrefab, locomotive.position, locomotive.rotation);
            _trainChain.AddLast(newWagon.transform);
        }
        private void RemoveLastWagon()
        {
            if (_trainChain.Count <= 1) return;
            Transform lastWagonTransform = _trainChain.Tail.Value;
            _trainChain.Remove(_trainChain.Tail);
            Destroy(lastWagonTransform.gameObject);
        }
        private void LateUpdate()
        {
            for (var n = _trainChain.Head.Next; n != null; n = n.Next)
            {
                Transform currentWagon = n.Value;
                Transform wagonAhead = n.Prev.Value;
                
                Vector3 targetPosition = wagonAhead.position - (wagonAhead.forward * spacing);
                
                currentWagon.position = targetPosition;
                currentWagon.rotation = wagonAhead.rotation;
            }
        }
    }
}