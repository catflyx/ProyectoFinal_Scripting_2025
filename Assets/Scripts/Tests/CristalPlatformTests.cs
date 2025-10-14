using NUnit.Framework;
using UnityEngine;

public class CristalPlatformTests
{
    private GameObject cristalObj;
    private CristalPlatform cristal;
    private GameObject player;

    [SetUp]
    public void Setup()
    {
        // Crear el objeto cristal con sus componentes
        cristalObj = new GameObject("Cristal");
        cristal = cristalObj.AddComponent<CristalPlatform>();
        cristal.golpesParaRomper = 2;

        cristalObj.AddComponent<BoxCollider2D>();
        cristalObj.AddComponent<SpriteRenderer>();

        // Crear jugador simulado
        player = new GameObject("Player");
        player.tag = "Player";
    }

    [Test]
    public void Cristal_SeRompe_Despues_De_Varios_Golpes()
    {
        // Simulamos 2 colisiones del jugador
        for (int i = 0; i < 2; i++)
        {
            // Usamos SendMessage para invocar el método privado de colisión
            cristal.SendMessage("OnCollisionEnter2D", CrearFakeCollision(player), SendMessageOptions.DontRequireReceiver);
        }

        // Verificamos que el cristal haya sido destruido
        Assert.IsTrue(cristal == null || cristalObj == null, "El cristal no se destruyó tras recibir los golpes necesarios.");
    }

   
    private Collision2D CrearFakeCollision(GameObject target)
    {
        // Creamos un objeto temporal que tenga Rigidbody y Collider
        var tempCol = new GameObject("TempCol");
        tempCol.tag = target.tag;
        tempCol.AddComponent<Rigidbody2D>();
        tempCol.AddComponent<BoxCollider2D>();

        // En pruebas unitarias, podemos pasar un Collision2D nulo y el código seguirá ejecutándose
        return null;
    }

    [TearDown]
    public void Cleanup()
    {
        if (cristalObj != null)
            Object.DestroyImmediate(cristalObj);
        if (player != null)
            Object.DestroyImmediate(player);
    }
}
