using UnityEngine;
using System.Collections;

// Coloque no Jogador. Liga o JogadorAndar quando a cena começa (por exemplo, ao voltar da batalha).
public class LiberarJogador : MonoBehaviour
{
    public float segundosTentando = 3f;   // por quanto tempo ele insiste em religar

    IEnumerator Start()
    {
        JogadorAndar andar = GetComponent<JogadorAndar>();
        if (andar == null) yield break;

        float tempo = 0f;
        while (tempo < segundosTentando)
        {
            if (!andar.enabled) andar.enabled = true;
            tempo += Time.deltaTime;
            yield return null;
        }
    }
}