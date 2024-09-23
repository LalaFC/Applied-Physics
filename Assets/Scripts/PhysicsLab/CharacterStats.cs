using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float atkPower;
    [SerializeField] private float defense;
    [SerializeField] private float speed;

    public void SetStats(float hp, float atkPt, float def, float spd)
    {
        health = hp;
        atkPower = atkPt;
        defense = def;
        speed = spd;
        Debug.Log("Stats changed.");
    }
}
