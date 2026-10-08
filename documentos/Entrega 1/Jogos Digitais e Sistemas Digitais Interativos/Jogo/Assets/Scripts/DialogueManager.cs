using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

[System.Serializable]
public class DialogueLine
{
    public string nomePersonagem;
    [TextArea(3, 5)]
    public string texto;
}

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("Elementos de UI")]
    public GameObject painelDialogo;
    public TextMeshProUGUI textoNome;
    public TextMeshProUGUI textoFala;
    public Button botaoAvancar; 

    private Queue<DialogueLine> falas = new Queue<DialogueLine>();
    private UnityEvent aoFinalizarDialogo;
    private bool emDialogo = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        if (botaoAvancar != null)
        {
            botaoAvancar.onClick.AddListener(ProximaFala);
        }
        if (painelDialogo != null)
        {
            painelDialogo.SetActive(false);
        }
    }

    void Update()
    {
        if (emDialogo && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)))
        {
            ProximaFala();
        }
    }

    public void IniciarDialogo(List<DialogueLine> listaFalas, UnityEvent eventoFinal = null)
    {
        falas.Clear();
        foreach (var fala in listaFalas)
        {
            string nomeDoPlayer = PlayerPrefs.GetString("NomeJogador", "Jogador");
            DialogueLine linhaProcessada = new DialogueLine
            {
                nomePersonagem = fala.nomePersonagem.Replace("{Player}", nomeDoPlayer),
                texto = fala.texto.Replace("{Player}", nomeDoPlayer)
            };
            falas.Enqueue(linhaProcessada);
        }

        aoFinalizarDialogo = eventoFinal;
        
        if (painelDialogo != null)
        {
            painelDialogo.SetActive(true);
        }
        
        emDialogo = true;

        ProximaFala();
    }

    public void ProximaFala()
    {
        if (falas.Count == 0)
        {
            FinalizarDialogo();
            return;
        }

        DialogueLine falaAtual = falas.Dequeue();
        textoNome.text = falaAtual.nomePersonagem;
        textoFala.text = falaAtual.texto;
    }

    void FinalizarDialogo()
    {
        if (painelDialogo != null)
        {
            painelDialogo.SetActive(false);
        }
        
        emDialogo = false;
        
        aoFinalizarDialogo?.Invoke();
    }

    public bool EstaEmDialogo()
    {
        return emDialogo;
    }
}