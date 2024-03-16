using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        int selectedCharacter = int.Parse(EventSystem.current.currentSelectedGameObject.name);

        SelectionManager.instance.CharIndex = selectedCharacter;

        SceneManager.LoadScene("Scene 1");
    }
}
