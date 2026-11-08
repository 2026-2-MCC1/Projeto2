using System;
using UnityEngine;
using UnityEngine.Serialization;

// Uma fala: quem fala e o que fala.
// Como é [Serializable], aparece no Inspector e você pode editar as falas lá.
// Use "Player" no campo personagem para aparecer o nome do jogador.
[Serializable]
public class DialogueLine
{
    [FormerlySerializedAs("personagem")]
    public string nomePersonagem;

    [TextArea(3, 5)]
    public string texto;

    public DialogueLine(string personagem, string texto)
    {
        nomePersonagem = personagem;
        this.texto = texto;
    }
}
