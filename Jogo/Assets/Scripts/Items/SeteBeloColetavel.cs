using UnityEngine;

// Coloque no 7Belo do mapa (Collider com "Is Trigger" marcado).
public class SeteBeloColetavel : MonoBehaviour
{
    public DialogueLine[] falas = new DialogueLine[]
    {
        new DialogueLine("Tortuguita", "Você encontrou uma bala 7Belo. Dizem que é a melhor bala do mundo, tenho certeza que quando você precisar de força é só comer ela que vai ajudar.")
    };
    public GameObject inimigo;   // arraste o inimigo aqui
    private bool coletado = false;

    void Start()
    {
        if (DadosBatalha.coletouSeteBeloFase0)
        {
            // Se já pegou o 7Belo antes (voltou da batalha), o inimigo precisa estar ativo
            if (inimigo != null)
            {
                inimigo.SetActive(true);
            }
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (coletado) return;
        if (!other.CompareTag("Player")) return;

        coletado = true;
        DadosBatalha.qtdSeteBelo = DadosBatalha.qtdSeteBelo + 1;
        DadosBatalha.coletouSeteBeloFase0 = true;

        // Esconde o item, mas o script continua vivo até o diálogo acabar
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            r.enabled = false;
        }
        GetComponent<Collider>().enabled = false;

        if (ObjetivoUI.Instance != null)
        {
            ObjetivoUI.Instance.MostrarAviso("Você pegou: 7Belo");
        }

        DialogueManager.Instance.IniciarDialogo(falas, AposDialogo);
    }

    void AposDialogo()
    {
        if (ObjetivoUI.Instance != null)
        {
            ObjetivoUI.Instance.AtualizarPorEstado();
        }
        if (inimigo != null)
        {
            inimigo.SetActive(true);
        }
        
    }
}
