using NUnit.Framework;
using UnityEngine;

public class ImanGravedadTests
{
    private GameObject imanObj;
    private ImanGravedad iman;
    private GameObject playerObj;
    private Rigidbody2D rb;
    private BoxCollider2D playerCollider;
    private BoxCollider2D imanCollider;

    [SetUp]
    public void Setup()
    {
        // Crear el objeto del imán
        imanObj = new GameObject("Iman");
        iman = imanObj.AddComponent<ImanGravedad>();
        imanCollider = imanObj.AddComponent<BoxCollider2D>();
        imanCollider.isTrigger = true;

        // Crear el jugador
        playerObj = new GameObject("Player");
        playerObj.tag = "Player";
        rb = playerObj.AddComponent<Rigidbody2D>();
        rb.gravityScale = 1f;
        playerCollider = playerObj.AddComponent<BoxCollider2D>();
    }

    [Test]
    public void Iman_InvierteGravedadAlEntrar()
    {
        // Simulamos la entrada al trigger
        iman.SendMessage("OnTriggerEnter2D", playerCollider, SendMessageOptions.DontRequireReceiver);

        // Verificamos que la gravedad fue invertida con el factor de reducción
        float expected = -Mathf.Abs(1f) * iman.factorReduccion;
        Assert.AreEqual(expected, rb.gravityScale, 0.01f, "La gravedad no se invirtió correctamente al entrar en el imán.");
    }

    [Test]
    public void Iman_RestauraGravedadAlSalir()
    {
        // Simulamos entrada y salida
        iman.SendMessage("OnTriggerEnter2D", playerCollider, SendMessageOptions.DontRequireReceiver);
        iman.SendMessage("OnTriggerExit2D", playerCollider, SendMessageOptions.DontRequireReceiver);

        // Verificamos que se restauró la gravedad original
        Assert.AreEqual(1f, rb.gravityScale, 0.01f, "La gravedad no se restauró correctamente al salir del imán.");
    }

    [TearDown]
    public void Cleanup()
    {
        Object.DestroyImmediate(imanObj);
        Object.DestroyImmediate(playerObj);
    }
}
