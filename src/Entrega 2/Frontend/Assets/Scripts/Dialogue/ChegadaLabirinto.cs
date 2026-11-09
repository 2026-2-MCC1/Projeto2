using System.Collections;
using UnityEngine;

public class ChegadaLabirinto : MonoBehaviour
{
    public DialogueLine[] falas = new DialogueLine[]
    {
        new DialogueLine("Tortuguita", "Agora precisamos passar pelo labirinto. Não consigo me lembrar do caminho correto, mas lembro que não é tão difícil."),
        new DialogueLine("Player", "Entendi, vamos pela tentativa e erro.")
    };

    IEnumerator Start()
    {
        if (DadosBatalha.chegadaLabirintoMostrada) yield break;
        DadosBatalha.chegadaLabirintoMostrada = true;

        yield return new WaitForSeconds(1.5f);

        DialogueManager.Instance.IniciarDialogo(falas, Terminou);
    }

    void Terminou()
    {
        if (ObjetivoUI.Instance != null)
            ObjetivoUI.Instance.MostrarAviso("Encontre a saída do labirinto", 5f);
    }
}