using UnityEngine;
using UnityEngine.SceneManagement;

public class EscenaController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    static public int escena = 0; static public int nivel = 0;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Salió del juego");
            Application.Quit();
        }
    }

    public void Play()
    {
        nivel = 1; escena = 2;
        CambiarEscena(); Debug.Log("Jugar");
    }
    public void Credits()
    {
        escena = 1; CambiarEscena(); Debug.Log("Créditos");
    }
    public void Volver()
    {
        switch (nivel)
        {
            case 0: escena = -1; break;
            case 1: escena = -2; break;
            case 2: escena = -3; break;
            case 3: escena = -4; break;
        }
        CambiarEscena(); escena = 0; Debug.Log("Volvió");
    }
    public void Salir()
    {
        Application.Quit(); Debug.Log("Salió");
    }

    public void SiguienteNivel()
    {
        nivel++;
        switch (nivel)
        {
            case 2: escena = 3; break;
            case 3: escena = 4; break;
        }
        CambiarEscena(); Debug.Log("Siquiente nivel: " + nivel);
    }

    public void CambiarEscena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + escena);
        //Debug.Log("Nivel: " + nivel + " Escenario = : " + escena);
    }
}
