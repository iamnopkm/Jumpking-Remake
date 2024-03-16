using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelinePlayer : MonoBehaviour
{
    private PlayableDirector director;
    public GameObject objectActiveDirector; // Assign the GameObject with PlayableDirector component in the Inspector
    public GameObject mCamera;

    // Gameobject controller object and instance
    [SerializeField] GameObject gameObjController;
    private GameObjectsController controllerScript;


    // 18.95
    void Awake()
    {
        //Find the controller script
        controllerScript = gameObjController.GetComponent<GameObjectsController>();

        mCamera.GetComponent<CameraFollow>().enabled = false; // Disable CameraFollow script initially
        director = objectActiveDirector.GetComponent<PlayableDirector>(); // Assign the PlayableDirector component
        director.stopped += OnPlayableDirectorStopped;

    }

    void OnDisable()
    {
        director.stopped -= OnPlayableDirectorStopped;
    }

    void OnPlayableDirectorStopped(PlayableDirector aDirector)
    {
        if (director == aDirector)
        {
            Debug.Log("PlayableDirector named " + aDirector.name + " is now stopped.");
            controllerScript.isFinished = true;
            DataPersistenceManager.instance.SaveGame();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("collide princess");
            StartTimeline();
        }
    }

    public void StartTimeline()
    {
        director.Play(); // Play the Timeline associated with the PlayableDirector component
    }
}
