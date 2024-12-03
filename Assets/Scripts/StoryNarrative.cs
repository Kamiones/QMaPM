using UnityEngine;
using System.Collections;

public class StoryNarrative : MonoBehaviour
{
    public CanvasGroup narrativeText; // Asignar el CanvasGroup del texto
    public float fadeDuration = 2f;   // Duración del fade-in/fade-out
    public float displayDuration = 5f; // Tiempo que el texto permanece visible

    private void Start()
    {
        StartCoroutine(PlayNarrative());
    }

    private IEnumerator PlayNarrative()
    {
        // Fade-in
        yield return StartCoroutine(FadeCanvasGroup(narrativeText, 0, 1, fadeDuration));
        yield return new WaitForSeconds(displayDuration);
        // Fade-out
        yield return StartCoroutine(FadeCanvasGroup(narrativeText, 1, 0, fadeDuration));
        
        // Aquí puedes cargar la siguiente escena o continuar con la narrativa
        Debug.Log("Narrativa completada");
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = endAlpha;
    }
}
