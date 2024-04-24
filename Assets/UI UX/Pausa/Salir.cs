using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorDeBoton : MonoBehaviour
{
    public void IrAStartGame()
    {
        SceneManager.LoadScene("Scenes/Start Game"); // Carga la escena llamada "StartGame" ubicada en la carpeta "Scenes"
    }
}
