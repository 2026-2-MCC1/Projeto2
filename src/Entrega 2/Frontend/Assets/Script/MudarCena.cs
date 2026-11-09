using UnityEngine;
using UnityEngine.SceneManagement;

public class MudarCena : MonoBehaviour
{
    public void CarregarCena()
    {
        SceneManager.LoadScene("02_Fase0");
    }
}