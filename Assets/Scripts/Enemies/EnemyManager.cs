using System.Collections;
using System.Collections.Generic;
using Enemies.Twig;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private GameObject twigPrefab;

    [SerializeField]
    private GameObject dryadPrefab;

    [SerializeField]
    private GameObject myconidPrefab;

    private int spawnedTwigs;
    private int spawnedDryads;
    private int spawnedMyconids;

    private List<GameObject> unitSpool = new List<GameObject>();

    [SerializeField]
    private int unitLimit;

    private int aliveUnits;

    [SerializeField]
    private float timeBetweenSpawns;

    private Vector2 spawnAreaMin;
    private Vector2 spawnAreaMax;

    private int encounterTwigs;
    private int encounterDryads;
    private int encounterMyconids;

    private float twigSpawnChance;
    private float dryadSpawnChance;
    private float myconidSpawnChance;

    public void BeginEncounter(
        int twigs,
        int dryads,
        int myconids,
        int encounterUnitLimit,
        Vector2 areaMin,
        Vector2 areaMax
    )
    {
        spawnedTwigs = 0;
        spawnedDryads = 0;
        spawnedMyconids = 0;

        encounterTwigs = twigs;
        encounterDryads = dryads;
        encounterMyconids = myconids;
        unitLimit = encounterUnitLimit;

        CalculateSpawnChances();

        spawnAreaMin = areaMin;
        spawnAreaMax = areaMax;

        StartCoroutine(SpawnEnemies());
    }

    private bool CalculateSpawnChances()
    {
        int encounterTotal =
            (encounterTwigs + encounterDryads + encounterMyconids)
            - (spawnedTwigs + spawnedDryads + spawnedMyconids);
        if (encounterTotal <= 0)
        {
            return false;
        }
        twigSpawnChance = (float)(encounterTwigs - spawnedTwigs) / encounterTotal;
        dryadSpawnChance = (float)(encounterDryads - spawnedDryads) / encounterTotal;
        myconidSpawnChance = (float)(encounterMyconids - spawnedMyconids) / encounterTotal;
        return true;
    }

    private IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(timeBetweenSpawns);

        if (aliveUnits >= unitLimit)
        {
            StartCoroutine(SpawnEnemies());
            yield break;
        }

        if (!CalculateSpawnChances())
        {
            //Encounter over
            //Event
            yield break;
        }

        GameObject unitToSpawn;
        EnemyType unitType;
        int randomNumber = Random.Range(1, 101);
        if (randomNumber <= (twigSpawnChance * 100f))
        {
            //spawn twig
            unitToSpawn = twigPrefab;
            unitType = EnemyType.twig;
        }
        else if (randomNumber <= ((twigSpawnChance + dryadSpawnChance) * 100f))
        {
            //spawn dryad
            unitToSpawn = dryadPrefab;
            unitType = EnemyType.dryad;
        }
        else
        {
            //spawn myconid
            unitToSpawn = myconidPrefab;
            unitType = EnemyType.myconid;
        }

        if (unitSpool.Count < unitLimit)
        {
            GameObject spawnedUnit = Instantiate(unitToSpawn, gameObject.transform);
            spawnedUnit.transform.position = new Vector3(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y),
                0f
            );
            unitSpool.Add(spawnedUnit);

            switch (unitType)
            {
                case EnemyType.twig:
                    spawnedTwigs++;
                    break;
                case EnemyType.dryad:
                    spawnedDryads++;
                    break;
                case EnemyType.myconid:
                    spawnedMyconids++;
                    break;
            }
        }
        else
        {
            bool unitFound = false;
            int i = 0;
            while (!unitFound) { }
        }
    }
}
