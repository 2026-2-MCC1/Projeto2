using UnityEngine;
using UnityEngine.SceneManagement;

public class MudarCena : MonoBehaviour
{
    public void CarregarCena(string Fase_1)
    {
        SceneManager.LoadScene("Fase_1");
    }
}