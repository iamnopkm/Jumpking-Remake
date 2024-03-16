using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;
using UnityEngine.SceneManagement;
using System.Linq;
public class DataPersistenceManager : MonoBehaviour
{
    [Header("Debugging")]
    [SerializeField] private bool initializeDataIfNull = false;

    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] public string selectedProfileId = "test";
    //= Directory.GetFiles(Application.persistentDataPath, "*.gamesave");
    public GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;

    public bool startNewGame = false;
    public static DataPersistenceManager instance { get; private set; }


    // Start is called before the first frame update

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Found more than one Data Persistence Manager in the scene.");
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    public void OnSceneLoaded(Scene scene,LoadSceneMode mode)
    {
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    private void OnSceneUnloaded(Scene scene)
    {
        SaveGame();
    }

    public void ChangeSelectedProfileId(string newProfileId)
    {
        this.selectedProfileId = newProfileId;
        Debug.Log("current profile id: " + selectedProfileId);
        // Load the game data
        LoadGame();
    }

    public void Update()
    {
    }
    public void NewGame()
    {
        this.gameData = new GameData();
        //TODO - push the loaded data to all other scripts that need it
        Debug.Log("New game created");
    }

    public void LoadGame()
    {
        if(startNewGame == false)
        {
            //TODO - Load any saved data from a file using the new handler
            this.gameData = dataHandler.Load(selectedProfileId); 
        }   
        //if no data can be loaded, initialize to a new game
        if(initializeDataIfNull && this.gameData == null)
        {
            Debug.Log("No data was found. Initializing data to defaults");
            NewGame();
        }        
        if(gameData == null)
        {
            Debug.Log("No data was found. A new Game needs to be started before can be loaded");
        }
        //TODO - push the loaded data to all other scripts that need it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }

    public void SetStartNewGameCheck(bool check)
    {
        this.startNewGame = check;
    }
    
    public void SaveGame()
    {
        //TODO - pass the data to other scripts so they can update it
        foreach ( IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
            Debug.Log("saving..");;
        }
        //save data to the file using data handler
        dataHandler.Save(gameData, selectedProfileId);
        
    }
    private void OnApplicationQuit() {
        SaveGame();
    }

    public List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }
    
    public bool HasGameData()
    {
        return gameData != null;
    }

    public Dictionary<string, GameData> GetAllProfilesGameData()
    {
        // Ensure dataHandler is not null before calling its methods
        if (dataHandler != null)
        {
            Dictionary<string, GameData> result = dataHandler.LoadAllProfiles();
            return result;
        }
        else
        {
            Debug.LogError("DataHandler is null in GetAllProfilesGameData");
            return new Dictionary<string, GameData>();
        }
    }
}
