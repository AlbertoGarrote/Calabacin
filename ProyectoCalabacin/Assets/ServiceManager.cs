using System.Collections;
using System.Collections.Generic;
using Components.KitchenComponents;
using Patterns.Singleton;
using UnityEngine;

public class ServiceManager : SingletonBehaviour<ServiceManager>
{
    [SerializeField] private Transform[] servicePoints;
    [SerializeField] private bool[] servicePointOccupied;


    public bool CanBeDelivered()
    {
        foreach (bool occupied in servicePointOccupied)
        {
            if (!occupied)
            {
                return true;
            }
        }
        return false;
    }

    public Vector3 GetDeliveryPoint(ref int id)
    {
        for (int i = 0; i < servicePointOccupied.Length; i++)
        {
            if (!servicePointOccupied[i])
            {
                servicePointOccupied[i] = true; // Mark this point as occupied
                id = i; // Assign the delivery ID to the paella container
                return servicePoints[i].position;
            }
        }
        return Vector3.zero; // No available delivery points
    }

    public void CleanDelivery(int deliveryId)
    {
        servicePointOccupied[deliveryId] = false; // Mark the delivery point as available
    }
}
