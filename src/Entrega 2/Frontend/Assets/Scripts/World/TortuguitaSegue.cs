using UnityEngine;

// Faz a Tortuguita seguir o jogador, sempre a uma certa distância.
public class TortuguitaSegue : MonoBehaviour
{
    public Transform alvo;               // arraste o Jogador aqui
    public float distancia = 2f;         // distância que ela mantém
    public float velocidade = 5f;        // um pouco maior que a do jogador
    public float velocidadeGiro = 6f;

    void Update()
    {
        if (alvo == null) return;

        Vector3 direcao = alvo.position - transform.position;
        direcao.y = 0f;                  // ignora altura
        float dist = direcao.magnitude;

        // Anda até chegar perto
        if (dist > distancia)
        {
            transform.position = transform.position + direcao.normalized * velocidade * Time.deltaTime;
        }

        // Vira para o jogador
        if (dist > 0.1f)
        {
            Quaternion rotacaoAlvo = Quaternion.LookRotation(direcao);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacaoAlvo, velocidadeGiro * Time.deltaTime);
        }
    }
}
