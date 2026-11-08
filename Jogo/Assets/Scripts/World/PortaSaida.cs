using UnityEngine;
using UnityEngine.SceneManagement;

// Zona na frente da porta (Collider com "Is Trigger").
// Fechada: avisa que está trancada. Aberta: leva o jogador para a próxima fase.
public class PortaSaida : MonoBehaviour
{
    public GameObject bloqueio;               // o objeto que fecha a passagem (some quando abre)
    public string cenaSeguinte = "04_Fase1";  // nome EXATO da próxima cena

    private bool aberta = false;
    private bool saindo = false;

    void Start()
    {
        // Se voltou da batalha e a porta já estava aberta, abre direto
        if (DadosBatalha.portaAberta)
        {
            Abrir();
        }
    }

    public void Abrir()
    {
        aberta = true;
        DadosBatalha.portaAberta = true;

        if (bloqueio != null)
        {
            bloqueio.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (saindo) return;

        // Se a batalha já destrancou a porta, abre esta também
        if (!aberta && DadosBatalha.portaAberta)
        {
            Abrir();
        }
        if (!aberta)
        {
            if (ObjetivoUI.Instance != null)
            {
                ObjetivoUI.Instance.MostrarAviso("A porta está trancada.");
            }
            return;
        }

        // Evita erro se a próxima fase ainda não existir
        if (!Application.CanStreamedLevelBeLoaded(cenaSeguinte))
        {
            if (ObjetivoUI.Instance != null)
            {
                ObjetivoUI.Instance.MostrarAviso("A cena '" + cenaSeguinte + "' ainda não existe no Build Settings.", 4f);
            }
            return;
        }

        saindo = true;

        if (FadeTela.Instance != null)
        {
            FadeTela.Instance.FadeOutECarregar(cenaSeguinte);
        }
        else
        {
            SceneManager.LoadScene(cenaSeguinte);
        }
    }
}
