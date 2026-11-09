using System;
using UnityEngine;

public class GuardaController : MonoBehaviour
{
    [Header("Referências")]
    public GameObject portaBloqueio; 

    private bool jogadorNaZona = false;

    private void Start()
    {
        // Teste: simula o Bonobon que a fase 2 vai entregar.
        // Remova esta linha quando a fase 2 estiver pronta.
        DadosFaseFinal.bonobonObtido = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) jogadorNaZona = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) jogadorNaZona = false;
    }

    private void Update()
    {
        if (!jogadorNaZona) return;
        if (DialogueManager.Instance.EmDialogo) return;
        if (Input.GetKeyDown(KeyCode.E)) Interagir();
    }

    private void Interagir()
    {
        if (DadosFaseFinal.passagemLiberada)
        {
            Falar(FalasDespedida());
            return;
        }

        if (!DadosFaseFinal.bonobonObtido) return;

        DadosFaseFinal.bonobonObtido = false;
        DadosFaseFinal.passagemLiberada = true;
        Falar(FalasEntrega(), AbrirPassagem);
    }

    private void Falar(DialogueLine[] falas, Action aoTerminar = null)
    {
        if (aoTerminar == null) aoTerminar = () => { };
        DialogueManager.Instance.IniciarDialogo(falas, aoTerminar);
    }

    private void AbrirPassagem()
    {
        if (portaBloqueio != null) portaBloqueio.SetActive(false);
    }

    private DialogueLine[] FalasEntrega()
    {
        return new DialogueLine[]
        {
             new DialogueLine("Guarda", "Ei! O que você está fazendo por aqui? Esta área é restrita!"),
            new DialogueLine("Tortuguita", "Ele precisa sair daqui, precisa voltar para o ônibus."),
            new DialogueLine("Guarda", "Um ônibus? Tem noção do perigo que acabou de atravessar no labirinto?"),
            new DialogueLine("Tortuguita", "Ele mandou super bem! Agora abre logo essa porta, por favor."),
            new DialogueLine("Guarda", "Hahaha, tudo bem, quem sou eu para discutir com uma tartaruga."),
            new DialogueLine("Guarda", "A sua passagem está liberada. Pode ir em frente e boa viagem!")
        };
    }

    private DialogueLine[] FalasDespedida()
    {
        return new DialogueLine[]
        {
            new DialogueLine("Guarda", "Ainda aqui? A excursão está esperando lá fora."),
            new DialogueLine("Tortuguita", "Foi uma honra ajudar você. Não esquece da gente, tá?")
        };
    }
}