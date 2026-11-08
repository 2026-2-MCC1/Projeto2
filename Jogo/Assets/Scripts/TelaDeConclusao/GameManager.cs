using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject painelConclusao; // arraste o painel aqui no Inspector

    public void FimDeJogo()
    {
        painelConclusao.SetActive(true); // mostra o painel
        Time.timeScale = 0f;             // pausa o jogo
    }
}