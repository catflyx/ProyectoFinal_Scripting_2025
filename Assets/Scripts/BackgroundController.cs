using UnityEngine;
using System.Collections;

public class BackgroundController : MonoBehaviour
{
    [Header("Fondos en orden de transición")]
    public Sprite[] fondos; // 4 sprites (0 = inicial, 1-3 = transición)
    public SpriteRenderer fondoRenderer; // el SpriteRenderer que muestra el fondo

    [Header("Duración entre cambios (segundos)")]
    public float duracionTransicion = 1.5f;

    private bool enTransicion = false;
    private bool fondoOscuroActivo = false;

    private Coroutine transicionCoroutine;

    void Start()
    {
        if (fondos.Length > 0 && fondoRenderer != null)
            fondoRenderer.sprite = fondos[0]; // Fondo inicial
    }

    // Método llamado por el TimerController
    public void ActualizarFondo(float tiempoActual)
    {
        if (tiempoActual <= 10f && !fondoOscuroActivo && !enTransicion)
        {
            // Iniciar transición suave
            if (transicionCoroutine != null)
                StopCoroutine(transicionCoroutine);

            transicionCoroutine = StartCoroutine(TransicionSuave());
        }
        else if (tiempoActual > 10f && fondoOscuroActivo)
        {
            // Regresar instantáneamente al fondo predeterminado
            if (transicionCoroutine != null)
                StopCoroutine(transicionCoroutine);

            fondoRenderer.sprite = fondos[0];
            fondoOscuroActivo = false;
            enTransicion = false;
        }
    }

    private IEnumerator TransicionSuave()
    {
        enTransicion = true;

        for (int i = 1; i < fondos.Length; i++)
        {
            // Desvanecer el fondo actual hacia el siguiente
            Sprite siguiente = fondos[i];
            Sprite actual = fondoRenderer.sprite;

            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime / duracionTransicion;
                fondoRenderer.color = new Color(1, 1, 1, 1 - t);
                yield return null;
            }

            fondoRenderer.sprite = siguiente;
            fondoRenderer.color = Color.white;

            yield return new WaitForSeconds(duracionTransicion);
        }

        fondoOscuroActivo = true;
        enTransicion = false;
    }
}

