using UnityEngine;

public class JogadorAndar : MonoBehaviour
{
    public float speed = 10f;
    public float mouseSensitivity = 200f;
    public Transform cam;
    private float rotationX = 0f;

    private CharacterController controller;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // --- 1. MOUSE (Câmera) ---
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -70f, 70f);

        if (cam != null)
        {
            cam.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        }

        // --- 2. MOVIMENTO (WASD) ---
        float moveX = 0f;
        float moveZ = 0f;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) moveZ = 1f;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) moveZ = -1f;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) moveX = -1f;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) moveX = 1f;

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // --- 3. IMPEDE A QUEDA ---
        // Pegamos a posição atual do boneco e travamos o eixo Y para ele não cair
        Vector3 movimentoFinal = move * speed;

        // Move o personagem respeitando as paredes
        controller.Move(movimentoFinal * Time.deltaTime);

        // Força a altura (Y) a ficar sempre igual, impedindo que ele caia no infinito
        Vector3 posicaoFixa = transform.position;
        posicaoFixa.y = 1f; // Mude este número se o seu chão estiver mais alto ou mais baixo (ex: 0, 1, 2)
        transform.position = posicaoFixa;
    }
}