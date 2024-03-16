using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;

[System.Serializable]
public class GameData
{
    public bool isFinished;
    //Jumpkingscript's data
    public float totalTimePlayed;
    public Vector3 playerPosition;
    public Dictionary<string, bool> itemsCollected;

    //GameObjectsController data
    public string currentPosition;

    //SaveSlot ID
    public int ID { get; set; }

    // the values defined in this constructor will be default values
    // the game starts with when there's no data to load
    public GameData()
    {
        isFinished = false;
        totalTimePlayed = 0;
        playerPosition = new Vector3(237.81f, 5.26f, -1.74f);
    }
    public float GetPosition()
    {
        return playerPosition.x;
    }

    public float GetTimePlayed()
    {
        return totalTimePlayed;
    }
}
