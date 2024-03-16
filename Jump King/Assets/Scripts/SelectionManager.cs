using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager instance;
    [SerializeField]
    private GameObject[] characters;

    private int _charIndex;

    public int CharIndex
    {
        get { return _charIndex; }
        set { _charIndex = value; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishLoaded;
    }

    void OnLevelFinishLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Scene 1")
        {
            Instantiate(characters[CharIndex]);
        }
    }

}
