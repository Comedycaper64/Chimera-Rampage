using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSetter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (
            other.TryGetComponent<Chimera.ChimeraStateMachine>(
                out Chimera.ChimeraStateMachine stateMachine
            )
        )
        {
            stateMachine.SetRespawnPoint(transform.position);
        }
    }
}
