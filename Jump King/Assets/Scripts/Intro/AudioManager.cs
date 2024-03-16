using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("AudioManager").AddComponent<AudioManager>();
            }
            return instance;
        }
    }

    private AudioSource sound_intro;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);

        // Tạo một AudioSource để chơi âm thanh
        sound_intro = gameObject.AddComponent<AudioSource>();
    }

    public void PlaySound(AudioClip sound)
    {
        // Chơi âm thanh
        if (sound != null)
        {
            sound_intro.PlayOneShot(sound);
        }
    }
}
