using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    [SerializeField] public GameObject pauseMenu;

    [SerializeField] public GameObject settingMenu;

    private static bool onPause = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        { 
            if (onPause == true) {
                Resume();
		    }else
            {
                Pause();
            }
        }
       
        
    }
    public void Pause()
    {
		Time.timeScale = 0.0f;
		pauseMenu.SetActive(true);
		Cursor.visible = true;
		// Camera.audio.Pause ();
		onPause = true;
    }
    public void Resume()
    {
		Time.timeScale = 1.0f;
		pauseMenu.SetActive(false);
		Cursor.visible = false;
		// Camera.audio.Pause ();
		onPause = false;
    }

    public void Setting()
    {
        settingMenu.SetActive(true);
    }

    public void Home(int SceneID)
    {   
        DataPersistenceManager.instance.SaveGame();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneID);
    }
    public void DoExitGame() 
    {
        Application.Quit();
    }
}
