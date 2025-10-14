using NUnit.Framework;
using UnityEngine;

public class PlayerMovementTests
{
    private GameObject player;
    private PlayerController movement;
    private Rigidbody2D rb;

    [SetUp]
    public void Setup()
    {
        player = new GameObject();
        rb = player.AddComponent<Rigidbody2D>();
        movement = player.AddComponent<PlayerController>();
        movement.jumpForce = 10f;
        // Configurar respawn point opcional
        var respawn = new GameObject("RespawnPoint").transform;
        respawn.position = Vector3.zero;
        movement.respawnPoint = respawn;
    }

    [Test]
    public void Player_Jumps_When_CanGroundJump_Is_True()
    {
        //  Forzamos el estado manualmente, ya que canGroundJump es privado
        typeof(PlayerController)
            .GetField("canGroundJump", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(movement, true);

        float oldVelocity = rb.linearVelocity.y;

        // Invocamos el método privado JumpToMouse()
        movement.SendMessage("JumpToMouse");

        Assert.Greater(rb.linearVelocity.y, oldVelocity, "El jugador no saltó correctamente.");
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(player);
    }
}