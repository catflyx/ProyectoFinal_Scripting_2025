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

    private float timer;
    private bool isCounting = true;
    private bool waitingReset = false; // espera a que EndsController avise

    void Start()
    {
        ResetTimer(); timerText.color = Color.white;
    }

    void Update()
    {
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

                // Muestra el mensaje
                if (timerText != null)
                    timerText.color = Color.red;
                    timerText.text = "ES MUY TARDE";

                Debug.Log("VIENE");
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
            timerText.text = $"Tiempo: {timer:F1}s";

        if (objectToActivate != null)
            objectToActivate.SetActive(false);
    }

    // Esta función será llamada por EndsController cuando toque al Player
    public void NotifyObjectTouchedPlayer()
    {
        if (waitingReset)
        {
            ResetTimer(); timerText.color = Color.white;
        }
    }
}
