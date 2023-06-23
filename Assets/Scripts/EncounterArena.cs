using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterArena : MonoBehaviour
{
    [SerializeField]
    private EnemyEncounter encounter;

    private Collider2D arenaCollider;

    public static EventHandler<EnemyEncounter> OnAnyEncounterStart;

    private void Awake()
    {
        arenaCollider = GetComponent<Collider2D>();
        LevelManager.OnChimeraRespawn += EnableArena;
    }

    private void OnDisable()
    {
        LevelManager.OnChimeraRespawn -= EnableArena;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<HealthSystem>())
        {
            BeginEncounter();
        }
    }

    private void EnableArena()
    {
        Debug.Log("ayaya");
        arenaCollider.enabled = true;
    }

    private void BeginEncounter()
    {
        arenaCollider.enabled = false;
        OnAnyEncounterStart?.Invoke(this, encounter);
    }
}
