using UnityEngine;
using Game.Collections;
using UnityEditor.Experimental.GraphView;
namespace Game.Runtime
{
    public class Station : MonoBehaviour
    {
        [SerializeField] private string stationName;
        [SerializeField] private int maxCapacity = 5;

        private RingQueue<string> _passengerQueue;

        private void Awake()
        {
            _passengerQueue = new RingQueue<string>(maxCapacity);
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                AddPassengerToQueue("Yolcu_" + Random.Range(100, 999));
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                BoardPassengerToTrain();
            }
        }
        private void AddPassengerToQueue(string passengerName)
        {
            if (_passengerQueue.Count < maxCapacity)
            {
                _passengerQueue.Enqueue(passengerName);
                Debug.Log($"[İSTASYON - {stationName}] {passengerName} kuyruğa girdi. Sırada bekleyen kişi sayısı: {_passengerQueue.Count}");
            }
            else
            {
                Debug.LogWarning($"[İSTASYON - {stationName}] Kapasite dolu! Peronda yer yok, {passengerName} kuyruğa girmedi.");
            }
        }
        private void BoardPassengerToTrain()
        {
            if (_passengerQueue.Count > 0)
            {
                string boardedPassenger = _passengerQueue.Dequeue();
                Debug.Log($"[TREN - {stationName}] {boardedPassenger} trene bindi. Sırada kalan kişi sayısı: {_passengerQueue.Count}");
            }
            else
            {
                Debug.LogWarning($"[TREN - {stationName}] İstasyon boş, sıradan alınıp trene bindirilecek kimse yok!");
            }
        }
    }
}