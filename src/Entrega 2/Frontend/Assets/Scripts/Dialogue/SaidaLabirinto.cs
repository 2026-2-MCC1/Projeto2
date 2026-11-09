using UnityEngine;
using UnityEngine.SceneManagement;

public class SaidaLabirinto : MonoBehaviour
{
    public DialogueLine[] falas = new DialogueLine[]
    {
        new DialogueLine("Tortuguita", "Conseguimos sair, agora não estamos longe da saída."),
        new DialogueLine("Player", "Certo, vamos avançar..."),
    }; 

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        DialogueManager.Instance.IniciarDialogo(falas, Terminou);
    }

    void Terminou()
    {
        
        SceneManager.LoadScene("FaseFinal");
    }
}