using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("Elementos de UI")]
    public GameObject painelDialogo;
    public TextMeshProUGUI textoNome;
    public TextMeshProUGUI textoFala;
    public Button botaoAvancar; 

    private Queue<DialogueLine> falas = new Queue<DialogueLine>();
    private Action aoFinalizarDialogo;
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

    public void IniciarDialogo(IEnumerable<DialogueLine> listaFalas, UnityEvent eventoFinal = null)
    {
        IniciarDialogoInterno(listaFalas, eventoFinal == null ? null : eventoFinal.Invoke);
    }

    public void IniciarDialogo(IEnumerable<DialogueLine> listaFalas, Action eventoFinal)
    {
        IniciarDialogoInterno(listaFalas, eventoFinal);
    }

    private void IniciarDialogoInterno(IEnumerable<DialogueLine> listaFalas, Action eventoFinal)
    {
        falas.Clear();
        foreach (var fala in listaFalas)
        {
            string nomeDoPlayer = PlayerPrefs.GetString("NomeJogador", "Jogador");
            DialogueLine linhaProcessada = new DialogueLine(
                fala.nomePersonagem.Replace("{Player}", nomeDoPlayer),
                fala.texto.Replace("{Player}", nomeDoPlayer));
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

    public void TravarJogador(bool travar)
    {
        GameObject jogador = GameObject.FindGameObjectWithTag("Player");
        if (jogador == null) return;

        PlayerMovement movimento = jogador.GetComponent<PlayerMovement>();
        if (movimento != null)
        {
            movimento.enabled = !travar;
        }

        JogadorAndar controle = jogador.GetComponent<JogadorAndar>();
        if (controle != null)
        {
            controle.enabled = !travar;
        }
    }
}