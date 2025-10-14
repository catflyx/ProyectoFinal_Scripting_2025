using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public float orbitRadius = 1.2f; // Distancia de la flecha al jugador

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        if (player == null)
            player = GameObject.FindWithTag("Player").transform;

        if (mainCamera == null)
            Debug.LogError("No se encontró una cámara con la etiqueta 'MainCamera'");
    }

    void Update()
    {
        if (mainCamera == null || player == null)
            return; // evita errores si no hay cámara o jugador

        // Si el mouse está fuera de la pantalla, se ignora la actualización
        if (Input.mousePosition.x < 0 || Input.mousePosition.y < 0 ||
            Input.mousePosition.x > Screen.width || Input.mousePosition.y > Screen.height)
            return;

        // Posición del mouse en coordenadas de mundo
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        // Dirección normalizada del jugador al mouse
        Vector3 direction = (mousePos - player.position).normalized;

        // Nueva posición de la flecha (orbita alrededor del jugador)
        transform.position = player.position + direction * orbitRadius;

        // Rotar la flecha para que mire hacia el mouse
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f); // Ajustar según el sprite
        //Debug.Log($"Ángulo: {angle}");
    }
}
