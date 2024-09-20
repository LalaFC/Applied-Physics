using UnityEngine;

[CreateAssetMenu(fileName = "Scriptables", menuName = "EnemyStat")]
public class EnemyType : ScriptableObject
{
    public GameObject enemyPrefab;

    public string name;
    public float attackPower;
    public float defense;
    public float health;
    public float speed;
    public Vector3 position;
}

