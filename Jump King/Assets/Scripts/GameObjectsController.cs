using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameObjectsController : MonoBehaviour, IDataPersistence
{
    public bool isFinished;
    
    [SerializeField] public string newArea;
    [SerializeField] private string currentArea;
    [SerializeField] private TMP_Text mPositionText;

    void Start()
    {
        mPositionText.enabled = false;
        StartCoroutine(DisplayCurrentPosition(5.0f));
    }
    private void Update()
    {
        if (newArea != currentArea)
        {
            currentArea = newArea;
            StartCoroutine(DisplayCurrentPosition(5.0f));
        }
        //check if game finished
        if(isFinished == true)
        {
            DataPersistenceManager.instance.SaveGame();

            //load back to the menu
            SceneManager.LoadScene(1);
        }
    }

    private IEnumerator DisplayCurrentPosition(float waitTime)
    {
        mPositionText.enabled = true;
        mPositionText.text = currentArea;
        yield return new WaitForSeconds(waitTime);
        mPositionText.enabled = false;
    }
    public void LoadData(GameData data)
    {
        isFinished = data.isFinished;
        currentArea = data.currentPosition;
    }

    public void SaveData(ref GameData data)
    {
        data.isFinished = isFinished;
        data.currentPosition = currentArea;
    }
}
