using System.Collections;
using UnityEngine;

// Faz o personagem correr até o alvo, bater, e voltar.
public class EfeitosBatalha : MonoBehaviour
{
    [Header("Personagens (objetos 3D da cena)")]
    public Transform jogador;
    public Transform inimigo;

    [Header("Partículas de impacto (OPCIONAL)")]
    public ParticleSystem impactoNoInimigo;
    public ParticleSystem impactoNoJogador;

    [Header("Animators (OPCIONAL - pode deixar vazio)")]
    public Animator animadorJogador;
    public Animator animadorInimigo;

    [Header("Jeito de atacar (pode mexer à vontade)")]
    public float distanciaParada = 1.5f;   // a que distância do alvo ele para
    public float tempoIda = 0.45f;         // tempo para chegar no alvo
    public float tempoImpacto = 0.3f;      // tempo parado batendo (alvo treme)
    public float tempoVolta = 0.5f;        // tempo para voltar
    public float alturaPulo = 0.6f;        // altura do pulinho na ida
    public float forcaTremida = 0.15f;     // força do tremor do alvo

    public void AtaqueDoJogador()
    {
        StartCoroutine(Investida(jogador, inimigo, impactoNoInimigo, animadorJogador));
    }

    public void AtaqueDoInimigo()
    {
        StartCoroutine(Investida(inimigo, jogador, impactoNoJogador, animadorInimigo));
    }

    IEnumerator Investida(Transform atacante, Transform alvo, ParticleSystem impacto, Animator animador)
    {
        Debug.Log("ATAQUE: " + atacante.name + " -> " + alvo.name + " | posições: " + atacante.position + " -> " + alvo.position);
        if (animador != null)
        {
            animador.SetTrigger("Atacar");
        }

        Vector3 inicio = atacante.position;

        // Descobre o ponto onde ele vai parar, perto do alvo
        Vector3 direcao = alvo.position - inicio;
        direcao.y = 0f;
        float distancia = direcao.magnitude;

        Vector3 destino = inicio;
        if (distancia > distanciaParada)
        {
            destino = inicio + direcao.normalized * (distancia - distanciaParada);
        }

        // IDA (com pulinho)
        float t = 0f;
        while (t < 1f)
        {
            t = t + Time.deltaTime / tempoIda;
            float progresso = Mathf.Clamp01(t);

            Vector3 pos = Vector3.Lerp(inicio, destino, progresso);
            pos.y = inicio.y + Mathf.Sin(progresso * Mathf.PI) * alturaPulo;
            atacante.position = pos;
            yield return null;
        }
        atacante.position = destino;

        // IMPACTO
        if (impacto != null)
        {
            impacto.Play();
        }
        yield return StartCoroutine(Tremer(alvo, tempoImpacto));

        // VOLTA
        t = 0f;
        while (t < 1f)
        {
            t = t + Time.deltaTime / tempoVolta;
            atacante.position = Vector3.Lerp(destino, inicio, Mathf.Clamp01(t));
            yield return null;
        }
        atacante.position = inicio;
    }

    // Faz o alvo tremer um pouquinho quando leva a pancada
    IEnumerator Tremer(Transform alvo, float duracao)
    {
        Vector3 posicaoOriginal = alvo.position;
        float tempo = 0f;

        while (tempo < duracao)
        {
            tempo = tempo + Time.deltaTime;
            Vector3 desvio = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)) * forcaTremida;
            alvo.position = posicaoOriginal + desvio;
            yield return null;
        }

        alvo.position = posicaoOriginal;
    }
}