using NUnit.Framework;
using UnityEngine;

public class ImanTests
{
    private GameObject player;
    private GameObject iman;
    private ImanGravedad imanScript;

    [SetUp]
    public void Setup()
    {
        // Crear jugador con Rigidbody2D y Collider2D
        player = new GameObject("Jugador");
        player.tag = "Player"; // importante para que pase el CompareTag
        player.AddComponent<Rigidbody2D>();
        player.AddComponent<BoxCollider2D>(); // << COLLIDER AÑADIDO

        // Crear imán con su script y Collider2D (marcado como trigger)
        iman = new GameObject("Iman");
        var collider = iman.AddComponent<BoxCollider2D>();
        collider.isTrigger = true; // << importante
        imanScript = iman.AddComponent<ImanGravedad>();
    }

    [Test]
    public void Player_InvierteGravedad_AlEntrarEnIman()
    {
        var playerRb = player.GetComponent<Rigidbody2D>();
        float oldGravity = playerRb.gravityScale;

        
        

        // Verificar que la gravedad se invirtió
        Assert.AreNotEqual(oldGravity, playerRb.gravityScale, "La gravedad no cambió al entrar en el imán.");
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(player);
        Object.DestroyImmediate(iman);
    }
}
