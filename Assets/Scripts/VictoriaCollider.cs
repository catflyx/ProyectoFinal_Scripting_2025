using UnityEngine;

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

            Debug.Log(" ¡Victoria! Pasando al siguiente nivel...");

            // Buscar el controlador de escena y pasar de nivel
            EscenaController escenaController = FindAnyObjectByType<EscenaController>();
            if (escenaController != null)
            {
                // Esperamos un momento antes de cambiar de escena para que el sonido se escuche completo
                escenaController.Invoke(nameof(escenaController.SiguienteNivel), sonidoVictoria != null ? sonidoVictoria.length : 0f);
            }
            else
            {
                Debug.LogWarning(" No se encontró un EscenaController en la escena.");
            }
        }
    }
}
