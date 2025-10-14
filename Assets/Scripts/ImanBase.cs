using UnityEngine;

public class ImanBase : MonoBehaviour
{
    [Header("Configuración base")]
    public float fuerzaMaxima = 50f;
    public float radioDeAtraccion = 5f;

    protected virtual float CalcularFuerza(float distancia)
    {
        // Fórmula base: más lejos = menos fuerza
        float intensidad = Mathf.Clamp01(1 - (distancia / radioDeAtraccion));
        return fuerzaMaxima * intensidad;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D rb = other.attachedRigidbody;
            if (rb != null && !rb.isKinematic)
            {
                Vector2 dir = (transform.position - other.transform.position);
                float distancia = dir.magnitude;
                dir.Normalize();

                float fuerza = CalcularFuerza(distancia);
                rb.AddForce(dir * fuerza, ForceMode2D.Force);

                Debug.DrawLine(other.transform.position, transform.position, Color.red);
            }
        }
    }
}

