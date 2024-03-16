using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Manage area name
public class AreaCollider : MonoBehaviour
{
    [SerializeField] private string areaName;
    [SerializeField] private GameObject gameController;
    private GameObjectsController controllerScript;

    void Start()
    {
        controllerScript = gameController.GetComponent<GameObjectsController>();
        if (controllerScript == null)
        {
            Debug.LogError("GameObjectsController component not found on GameController GameObject.");
        }
    }
    //Change the name of newarea in the gameobjectcontroller script
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && controllerScript != null)
        {
            // Access controllerScript and set the currentArea
            controllerScript.newArea = areaName;
        }
    }
}
