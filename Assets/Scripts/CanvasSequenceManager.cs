using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasSequenceManager : MonoBehaviour
{
    [SerializeField, TextArea] private string[] textos;
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] float fadeDuration = 1f, displayDuration = 3f;

    void Start()
    {
        StartCoroutine(PlaySequence());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) NextScene();
    }

    private void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private IEnumerator PlaySequence()
    {
        foreach (string t in textos)
        {
            textMeshPro.text = t;
            yield return StartCoroutine(FadeAlpha(true, fadeDuration));
            yield return new WaitForSeconds(displayDuration);
            yield return StartCoroutine(FadeAlpha(false, fadeDuration));
        }
        NextScene();
    }

    private IEnumerator FadeAlpha(bool fadeIn, float duration)
    {
        float elapsed = 0f, start, end;
        if (fadeIn)
        {
            start = 0f;
            end = 1f;
        }
        else
        {
            start = 1f;
            end = 0f;
        }
        Color aux = Color.white;
        while (elapsed < duration)
        {
            float a = Mathf.Lerp(start, end, elapsed / duration);
            aux = textMeshPro.color;
            textMeshPro.color = new Color(aux.r, aux.g, aux.b, a);
            elapsed += Time.deltaTime;
            yield return null;
        }
        textMeshPro.color = new Color(aux.r, aux.g, aux.b, end);
    }

}