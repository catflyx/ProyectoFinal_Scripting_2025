using UnityEngine;

public class ImanInverso : ImanBase
{
    protected override float CalcularFuerza(float distancia)
    {
        // Misma fórmula que el base, pero fuerza negativa = repulsión
        float intensidad = Mathf.Clamp01(1 - (distancia / radioDeAtraccion));
        return -fuerzaMaxima * intensidad;
    }
}
