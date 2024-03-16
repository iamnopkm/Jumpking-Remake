using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;    
using UnityEngine.UI;

public class SaveSlotsMenu : Menu
{
    [Header("Menu Navigation")]
    [SerializeField] private MainMenu mainMenu;

    [Header("Menu Button")]

    [SerializeField] private Button backButton;
    
    private SaveSlot[] saveSlots;

    private bool isLoadingGame = false;
    private void Awake()
    {
        saveSlots = this.GetComponentsInChildren<SaveSlot>();
    }

    public void OnSaveSlotClicked(SaveSlot saveSlot)
    {
        //disable all buttons
        DisableMenuButtons();

        // update the selected profile id to be used for data persistence
        DataPersistenceManager.instance.ChangeSelectedProfileId(saveSlot.GetProfileId());
        Debug.Log("is loading game: " + this.isLoadingGame);
        if(!this.isLoadingGame)
        {
            // create a new game - which will initialize our data to a clean slate
            Debug.Log("Loading new game save slots ");
            DataPersistenceManager.instance.SetStartNewGameCheck(true);
            DataPersistenceManager.instance.NewGame();
        }else
        {
            DataPersistenceManager.instance.SetStartNewGameCheck(false);
        };
        Debug.Log("Loading Data for game");
        // Load the scene - which will in turn save the game becausse of OnSceneUnLoaded() in the DataPersistenceManager
        SceneManager.LoadSceneAsync("Scene map 1");
    }

    public void OnBackClicked()
    {
        mainMenu.ActivateMenu();
        this.DeactivateMenu();
    }
    
    public void ActivateMenu(bool isLoadingGame)
    {
        //set this menu to be active
        this.gameObject.SetActive(true);

        // set mode
        this.isLoadingGame = isLoadingGame;

        // Load all of the profiles that exist
        Dictionary<string, GameData> profilesGameData = DataPersistenceManager.instance.GetAllProfilesGameData();

        // Loop through each save slot in the UI and set the content appropriately
        foreach (SaveSlot saveSlot in saveSlots)
        {
            GameData profileData = null;
            profilesGameData.TryGetValue(saveSlot.GetProfileId(), out profileData);
            saveSlot.SetData(profileData);
            if(profileData == null && isLoadingGame)
            {
                saveSlot.SetInteractable(false);
            }
            else
            {
                saveSlot.SetInteractable(true);
            }
        }
    }
    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }

    private void DisableMenuButtons()
    {
        foreach( SaveSlot saveSlot in saveSlots)
        {
            saveSlot.SetInteractable(false);
        } 
        backButton.interactable = false;
    }
}
