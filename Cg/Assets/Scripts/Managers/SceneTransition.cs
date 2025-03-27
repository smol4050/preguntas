using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    [Header("Referencias Obligatorias")]
    [SerializeField] private GameObject transitionOverlay;
    [SerializeField] private Animator animator;
    [SerializeField] private Button playButton;

    [Header("Configuración de Tiempos")]
    [SerializeField] private float fadeOutDuration = 1f;
    [SerializeField] private float fadeInDuration = 1f;

    private int currentSceneIndex;

    private void Awake()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        ConfigureOverlay();
    }

    private void Start()
    {
        if (currentSceneIndex == 0)
        {
            playButton.onClick.AddListener(TriggerFadeOut);
            transitionOverlay.SetActive(false);
        }
        else
        {
            StartCoroutine(PlayFadeIn());
        }
    }

    private void ConfigureOverlay()
    {
        RectTransform rt = transitionOverlay.GetComponent<RectTransform>();
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;

    }

    private void TriggerFadeOut() => StartCoroutine(PlayFadeOut());

    private IEnumerator PlayFadeOut()
    {
        transitionOverlay.SetActive(true);
        animator.SetTrigger("StartFadeOut");
        yield return new WaitForSeconds(fadeOutDuration);
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    private IEnumerator PlayFadeIn()
    {
        transitionOverlay.SetActive(true);
        animator.SetTrigger("StartFadeIn");
        yield return new WaitForSeconds(fadeInDuration);
        transitionOverlay.SetActive(false);
    }
}