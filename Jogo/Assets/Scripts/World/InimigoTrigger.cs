using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// Coloque no inimigo da Fase 0 (o objeto precisa de um Collider com "Is Trigger" marcado).
public class InimigoTrigger : MonoBehaviour
{
    public DialogueLine[] falas = new DialogueLine[]
    {
        new DialogueLine("Player", "O que seria isso?"),
        new DialogueLine("Tortuguita", "Eu avisei que os doces iriam estranhar uma pessoa aqui dentro.")
    };

    private bool jaAtivou = false;

    void Start()
    {
        // Se o inimigo já foi derrotado, ele some quando a cena recarrega
        if (DadosBatalha.inimigoDerrotado)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (jaAtivou) return;
        if (!other.CompareTag("Player")) return;

        // O jogador precisa pegar o 7Belo antes de lutar
        if (!DadosBatalha.coletouSeteBeloFase0)
        {
            if (ObjetivoUI.Instance != null)
            {
                ObjetivoUI.Instance.MostrarAviso("Procure o 7Belo na sala antes de seguir!");
            }
            return;
        }

        jaAtivou = true;

        // Guarda os dados para a batalha e para a volta
        DadosBatalha.cenaDeRetorno = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        DadosBatalha.posicaoRetorno = other.transform.position;
        DadosBatalha.temPosicaoRetorno = true;
        DadosBatalha.seteBeloAntesDaBatalha = DadosBatalha.qtdSeteBelo;

        StartCoroutine(Sequencia(other.transform));
    }

    IEnumerator Sequencia(Transform jogador)
    {
        DialogueManager.Instance.TravarJogador(true);

        // Vira o doce para o jogador
        Vector3 alvo = jogador.position;
        alvo.y = transform.position.y;
        transform.LookAt(alvo);

        // Tremidinha: o doce "acorda"
        Vector3 posicaoInicial = transform.position;
        float tempo = 0f;
        while (tempo < 0.6f)
        {
            tempo = tempo + Time.deltaTime;
            transform.position = posicaoInicial + transform.right * Mathf.Sin(tempo * 60f) * 0.05f;
            yield return null;
        }
        transform.position = posicaoInicial;

        DialogueManager.Instance.IniciarDialogo(falas, IrParaBatalha);
    }

    void IrParaBatalha()
    {
        // Mantém o jogador parado durante o fade
        DialogueManager.Instance.TravarJogador(true);

        if (FadeTela.Instance != null)
        {
            FadeTela.Instance.FadeOutECarregar("03_Batalha");
        }
        else
        {
            SceneManager.LoadScene("03_Batalha");
        }
    }
}
