using UnityEngine;

public class SaidaLabirinto : MonoBehaviour
{
    public DialogueLine[] falas = new DialogueLine[]
    {
        new DialogueLine("Tortuguita", "Conseguimos sair, agora não estamos longe da saída."),
        new DialogueLine("Player", "Certo, vamos avançar..."),
        new DialogueLine("BLOCK", "Tuhn.... tuhn...."),
        new DialogueLine("Player", "O que é isso?"),
        new DialogueLine("Tortuguita", "Esse é o BLOCK, o maciço. Ele é feito de chocolate rígido e é exageradamente grande.")
    };

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (DadosBatalha.saidaLabirintoMostrada) return;
        DadosBatalha.saidaLabirintoMostrada = true;

        DialogueManager.Instance.IniciarDialogo(falas, Terminou);
    }

    void Terminou()
    {
        // Próximo passo: o BLOCK aparece e começa a batalha.
        Debug.Log("Fim das falas da saída. Aqui entra a luta com o BLOCK.");
    }
}