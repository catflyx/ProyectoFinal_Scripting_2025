using UnityEngine;

public class ImanConstante : ImanBase
{
    protected override float CalcularFuerza(float distancia)
    {
        // Fuerza constante: no depende de la distancia
        return fuerzaMaxima;
    }
}
