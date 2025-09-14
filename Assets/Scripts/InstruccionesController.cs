using TMPro;
using UnityEngine;

public class InstruccionesController : MonoBehaviour
{
    public TextMeshProUGUI Text;

    bool IsTutorial = false;
    public GameObject Timer;

    void Start()
    {
        gameObject.SetActive(true);
        IsTutorial = true; Timer.SetActive(false);
    }

    void Update()
    {
        if (IsTutorial)
        {
            Text.text = "Da click a la pantalla";

            // Si se da click en cualquier parte
            if (Input.GetMouseButtonDown(0))
            {
                gameObject.SetActive(false);
                IsTutorial = false; Timer.SetActive(true);
            }
        }
    }
}
