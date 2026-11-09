using UnityEngine;
using UnityEngine.SceneManagement;

public class TelaConclusao : MonoBehaviour
{
    // Joga o jogo de novo (recarrega a cena atual)
    public void JogarNovamente()
    {
        Time.timeScale = 1f; // volta o tempo ao normal
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Vai para a tela de início
    public void IrParaMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuInicial"); // troque pelo nome da SUA cena de menu
    }

    // Fecha o jogo
    public void Sair()
    {
        Debug.Log("Saindo do jogo..."); // aparece no Console (o Quit não funciona no editor)
        Application.Quit();
    }
}