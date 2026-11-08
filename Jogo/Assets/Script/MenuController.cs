using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void Jogar()
    {
        SceneManager.LoadScene("01_Introducao");
    }

    public void Sair()
    {
        Application.Quit();
    }
}