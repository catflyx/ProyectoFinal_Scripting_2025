using NUnit.Framework;
using UnityEngine;

public class VictoriaColliderTests
{
    private GameObject player;
    private GameObject colliderVictoria;
    private GameObject escenaControllerGO;
    private MockEscenaController mockController;
    private VictoriaCollider scriptVictoria;

    // Mock del controlador de escena
    private class MockEscenaController : MonoBehaviour
    {
        public bool volverLlamado = false;
        public void Volver()
        {
            volverLlamado = true;
        }
    }

    [SetUp]
    public void Setup()
    {
        // Crear jugador
        player = new GameObject("Jugador");
        player.tag = "Player";
        player.AddComponent<Rigidbody2D>();
        player.AddComponent<BoxCollider2D>(); // NECESARIO

        // Crear el collider de victoria
        colliderVictoria = new GameObject("Victoria");
        var col = colliderVictoria.AddComponent<BoxCollider2D>();
        col.isTrigger = true; // NECESARIO
        scriptVictoria = colliderVictoria.AddComponent<VictoriaCollider>();

        // Crear mock del controlador de escena
        escenaControllerGO = new GameObject("EscenaController");
        mockController = escenaControllerGO.AddComponent<MockEscenaController>();
    }

    [Test]
    public void Al_Tocar_El_Collider_Llama_A_Volver_En_El_Controlador()
    {
        // Actuar
       

        // Afirmar
        Assert.IsTrue(mockController.volverLlamado, "El método Volver() no fue llamado al tocar el collider de victoria.");
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(player);
        Object.DestroyImmediate(colliderVictoria);
        Object.DestroyImmediate(escenaControllerGO);
    }
}
