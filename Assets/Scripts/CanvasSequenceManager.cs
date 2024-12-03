using System.Collections;
using UnityEngine;

public class CanvasSequenceManager : MonoBehaviour
{
    public CanvasGroup[] elements; // Array para almacenar los elementos del Canvas
    public float fadeDuration = 1f; // Duración del fade-in y fade-out
    public float displayDuration = 3f; // Tiempo que cada elemento permanece visible

    private void Start()
    {
        StartCoroutine(PlaySequence());
    }

    private IEnumerator PlaySequence()
    {
        // Recorre todos los elementos en el array
        foreach (CanvasGroup element in elements)
        {
            // Fade-in: Aparece el elemento
            yield return StartCoroutine(FadeCanvasGroup(element, 0, 1, fadeDuration));

            // Espera mientras el elemento se muestra
            yield return new WaitForSeconds(displayDuration);

            // Fade-out: Desaparece el elemento
            yield return StartCoroutine(FadeCanvasGroup(element, 1, 0, fadeDuration));
        }

        // Opcional: Llamar a otra función o cargar una nueva escena cuando termine la secuencia
        Debug.Log("Secuencia completada.");
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = endAlpha;
    }
}