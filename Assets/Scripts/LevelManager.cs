using System;
using System.Collections;
using System.Collections.Generic;
using Chimera;
using Enemies.Elemental;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static Action OnChimeraRespawn;
    private ChimeraStateMachine chimera;

    [SerializeField]
    private GameObject deathUI;

    [SerializeField]
    private GameObject restartButton;

    [SerializeField]
    private List<LevelGate> levelGates = new List<LevelGate>();

    private void Start()
    {
        chimera = GameObject.FindGameObjectWithTag("Player").GetComponent<ChimeraStateMachine>();
        chimera.health.OnDeath += TriggerDeathUI;
        EncounterArena.OnAnyEncounterStart += RaiseEntryGates;
        EnemyManager.OnEncounterFinish += LowerExitGates;
        ElementalStateMachine.EndGame += EnableRestartButton;
    }

    private void OnDisable()
    {
        chimera.health.OnDeath -= TriggerDeathUI;
        EncounterArena.OnAnyEncounterStart -= RaiseEntryGates;
        EnemyManager.OnEncounterFinish -= LowerExitGates;
        ElementalStateMachine.EndGame -= EnableRestartButton;
    }

    private void EnableRestartButton(object sender, EventArgs e)
    {
        restartButton.SetActive(true);
    }

    private void LowerExitGates(object sender, EnemyEncounter encounter)
    {
        int encounterNumber = encounter.encounterNumber;
        LevelGate gateToLower = levelGates[(2 * encounterNumber) - 1];
        gateToLower.gameObject.SetActive(false);
    }

    private void RaiseEntryGates(object sender, EnemyEncounter encounter)
    {
        int encounterNumber = encounter.encounterNumber;
        LevelGate gateToRaise = levelGates[(2 * encounterNumber) - 2];
        gateToRaise.gameObject.SetActive(true);
    }

    private void TriggerDeathUI(object sender, EventArgs e)
    {
        deathUI.SetActive(true);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(0);
    }

    public void RespawnLevel()
    {
        deathUI.SetActive(false);
        foreach (LevelGate gate in levelGates)
        {
            if ((levelGates.IndexOf(gate) % 2) == 1)
            {
                gate.gameObject.SetActive(true);
            }
            else
            {
                gate.gameObject.SetActive(false);
            }
        }
        OnChimeraRespawn?.Invoke();
    }
}
