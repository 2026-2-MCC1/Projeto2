using UnityEngine;

public class TortuguitaFollower : MonoBehaviour
{
    [Header("Alvo a Seguir")]
    public Transform player;

    [Header("Configurações de Acompanhamento")]
    public float velocidade = 4f;
    public float distanciaMinima = 2f;
    public float velocidadeRotacao = 10f;

    void Start()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }
    }

    void Update()
    {
        if (player == null) return;
        if (DialogueManager.Instance != null && DialogueManager.Instance.EstaEmDialogo()) return;
        Vector3 alvoMesmaAltura = new Vector3(player.position.x, transform.position.y, player.position.z);
        float distancia = Vector3.Distance(transform.position, alvoMesmaAltura);

        if (distancia > distanciaMinima)
        {
            Vector3 direcao = (alvoMesmaAltura - transform.position).normalized;
            transform.position = Vector3.MoveTowards(transform.position, alvoMesmaAltura, velocidade * Time.deltaTime);
            if (direcao != Vector3.zero)
            {
                Quaternion rotacaoDesejada = Quaternion.LookRotation(direcao);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotacaoDesejada, velocidadeRotacao * Time.deltaTime);
            }
        }
    }
}