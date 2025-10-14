using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ImanGravedad : MonoBehaviour
{
    [Header("Configuración del imán de gravedad")]
    [Tooltip("Multiplicador para reducir la gravedad cuando se invierte")]
    public float factorReduccion = 0.3f;
    [Tooltip("Velocidad de rotación visual del jugador")]
    public float rotacionSuavizada = 5f;

    private Rigidbody2D rbJugador;
    private bool jugadorDentro = false;
    private float gravedadOriginal;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            rbJugador = other.attachedRigidbody;

            if (rbJugador != null)
            {
                // Guardamos la gravedad original
                gravedadOriginal = rbJugador.gravityScale;

                // Invertimos la gravedad con menor intensidad
                rbJugador.gravityScale = -Mathf.Abs(gravedadOriginal) * factorReduccion;

                jugadorDentro = true;

                // Giramos visualmente al jugador (como si estuviera boca abajo)
                other.transform.rotation = Quaternion.Euler(0, 0, 180f);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && jugadorDentro)
        {
            // Restauramos la gravedad original
            rbJugador.gravityScale = gravedadOriginal;
            jugadorDentro = false;

            // Regresamos la rotación visual a la normal
            other.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void Update()
    {
        // Rotación suavizada (solo efecto visual)
        if (jugadorDentro && rbJugador != null)
        {
            Quaternion rotObjetivo = Quaternion.Euler(0, 0, 180f);
            rbJugador.transform.rotation = Quaternion.Slerp(
                rbJugador.transform.rotation,
                rotObjetivo,
                Time.deltaTime * rotacionSuavizada
            );
        }
    }
}