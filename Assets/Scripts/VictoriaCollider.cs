using UnityEngine;
using System.Collections; //  necesario para usar corrutinas

public class VictoriaCollider : MonoBehaviour
{
    [Header("Sonido de victoria")]
    public AudioClip sonidoVictoria; // sonido que sonará al llegar a la meta

    private bool jugadorDentro = false;
    private AudioSource audioSource;

    void Start()
    {
        // Aseguramos que haya un AudioSource en el objeto
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (jugadorDentro) return; // evitar llamadas dobles
        if (other.CompareTag("Player"))
        {
            jugadorDentro = true;

            // Reproducir sonido de victoria
            if (sonidoVictoria != null && audioSource != null)
                audioSource.PlayOneShot(sonidoVictoria);

            Debug.Log("¡Victoria! Pasando al siguiente nivel...");

            // Buscar el controlador de escena
            EscenaController escenaController = FindAnyObjectByType<EscenaController>();
            if (escenaController != null)
            {
                //  Esperar a que termine el sonido antes de pasar de nivel
                StartCoroutine(CambiarNivelDespuesDeSonido(escenaController));
            }
            else
            {
                Debug.LogWarning("No se encontró un EscenaController en la escena.");
            }
        }
    }

    //  Nueva corrutina añadida (no reemplaza nada existente)
    private IEnumerator CambiarNivelDespuesDeSonido(EscenaController escenaController)
    {
        if (sonidoVictoria != null)
        {
            // Esperar la duración exacta del clip
            yield return new WaitForSeconds(sonidoVictoria.length);
        }

        escenaController.SiguienteNivel();
    }
}
