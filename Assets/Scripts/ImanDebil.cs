using UnityEngine;

public class ImanDebil : ImanBase
{
    protected override float CalcularFuerza(float distancia)
    {
        // Caída rápida (solo sientes atracción si estás casi encima)
        float intensidad = Mathf.Clamp01(1 - Mathf.Pow(distancia / radioDeAtraccion, 2f));
        return fuerzaMaxima * 0.5f * intensidad;
    }
}
