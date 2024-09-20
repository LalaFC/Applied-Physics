using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    private float health;
    private float atkPower;
    private float defense;
    private float speed;

    public void SetStats(float hp, float atkPt, float def, float spd)
    {
        health = hp;
        atkPower = atkPt;
        defense = def;
        speed = spd;
    }
}
