using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
public class MainMenu : Menu
{
    [Header("Menu Navigation")]
    [SerializeField] private SaveSlotsMenu saveSlotsMenu;
    
    [Header("Menu Button")]

    [SerializeField] public Button NewGameButton;
    
    [SerializeField] private Button LoadGameButton;
    
    private void Start()
    {
        // if(!DataPersistenceManager.instance.HasGameData)
        // {
        //     ContinueGameButton.interactable = false;
        // }
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Game closed!");
    }

    public void Continue()
    {

    }

    public void OnNewGameClicked()
    {
        saveSlotsMenu.ActivateMenu(false);
        this.DeactivateMenu();
    }
    public void OnLoadGameClicked()
    {
        saveSlotsMenu.ActivateMenu(true);
        this.DeactivateMenu();
    }
    public void OnSaveGameClicked()
    {
        DataPersistenceManager.instance.SaveGame();
    }
    public void SaveGame()
    {
        
    }

    public void Options()
    {

    }

    public void ActivateMenu()
    {
        this.gameObject.SetActive(true);
    }

    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }
}
