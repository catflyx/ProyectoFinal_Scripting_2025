using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public float orbitRadius = 1.2f; // Distancia de la flecha al jugador

    void Update()
    {
        // Posición del mouse en coordenadas de mundo
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        // Dirección normalizada del jugador al mouse
        Vector3 direction = (mousePos - player.position).normalized;

        // Nueva posición de la flecha (orbita alrededor del jugador)
        transform.position = player.position + direction * orbitRadius;

        // Rotar la flecha para que mire hacia el mouse
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
        // El -90 depende de cómo esté orientado tu sprite de flecha
    }
}
