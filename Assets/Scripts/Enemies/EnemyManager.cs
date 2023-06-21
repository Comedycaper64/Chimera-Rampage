using System.Collections;
using System.Collections.Generic;
using Enemies.Dryad;
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

    [SerializeField]
    private EnemyEncounter testEncounter;

    private void Start()
    {
        TwigStateMachine.OnAnyEnemyDeath += OnAnyEnemyDeath;
        DryadStateMachine.OnAnyEnemyDeath += OnAnyEnemyDeath;
        BeginEncounter(testEncounter);
    }

    private void OnDisable()
    {
        TwigStateMachine.OnAnyEnemyDeath -= OnAnyEnemyDeath;
        DryadStateMachine.OnAnyEnemyDeath -= OnAnyEnemyDeath;
    }

    public void BeginEncounter(EnemyEncounter encounter)
    {
        spawnedTwigs = 0;
        spawnedDryads = 0;
        spawnedMyconids = 0;

        encounterTwigs = encounter.twigs;
        encounterDryads = encounter.dryads;
        encounterMyconids = encounter.myconids;
        unitLimit = encounter.encounterUnitLimit;

        CalculateSpawnChances();

        spawnAreaMin = encounter.areaMin;
        spawnAreaMax = encounter.areaMax;

        StartCoroutine(SpawnEnemies());
    }

    private bool CalculateSpawnChances()
    {
        int encounterTotal =
            (encounterTwigs + encounterDryads + encounterMyconids)
            - (spawnedTwigs + spawnedDryads + spawnedMyconids);

        //Debug.Log(encounterTotal);
        if (encounterTotal <= 0)
        {
            return false;
        }
        twigSpawnChance = (float)(encounterTwigs - spawnedTwigs) / encounterTotal;
        dryadSpawnChance = (float)(encounterDryads - spawnedDryads) / encounterTotal;
        myconidSpawnChance = (float)(encounterMyconids - spawnedMyconids) / encounterTotal;
        return true;
    }

    private void SpawnUnit(GameObject unitToSpawn, EnemyType unitType)
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

        //Debug.Log("Spawn New Unit");
    }

    private IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(timeBetweenSpawns);

        if (aliveUnits >= unitLimit)
        {
            StartCoroutine(SpawnEnemies());
            //Debug.Log("Waiting");
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
        float randomNumber = Random.Range(0f, 1f);
        if (randomNumber <= twigSpawnChance)
        {
            //spawn twig
            unitToSpawn = twigPrefab;
            unitType = EnemyType.twig;
        }
        else if (randomNumber <= (twigSpawnChance + dryadSpawnChance))
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
            SpawnUnit(unitToSpawn, unitType);
        }
        else
        {
            bool unitFound = false;
            int i = 0;
            while (!unitFound)
            {
                GameObject tempUnit = unitSpool[i];
                if (
                    (unitType == EnemyType.twig)
                    && tempUnit.TryGetComponent<TwigStateMachine>(out TwigStateMachine twig)
                    && twig.isDead
                )
                {
                    unitFound = true;
                    spawnedTwigs++;
                    twig.RespawnTwig(
                        new Vector2(
                            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
                        )
                    );
                }
                else if (
                    (unitType == EnemyType.dryad)
                    && tempUnit.TryGetComponent<DryadStateMachine>(out DryadStateMachine dryad)
                    && dryad.isDead
                )
                {
                    unitFound = true;
                    spawnedDryads++;
                    dryad.RespawnDryad(
                        new Vector2(
                            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
                        )
                    );
                }
                // else if ((unitType == EnemyType.myconid) && tempUnit.TryGetComponent<MyconidStateMachine>(out TwigStateMachine twig))
                // {

                // }
                else
                {
                    i++;
                    if (i >= unitSpool.Count)
                    {
                        Debug.Log("Unit not found");
                        SpawnUnit(unitToSpawn, unitType);
                        break;
                    }
                }
            }
        }
        aliveUnits++;
        StartCoroutine(SpawnEnemies());
    }

    private void OnAnyEnemyDeath()
    {
        aliveUnits--;
    }
}
