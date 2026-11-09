using UnityEngine;

// Coloque no Player da Fase 0.
// Quando o jogador volta da batalha, ele reaparece onde estava.
public class RestaurarPosicaoJogador : MonoBehaviour
{
    void Start()
    {
        if (!DadosBatalha.temPosicaoRetorno) return;

        // CharacterController "briga" com mudança de posição, então desligamos um instante
        CharacterController controle = GetComponent<CharacterController>();
        if (controle != null)
        {
            controle.enabled = false;
        }

        transform.position = DadosBatalha.posicaoRetorno;

        if (controle != null)
        {
            controle.enabled = true;
        }
    }
}
