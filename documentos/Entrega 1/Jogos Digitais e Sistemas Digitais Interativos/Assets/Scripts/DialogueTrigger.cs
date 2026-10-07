using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    public List<DialogueLine> dialogo;
    public bool iniciarNoStart = false;
    public UnityEvent aoConcluirDialogo;

    void Start()
    {
        if (iniciarNoStart)
        {
            DispararDialogo();
        }
    }

    public void DispararDialogo()
    {
        DialogueManager.Instance.IniciarDialogo(dialogo, aoConcluirDialogo);
    }
}