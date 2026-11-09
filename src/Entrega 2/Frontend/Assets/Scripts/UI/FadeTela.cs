using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// Tela preta que clareia quando a cena começa e escurece antes de trocar de cena.
// Coloque numa Image preta que cobre a tela toda.
[RequireComponent(typeof(CanvasGroup))]
public class FadeTela : MonoBehaviour
{
    public static FadeTela Instance;

    public float duracao = 1f;

    private CanvasGroup grupo;

    void Awake()
    {
        Instance = this;

        grupo = GetComponent<CanvasGroup>();
        grupo.alpha = 1f;               // começa tudo preto
        grupo.blocksRaycasts = false;   // não atrapalha os cliques
        grupo.interactable = false;
    }

    void Start()
    {
        StartCoroutine(Clarear());
    }

    IEnumerator Clarear()
    {
        float t = 0f;
        while (t < 1f)
        {
            t = t + Time.deltaTime / duracao;
            grupo.alpha = 1f - t;
            yield return null;
        }
        grupo.alpha = 0f;
    }

    public void FadeOutECarregar(string nomeDaCena)
    {
        StartCoroutine(Escurecer(nomeDaCena));
    }

    IEnumerator Escurecer(string nomeDaCena)
    {
        float t = 0f;
        while (t < 1f)
        {
            t = t + Time.deltaTime / duracao;
            grupo.alpha = t;
            yield return null;
        }
        grupo.alpha = 1f;
        SceneManager.LoadScene(nomeDaCena);
    }
}
