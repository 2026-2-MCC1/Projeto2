using System.Collections;
using UnityEngine;
using TMPro;

// Mostra o objetivo atual (canto da tela) e avisos rápidos (topo, centro).
public class ObjetivoUI : MonoBehaviour
{
    public static ObjetivoUI Instance;

    public TMP_Text textoObjetivo;
    public TMP_Text textoAviso;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        textoAviso.text = "";
        AtualizarPorEstado();
    }

    // Escolhe o objetivo de acordo com o que já aconteceu no jogo
    public void AtualizarPorEstado()
    {
        if (DadosBatalha.inimigoDerrotado)
        {
            DefinirObjetivo("Vá até a porta");
        }
        else if (DadosBatalha.coletouSeteBeloFase0)
        {
            DefinirObjetivo("Siga em frente");
        }
        else
        {
            DefinirObjetivo("Explore a sala");
        }
    }

    public void DefinirObjetivo(string texto)
    {
        textoObjetivo.text = "Objetivo: " + texto;
    }

    // Mensagem que aparece por alguns segundos e some
    public void MostrarAviso(string texto, float segundos = 3f)
    {
        StopAllCoroutines();
        StartCoroutine(AvisoTemporario(texto, segundos));
    }

    IEnumerator AvisoTemporario(string texto, float segundos)
    {
        textoAviso.text = texto;
        yield return new WaitForSeconds(segundos);
        textoAviso.text = "";
    }
}
