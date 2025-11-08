using UnityEngine;

public class EndsController : MonoBehaviour
{
    [Header("Velocidad de movimiento hacia la derecha")]
    [SerializeField] private float moveSpeed = 120f;

    [Header("Sonido al colisionar con el jugador")]
    public AudioClip sonidoImpacto; //  asigna tu clip aquí

    private Vector3 startPosition;
    private TimerController timerController;
    private AudioSource audioSource;

    void Start()
    {
        startPosition = transform.position;
        timerController = FindFirstObjectByType<TimerController>();

        // Aseguramos que haya un AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
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
            //  Reproducir sonido al impactar
            if (sonidoImpacto != null && audioSource != null)
                audioSource.PlayOneShot(sonidoImpacto);

            // Volver a posición inicial
            transform.position = startPosition;

            // Avisar al TimerController que reinicie el tiempo
            if (timerController != null)
                timerController.NotifyObjectTouchedPlayer();
        }
    }
}
