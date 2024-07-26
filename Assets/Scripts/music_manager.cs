using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    public AudioClip music1;
    public AudioClip music2;
    private AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.buildIndex)
        {
            case 1:
            case 2:
            case 3:
            case 4:
                if (audioSource.clip != music1)
                {
                    audioSource.clip = music1;
                    audioSource.Play();
                }
                break;
            case 5:
                if (audioSource.clip != music2)
                {
                    audioSource.clip = music2;
                    audioSource.Play();
                }
                break;
            default:
                break;
        }
    }
}