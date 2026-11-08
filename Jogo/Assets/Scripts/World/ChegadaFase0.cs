using System.Collections;
using UnityEngine;

// Fala da chegada na sala. Roda uma vez só, quando a Fase 0 começa.
public class ChegadaFase0 : MonoBehaviour
{
    public string textoControles = "W / S: andar     A / D: girar";

    public DialogueLine[] falas = new DialogueLine[]
    {
        new DialogueLine("Tortuguita", "É por aqui. Fique perto de mim e preste atenção nos doces.")
    };

    // Start pode ser uma "coroutine" (IEnumerator) para poder esperar um tempo
    IEnumerator Start()
    {
        if (DadosBatalha.chegadaMostrada) yield break;
        DadosBatalha.chegadaMostrada = true;

        yield return new WaitForSeconds(1.5f);   // espera a tela clarear

        DialogueManager.Instance.IniciarDialogo(falas, MostrarControles);
    }

    void MostrarControles()
    {
        if (ObjetivoUI.Instance != null)
        {
            ObjetivoUI.Instance.MostrarAviso(textoControles, 5f);
        }
    }
}
