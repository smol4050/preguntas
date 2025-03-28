using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Efectos de Sonido")]
    [SerializeField] private AudioClip correctSound;
    [SerializeField] private AudioClip incorrectSound;

    [Header("Música de Fondo")]
    [SerializeField] private AudioClip bgScene0;
    [SerializeField] private AudioClip bgScene1;

    private AudioSource bgSource;
    private AudioSource sfxSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            bgSource = gameObject.AddComponent<AudioSource>();
            bgSource.loop = true;
            sfxSource = gameObject.AddComponent<AudioSource>();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0)
        {
            PlayBackground(bgScene0);
        }
        else if (scene.buildIndex == 1)
        {
            PlayBackground(bgScene1);
        }
    }

    public void PlayBackground(AudioClip clip)
    {
        if (bgSource.clip != clip)
        {
            bgSource.clip = clip;
            bgSource.Play();
        }
    }

    public void PlayCorrect()
    {
        if (correctSound != null)
            sfxSource.PlayOneShot(correctSound);
        else
            Debug.LogError("correctSound no está asignado en AudioManager.");
    }

    public void PlayIncorrect()
    {
        if (incorrectSound != null)
            sfxSource.PlayOneShot(incorrectSound);
        else
            Debug.LogError("incorrectSound no está asignado en AudioManager.");
    }
}

