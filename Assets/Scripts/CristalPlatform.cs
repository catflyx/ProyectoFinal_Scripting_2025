using UnityEngine;

public class CristalPlatform : MonoBehaviour
{
    [Header("Configuración del cristal")]
    public int golpesParaRomper = 3;
    public float tiempoParaRegenerar = 5f;
    public Sprite cristalRoto;
    public Sprite cristalNormal;

    [Header("Referencia al Timer")]
    public TimerController timerController;

    [Header("Sonido del cristal")]
    public AudioClip sonidoRoto; // asigna aquí tu clip de sonido
    private AudioSource audioSource;

    private int golpesRecibidos = 0;
    private bool roto = false;
    private SpriteRenderer sr;
    private Collider2D col;
    private bool esperandoRegeneracion = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        if (sr != null)
            cristalNormal = sr.sprite;

        if (timerController == null)
            timerController = FindObjectOfType<TimerController>();

        // Añadimos (o usamos) el AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        // Configuración básica para que no se corte
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (timerController != null && timerController.CurrentTime <= 0f && roto)
        {
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

        // Reproducir sonido
        if (sonidoRoto != null && audioSource != null)
            audioSource.PlayOneShot(sonidoRoto);

        // Cambiar sprite
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

        // Regeneración automática
        if (tiempoParaRegenerar > 0)
            Invoke(nameof(RegenerarCristal), tiempoParaRegenerar);
    }

    void RegenerarCristal()
    {
        if (!esperandoRegeneracion) return;

        roto = false;
        esperandoRegeneracion = false;
        golpesRecibidos = 0;

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
