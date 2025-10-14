using UnityEngine;

public class ImanPlataforma : MonoBehaviour
{
    [Header("Configuración del imán")]
    public float fuerzaMaxima = 50f;
    public float radioDeAtraccion = 5f;
    public bool soloVertical = false;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D rb = other.attachedRigidbody;
            if (rb != null && !rb.isKinematic)
            {
                Vector2 direccion = (transform.position - other.transform.position);

                if (soloVertical)
                    direccion = new Vector2(0, direccion.y);

                float distancia = direccion.magnitude;
                direccion.Normalize();

                float intensidad = Mathf.Clamp01(1 - (distancia / radioDeAtraccion));
                float fuerza = fuerzaMaxima * intensidad;

                rb.AddForce(direccion * fuerza, ForceMode2D.Force);

                Debug.DrawLine(other.transform.position, transform.position, Color.red);
            }
        }
    }
}
