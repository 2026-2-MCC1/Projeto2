using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Alvo a Seguir")]
    public Transform target;

    [Header("Configurações da Câmara")]
    public Vector3 offset = new Vector3(0f, 8f, -7f); 
    public float velocidadeSuavizacao = 5f;          

    void LateUpdate()
    {
        if (target == null) return;
        Vector3 posicaoDesejada = target.position + offset;
        Vector3 posicaoSuave = Vector3.Lerp(transform.position, posicaoDesejada, velocidadeSuavizacao * Time.deltaTime);
        transform.position = posicaoSuave;
        transform.LookAt(target.position);
    }
}