using UnityEngine;

// Toca a animação "Andando" quando o personagem se move e "Parado" quando para.
// Coloque no objeto PAI (Jogador ou Tortuguita). Funciona sem setas no Animator.
public class AnimacaoMovimento : MonoBehaviour
{
    public Animator animador;                  // se deixar vazio, procura nos filhos
    public string estadoParado = "Parado";     // nome da caixa de parado
    public string estadoAndando = "Andando";   // nome da caixa de andando
    public float velocidadeMinima = 0.3f;      // abaixo disso conta como parado
    public float tempoParaParar = 0.3f;        // espera isso sem se mexer antes de ficar parado
    public float tempoMistura = 0.15f;         // suavidade da troca
    public bool mostrarMensagens = false;      // marque para ver mensagens no Console

    private Vector3 posicaoAnterior;
    private bool andandoAntes = false;
    private float ultimoMovimento = -100f;

    void Start()
    {
        posicaoAnterior = transform.position;

        if (animador == null)
        {
            animador = GetComponentInChildren<Animator>();
        }

        if (animador != null)
        {
            animador.Play(estadoParado);
        }
    }

    void Update()
    {
        if (Time.deltaTime <= 0f) return;
        if (animador == null) return;

        // Só conta o movimento no chão (ignora subir e descer)
        Vector3 deslocamento = transform.position - posicaoAnterior;
        deslocamento.y = 0f;
        float velocidade = deslocamento.magnitude / Time.deltaTime;
        posicaoAnterior = transform.position;

        // Guarda a última vez que ele se mexeu
        if (velocidade > velocidadeMinima)
        {
            ultimoMovimento = Time.time;
        }

        // Fica "andando" até passar um tempinho sem se mexer
        bool andando = (Time.time - ultimoMovimento) < tempoParaParar;

        // Só troca quando muda (parado <-> andando)
        if (andando != andandoAntes)
        {
            if (mostrarMensagens)
            {
                Debug.Log(name + (andando ? ": ANDANDO" : ": PARADO"));
            }

            if (andando)
            {
                animador.CrossFade(estadoAndando, tempoMistura);
            }
            else
            {
                animador.CrossFade(estadoParado, tempoMistura);
            }
            andandoAntes = andando;
        }
    }
}