using UnityEngine;

public class CristalPlatform : MonoBehaviour
{
    [Header("Configuración del cristal")]
    public int golpesParaRomper = 3;
    public float tiempoParaRegenerar = 5f;
    public Sprite cristalRoto;
    public Sprite cristalNormal; // guardamos el sprite original

    [Header("Referencia al Timer")]
    public TimerController timerController;

    private int golpesRecibidos = 0;
    private bool roto = false;
    private SpriteRenderer sr;
    private Collider2D col;
    private bool esperandoRegeneracion = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        // Guarda el sprite original para restaurarlo luego
        if (sr != null)
            cristalNormal = sr.sprite;

        // Opcional: si no se asigna en el inspector, busca automáticamente el TimerController
        if (timerController == null)
            timerController = FindObjectOfType<TimerController>();
    }

    void Update()
    {
        // Si hay un TimerController y el tiempo llegó a 0 o menos  regenerar
        if (timerController != null && timerController.CurrentTime <= 0f && roto)
        {
            // regenerar aunque no haya pasado el tiempo fijo
            RegenerarCristal();
        }
    }

    void OnCollisionEnter2D(Collision2D colision)
    {
        if (colision.gameObject.CompareTag("Player") && !roto)
        {
            golpesRecibidos++;

            if (golpesRecibidos >= golpesParaRomper)
            {
                RomperCristal();
            }
        }
    }

    void RomperCristal()
    {
        roto = true;
        esperandoRegeneracion = true;

        // Ocultar el cristal visualmente
        if (sr)
        {
            if (cristalRoto != null)
                sr.sprite = cristalRoto;
            else
                sr.enabled = false;
        }

        // Desactivar colisión
        if (col) col.enabled = false;

        Debug.Log(" Cristal roto!");

        // Si tiene tiempo de regeneración automática
        if (tiempoParaRegenerar > 0)
            Invoke(nameof(RegenerarCristal), tiempoParaRegenerar);
    }

    void RegenerarCristal()
    {
        if (!esperandoRegeneracion) return;

        roto = false;
        esperandoRegeneracion = false;
        golpesRecibidos = 0;

        // Restaurar sprite y colisión
        if (sr)
        {
            sr.enabled = true;
            if (cristalNormal != null)
                sr.sprite = cristalNormal;
        }
        if (col) col.enabled = true;

        Debug.Log(" Cristal regenerado!");
    }
}
