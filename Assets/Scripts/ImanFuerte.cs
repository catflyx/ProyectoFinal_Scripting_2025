using UnityEngine;

public class ImanFuerte : ImanBase
{
    protected override float CalcularFuerza(float distancia)
    {
        // Caída lenta de fuerza (afecta más a larga distancia)
        float intensidad = Mathf.Clamp01(1 - Mathf.Pow(distancia / radioDeAtraccion, 0.5f));
        return fuerzaMaxima * 2f * intensidad; // Doble de fuerza base
    }
}
