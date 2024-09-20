using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<EnemyType> m_EnemyTypes = new List<EnemyType>();
    [SerializeField] int typeIndex = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && typeIndex<m_EnemyTypes.Count)
        {
            EnemyType chosenType = m_EnemyTypes[typeIndex];
            Instantiate(chosenType.enemyPrefab, chosenType.position, Quaternion.identity);
            typeIndex += 1;
        }
    }
}
