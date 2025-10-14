using UnityEngine;

public class VictoriaCollider : MonoBehaviour
{
    private bool jugadorDentro = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (jugadorDentro) return; // evitar llamadas dobles
        if (other.CompareTag("Player"))
        {
            jugadorDentro = true;
            Debug.Log("¡Victoria! Pasando al siguiente nivel...");

            // Buscar el controlador de escena y pasar de nivel
            EscenaController escenaController = FindAnyObjectByType<EscenaController>();
            if (escenaController != null)
            {
                escenaController.Volver();
            }
            else
            {
                Debug.LogWarning(" No se encontró un EscenaController en la escena.");
            }
        }
    }
}
