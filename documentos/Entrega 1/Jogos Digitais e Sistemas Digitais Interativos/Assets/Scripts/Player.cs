using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    public float velocidade = 5f;
    public float velocidadeRotacao = 10f;

    private Rigidbody rb;
    private Vector3 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Trava a rotação física para o personagem não tombar ao colidir
        rb.freezeRotation = true;
    }

    void Update()
    {
        // Se estiver num diálogo, para o movimento
        if (DialogueManager.Instance != null && DialogueManager.Instance.EstaEmDialogo())
        {
            moveInput = Vector3.zero;
            return;
        }

        // Lê teclas WASD ou Setas
        float moveX = Input.GetAxisRaw("Horizontal"); // A/D (Esquerda / Direita)
        float moveZ = Input.GetAxisRaw("Vertical");   // W/S (Frente / Trás)

        // Cria o vetor de movimento no plano XZ
        moveInput = new Vector3(moveX, 0f, moveZ).normalized;
    }

    void FixedUpdate()
    {
        if (moveInput.sqrMagnitude > 0)
        {
            // Aplica o movimento 3D no Rigidbody
            Vector3 targetPosition = rb.position + moveInput * velocidade * Time.fixedDeltaTime;
            rb.MovePosition(targetPosition);

            // Roda o modelo do jogador para a direção em que está a andar
            Quaternion targetRotation = Quaternion.LookRotation(moveInput);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, Time.fixedDeltaTime * velocidadeRotacao));
        }
    }
}