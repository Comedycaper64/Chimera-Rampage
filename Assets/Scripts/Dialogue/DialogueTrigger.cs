using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
    private Conversation standardConversation;

    [SerializeField]
    private Collider2D triggerCollider;

    private void Awake()
    {
        triggerCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerDialogue();
        }
    }

    public void TriggerDialogue()
    {
        if (!DialogueManager.Instance.inConversation)
        {
            DialogueManager.Instance.StartConversation(standardConversation);

            triggerCollider.enabled = false;
        }
    }
}
