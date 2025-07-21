using System;
using Patterns.Singleton;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Components.Clients
{
    public class ClientManager : SingletonBehaviour<ClientManager>
    {
        [SerializeField] Sprite[] clientSprites;
        [SerializeField] GameObject clientPrefab;
        [SerializeField] Transform[] spawnPoints;
        [SerializeField] bool[] spawnPointsOccupied;
        [SerializeField] private int minWaitTimeForSpawn = 20;
        [SerializeField] private int maxWaitTimeForSpawn = 30;

        private int timeForNextClient;
        private int elapsedTime;
        
        private void Start()
        {
            spawnPointsOccupied = new bool[spawnPoints.Length];
            elapsedTime = 0;
            minWaitTimeForSpawn *= 1000; // Convert to milliseconds
            maxWaitTimeForSpawn *= 1000; // Convert to milliseconds
            timeForNextClient = Random.Range(minWaitTimeForSpawn, maxWaitTimeForSpawn);
        }
        
        public GameObject SpawnClient()
        {
            for (int i = 0; i < spawnPointsOccupied.Length; i++)
            {
                if (!spawnPointsOccupied[i])
                {
                    spawnPointsOccupied[i] = true;
                    GameObject client = Instantiate(clientPrefab, spawnPoints[i].position, Quaternion.identity);
                    client.GetComponent<Client>().Id = i; 
                    return client;
                }
            }
            
            return null;
        }
        
        public void ReleaseSpawnPoint(int index)
        {
            if (index >= 0 && index < spawnPointsOccupied.Length)
            {
                spawnPointsOccupied[index] = false;
            }
            else
            {
                Debug.LogWarning("Index out of bounds for spawn points.");
            }
        }

        private void Update()
        {
            if (elapsedTime >= timeForNextClient)
            {
                GameObject client = SpawnClient();
                if (client != null)
                {
                    client.GetComponent<SpriteRenderer>().sprite = clientSprites[Random.Range(0, clientSprites.Length)];
                    elapsedTime = 0; // Reset elapsed time after spawning a client
                    timeForNextClient = Random.Range(minWaitTimeForSpawn, maxWaitTimeForSpawn); // Set new wait time
                }
            }
            else
            {
                elapsedTime += (int)(Time.deltaTime * 1000); // Convert delta time to milliseconds
            }
            
        }
    }
}