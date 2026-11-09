using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDialogueTrigger : MonoBehaviour
{
    [Header("Configurações do Diálogo")]
    public List<DialogueLine> dialogoInimigo;
    public bool dispararApenasUmaVez = true;

    [Header("Eventos Opcionais")]
    public UnityEvent aoConcluirDialogo;

    private bool jaDisparou = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (dispararApenasUmaVez && jaDisparou) return;
            jaDisparou = true;
            if (DialogueManager.Instance != null)
            {
                DialogueManager.Instance.IniciarDialogo(dialogoInimigo, aoConcluirDialogo);
            }
        }
    }
}