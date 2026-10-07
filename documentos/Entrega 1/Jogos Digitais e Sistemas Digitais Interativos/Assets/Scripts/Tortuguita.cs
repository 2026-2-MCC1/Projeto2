using UnityEngine;

public class TortuguitaFollower : MonoBehaviour
{
    [Header("Alvo a Seguir")]
    public Transform player;

    [Header("Configurações de Acompanhamento")]
    public float velocidade = 4f;
    public float distanciaMinima = 2f; // Distância mínima para não colar no player

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }
    }

    void FixedUpdate()
    {
        if (player == null) return;
        if (DialogueManager.Instance != null && DialogueManager.Instance.EstaEmDialogo()) return;

        // Calcula a distância 3D entre a Tortuguita e o Player
        float distancia = Vector3.Distance(transform.position, player.position);

        if (distancia > distanciaMinima)
        {
            // Calcula a direção mantendo a altura (Y) zerada para ela não flutuar
            Vector3 direcao = (player.position - transform.position).normalized;
            direcao.y = 0f; 

            rb.MovePosition(rb.position + direcao * velocidade * Time.fixedDeltaTime);
        }
    }
}