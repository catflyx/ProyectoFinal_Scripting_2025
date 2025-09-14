using UnityEngine;

public class EndsController : MonoBehaviour
{
    [Header("Velocidad de movimiento hacia la derecha")]
    float moveSpeed = 70f; //velocidad

    private Vector3 startPosition;
    private TimerController timerController;

    void Start()
    {
        startPosition = transform.position;
        // Nuevo método recomendado por Unity
        timerController = FindFirstObjectByType<TimerController>();
    }

    void OnEnable()
    {
        transform.position = startPosition;
    }

    void Update()
    {
        // Movimiento constante hacia la derecha
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Volver a posición inicial
            transform.position = startPosition;

            // Avisar al TimerController que reinicie el tiempo
            if (timerController != null)
                timerController.NotifyObjectTouchedPlayer();
        }
    }
}
