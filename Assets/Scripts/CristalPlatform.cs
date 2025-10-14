using UnityEngine;

public class CristalPlatform : MonoBehaviour
{
    [Header("Configuración del cristal")]
    public int golpesParaRomper = 3;         // Cuántos golpes aguanta
    public float tiempoParaRegenerar = 0f;   // 0 = no se regenera
    public Sprite cristalRoto;               // Sprite opcional para efecto visual

    private int golpesRecibidos = 0;
    private bool roto = false;
    private SpriteRenderer sr;
    private Collider2D col;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    void OnCollisionEnter2D(Collision2D colision)
    {
        // Solo reaccionar si es el jugador
        if (colision.gameObject.CompareTag("Player") && !roto)
        {
            golpesRecibidos++;

            // Verificar si ya se rompió
            if (golpesRecibidos >= golpesParaRomper)
            {
                RomperCristal();
            }
        }
    }

    void RomperCristal()
    {
        roto = true;

        // Desactivar colisión y cambiar apariencia
        if (col) col.enabled = false;
        if (sr && cristalRoto) sr.sprite = cristalRoto;

        // Efecto opcional: temblor o sonido
        Debug.Log(" Cristal roto!");

        // Si debe regenerarse
        if (tiempoParaRegenerar > 0)
            Invoke(nameof(RegenerarCristal), tiempoParaRegenerar);
        else
            Destroy(gameObject);
    }

    void RegenerarCristal()
    {
        roto = false;
        golpesRecibidos = 0;
        if (col) col.enabled = true;
        Debug.Log(" Cristal regenerado!");
    }
}
