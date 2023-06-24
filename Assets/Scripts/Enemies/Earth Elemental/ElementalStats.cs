using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalStats : MonoBehaviour
{
    public int health;
    public float encounterStartRange;
    public float cooldownTime;
    public float dryadSpawnChance;

    [Header("Quake")]
    public float quakeChance;
    public float quakeDamage;
    public float quakeRange;

    [Header("Rock Fall")]
    public float rockFallChance;
    public int rockFallDamage;
    public float rockFallArea;
    public float rockFallNumber;
}
