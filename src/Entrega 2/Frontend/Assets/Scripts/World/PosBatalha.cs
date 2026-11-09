using System.Collections;
using UnityEngine;

// Quando o jogador volta da batalha vencendo, mostra as falas e abre a porta.
public class PosBatalha : MonoBehaviour
{
    public PortaSaida porta;

    public DialogueLine[] falas = new DialogueLine[]
    {
        new DialogueLine("Tortuguita", "Essa foi por pouco. Ainda bem que você conseguiu derrotá-la."),
        new DialogueLine("Player", "Vamos, preciso continuar."),
        new DialogueLine("Tortuguita", "A porta abriu! O labirinto fica logo depois.")
    };

    IEnumerator Start()
    {
        if (!DadosBatalha.inimigoDerrotado) yield break;
        if (DadosBatalha.dialogoPosBatalhaMostrado) yield break;
        DadosBatalha.dialogoPosBatalhaMostrado = true;

        yield return new WaitForSeconds(1.5f);   // espera a tela clarear

        if (ObjetivoUI.Instance != null)
        {
            ObjetivoUI.Instance.MostrarAviso("Você recebeu: Saco de 7Belo", 4f);
        }

        DialogueManager.Instance.IniciarDialogo(falas, AbrirPorta);
    }

    void AbrirPorta()
    {
        // Garante que a porta fica aberta mesmo que o campo "Porta" esteja vazio
        DadosBatalha.portaAberta = true;

        if (porta != null)
        {
            porta.Abrir();
        }

        if (ObjetivoUI.Instance != null)
        {
            ObjetivoUI.Instance.AtualizarPorEstado();
        }
    }
}
