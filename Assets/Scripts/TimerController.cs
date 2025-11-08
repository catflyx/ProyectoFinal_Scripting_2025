using UnityEngine;
using TMPro; // Necesario para usar TextMeshPro

public class TimerController : MonoBehaviour
{
    [Header("Tiempo en segundos")]
    public float countdownTime = 20f;

    [Header("Objeto que se activará")]
    public GameObject objectToActivate;

    [Header("Texto en pantalla")]
    public TextMeshProUGUI timerText;

    [Header("Controlador del fondo")]
    public BackgroundController backgroundController;

    // 🔊 NUEVO: control de música
    [Header("Música del nivel")]
    public AudioClip musicaDuranteNivel;
    [Header("Música al finalizar")]
    public AudioClip musicaFinNivel;

    private AudioSource audioSource;

    // ahora 'timer' e 'isCounting' siguen privados pero expuestos vía propiedades públicas
    private float timer;
    private bool isCounting = true;
    private bool waitingReset = false; // espera a que EndsController avise

    public bool IsCounting => isCounting;
    public float CurrentTime => timer;

    public void Start()
    {
        // 🎵 Crear o recuperar AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.loop = false; // no queremos que se repita

        ResetTimer();
        if (timerText != null) timerText.color = Color.white;

        // 🎶 Reproducir música del nivel al iniciar
        if (musicaDuranteNivel != null)
        {
            audioSource.clip = musicaDuranteNivel;
            audioSource.Play();
        }
    }

    void Update()
    {
        if (backgroundController != null)
            backgroundController.ActualizarFondo(timer);

        // Solo cuenta si está activo
        if (isCounting)
        {
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                isCounting = false;
                waitingReset = true;

                // Activa el objeto
                if (objectToActivate != null)
                    objectToActivate.SetActive(true);

                // Muestra el mensaje (ambas líneas protegidas por null-check)
                if (timerText != null)
                {
                    timerText.color = Color.red;
                    timerText.text = "ES MUY TARDE";
                }

                Debug.Log("VIENE");

                // 🎵 Reproducir música final al acabar el tiempo
                if (musicaFinNivel != null)
                {
                    audioSource.Stop();
                    audioSource.clip = musicaFinNivel;
                    audioSource.Play();
                }
            }
            else
            {
                // Actualiza el texto con tiempo restante
                if (timerText != null)
                    timerText.text = $"{timer:F1}s";
            }
        }
    }

    public void ResetTimer()
    {
        timer = countdownTime;
        isCounting = true;
        waitingReset = false;

        if (timerText != null)
        {
            timerText.color = Color.white;
            timerText.text = $"{timer:F1}s";
        }

        if (objectToActivate != null)
            objectToActivate.SetActive(false);

        Debug.Log(" Timer reiniciado");

        // 🎶 Reiniciar música del nivel si existe
        if (musicaDuranteNivel != null && audioSource != null)
        {
            audioSource.Stop();
            audioSource.clip = musicaDuranteNivel;
            audioSource.Play();
        }
    }

    // Método público para pruebas: forzar que el timer llegue a cero
    public void ForceExpire()
    {
        timer = 0f;
        isCounting = true;      // garantizamos que Update() procese el caso
        Update();               // en modo test llamamos Update() manualmente si queremos
    }

    // Esta función será llamada por EndsController cuando toque al Player
    public void NotifyObjectTouchedPlayer()
    {
        ResetTimer();
    }
}
