using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class DialogueTrigger : MonoBehaviour
// {


//     private void Start()
//     {
//         // Get necessary components at the start
//         rb = gameObject.GetComponent<Rigidbody2D>();
//         anim = gameObject.GetComponent<Animator>();
//     }

//     //Detect trigger with player
//     private void OnTriggerEnter2D(Collider2D collision)
//     {
//         //If we triggerd the player enable playerdeteced and show indicator
//         if(collision.tag == "Player")
//         {
//             playerDetected = true;
//             dialogueScript.ToggleIndicator(playerDetected);
//         }
//     }

//     private void OnTriggerExit2D(Collider2D collision)
//     {
//         //If we lost trigger  with the player disable playerdeteced and hide indicator
//         if (collision.tag == "Player")
//         {
//             playerDetected = false;
//             dialogueScript.ToggleIndicator(playerDetected);
//             dialogueScript.EndDialogue();
//         }
//     }
//     //While detected if we interact start the dialogue
//     private void Update()
//     {
//         if (playerDetected && Input.GetKeyDown(KeyCode.E))
//         {
//             // Create a list of dialogues and set the writing speed
//             List<string> newDialogues = new List<string>();
//             // Add your dialogues to the list

//             float newWritingSpeed = 0.05f; // Set your desired writing speed

//             // Start dialogue with parameters
//             dialogueScript.StartDialogueWithParameters(newDialogues, newWritingSpeed);
//         }
//         UpdateAnimation(); // Update character animations
//     }

  

    // Function to update character animations
    // void UpdateAnimation()
    // {
    //     Debug.Log("Updating Animation");
    //     // Reset all animation states
    //     anim.SetBool("idle", false);
    //     anim.SetBool("lookup", false);
    //     if(!playerDetected){
    //         anim.SetBool("idle", true);
    //     }else if(playerDetected ){
    //         anim.SetBool("lookup", true);
    //     }
    // }

public class DialogueTrigger : MonoBehaviour
{ 
    public Rigidbody2D rb;    

    public Dialogue dialogueScript;

    private bool playerDetected;

    public Animator anim;
    private void Start()
    {
        // Get necessary components at the start
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
    }
    //Detect trigger with player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If we triggerd the player enable playerdeteced and show indicator
        if(collision.tag == "Player")
        {
            if(anim.GetBool("lookup") == false)
            {
                anim.SetBool("lookup", true);
            }
            playerDetected = true;
            dialogueScript.ToggleIndicator(playerDetected);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //If we lost trigger  with the player disable playerdeteced and hide indicator
        if (collision.tag == "Player")
        {
            playerDetected = false;
            dialogueScript.ToggleIndicator(playerDetected);
            dialogueScript.EndDialogue();
            UpdateAnimation();
        }
    }
    //While detected if we interact start the dialogue
    private void Update()
    {
        UpdateAnimation();
        if(playerDetected && Input.GetKeyDown(KeyCode.E))
        {
            dialogueScript.StartDialogue();
        }
    }
    void UpdateAnimation()
    {
        // Reset all animation states
        anim.SetBool("idle", false);
        anim.SetBool("lookup", false);
        anim.SetBool("sleep", false);

        if (dialogueScript.IsDialogueRunning())
        {
            // If dialogue is running, set idle animation
            anim.SetBool("idle", true);
        }
        else if (playerDetected)
        {
            // If player detected and not in dialogue, set lookup animation
            anim.SetBool("lookup", true);
        }
        else
        {
            // If neither player detected nor in dialogue, set idle animation
            anim.SetBool("sleep", true);
        }
    }
}
