using UnityEngine;

// Vira o modelo do personagem para a direção em que ele está andando.
// Coloque no objeto PAI (Jogador). Não mexe no script de andar nem na câmera.
public class VirarParaDirecao : MonoBehaviour
{
    public Transform modelo;               // o desenho do personagem (o filho)
    public float velocidadeGiro = 12f;     // quanto maior, mais rápido ele vira
    public float anguloAjuste = 0f;        // se o modelo olhar torto, tente 90, -90 ou 180

    private Vector3 posicaoAnterior;

    void Start()
    {
        posicaoAnterior = transform.position;

        if (modelo == null && transform.childCount > 0)
        {
            modelo = transform.GetChild(0);
        }
    }

    void Update()
    {
        // Para onde o personagem andou neste frame (sem contar subir e descer)
        Vector3 deslocamento = transform.position - posicaoAnterior;
        deslocamento.y = 0f;
        posicaoAnterior = transform.position;

        // Parado: mantém para onde estava olhando
        if (deslocamento.sqrMagnitude < 0.00001f) return;
        if (modelo == null) return;

        Quaternion alvo = Quaternion.LookRotation(deslocamento.normalized) * Quaternion.Euler(0f, anguloAjuste, 0f);
        modelo.rotation = Quaternion.Slerp(modelo.rotation, alvo, velocidadeGiro * Time.deltaTime);
    }
}