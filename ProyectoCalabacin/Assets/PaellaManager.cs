using System;
using System.Collections;
using System.Collections.Generic;
using Components.KitchenComponents;
using Patterns.Singleton;
using UnityEngine;

public class PaellaManager : SingletonBehaviour<PaellaManager>
{
    [SerializeField] private GameObject paellaPrefab;
    [SerializeField] private Transform[] paellaSpawnPoints;
    
    public void CreatePaella(int id)
    {
        Transform spawnPoint = paellaSpawnPoints[id];
        
        GameObject paellaInstance = Instantiate(paellaPrefab, spawnPoint.position, Quaternion.identity);
        PaellaContainer paellaContainer = paellaInstance.GetComponent<PaellaContainer>();
        if (paellaContainer != null)
        {
            paellaContainer.Id = id;
        }
    }

    public void Start()
    {
        for (int i = 0; i < paellaSpawnPoints.Length; i++)
        {
            CreatePaella(i);
        }
    }
}
