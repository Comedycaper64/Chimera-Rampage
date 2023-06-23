using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEncounter", menuName = "Chimera Rampage/EnemyEncounter", order = 0)]
public class EnemyEncounter : ScriptableObject
{
    public int twigs;
    public int dryads;
    public int myconids;
    public int encounterUnitLimit;
    public Vector3 areaMin;
    public Vector3 areaMax;
    public int encounterNumber;
}
