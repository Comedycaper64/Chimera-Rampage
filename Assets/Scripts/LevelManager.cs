using System;
using System.Collections;
using System.Collections.Generic;
using Chimera;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static Action OnChimeraRespawn;
    private ChimeraStateMachine chimera;

    [SerializeField]
    private GameObject deathUI;

    private void Start()
    {
        chimera = GameObject.FindGameObjectWithTag("Player").GetComponent<ChimeraStateMachine>();
        chimera.health.OnDeath += TriggerDeathUI;
    }

    private void OnDisable()
    {
        chimera.health.OnDeath -= TriggerDeathUI;
    }

    private void TriggerDeathUI()
    {
        deathUI.SetActive(true);
    }

    public void RespawnLevel()
    {
        deathUI.SetActive(false);
        OnChimeraRespawn?.Invoke();
    }
}
