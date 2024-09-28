using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField] protected GameObject towerPref;
    private bool towerSpawned = false;
    public void SpawnTower()
    {
        if (!towerSpawned)
        {
            Instantiate(towerPref, transform.position, Quaternion.identity);
            towerSpawned = true;
        }
        else Debug.Log("Tower Already Spawned.");
    }
}
