using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class DialogueNPC : MonoBehaviour
{
    public float rangeToStartDialogue;
    private InputSystem_Actions inputActions;
    public  Dialogue startingDialogue, regularDialogue;
    private bool startedDialogue = false;
    public GameObject interactButton;
    
    private bool finishedStartingDialogue = false;
    
    private void OnEnable()
    {
        DialogueManager.OnDialogueEnd += DialogueEnd;
    }

    private void Start()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Player.Enable();
        
    }

    private void Update()
    {
        if (inputActions.Player.Interact.WasPressedThisFrame())
        {
            
            if (Vector2.Distance(PlayerMovement.Instance.transform.position, transform.position) <= rangeToStartDialogue && !startedDialogue && EnemyManager.Instance.enemyCount <= 0)
            {
                DialogueManager.Instance.StartDialogue(regularDialogue);
                startedDialogue = true;
            }else if (Vector2.Distance(PlayerMovement.Instance.transform.position, transform.position) <= rangeToStartDialogue && !startedDialogue)
            {
                
                DialogueManager.Instance.StartDialogue(startingDialogue);
                startedDialogue = true;
            }
            
        }
        
        interactButton.SetActive(Vector2.Distance(PlayerMovement.Instance.transform.position, transform.position) <= rangeToStartDialogue && !startedDialogue);
    }

    void DialogueEnd(string name)
    {
        if (name == startingDialogue.displayName)
        {
            startedDialogue = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangeToStartDialogue);
    }

    
    
}
