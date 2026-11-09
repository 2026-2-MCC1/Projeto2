using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class IntroducaoController : MonoBehaviour
{
    [Header("Painel do Nome")]
    public GameObject painelNome;
    public TMP_InputField inputNome;
    public Button botaoConfirmarNome;

    [Header("Painel da História / Diálogo")]
    public GameObject painelHistoria;
    public TextMeshProUGUI textoTitulo;
    public TextMeshProUGUI textoHistoria;
    public Button botaoAvancar;

    [Header("Configuração de Transição")]
    public string nomeProximaCena = "Fase_0"; 

    public static string NomeJogador { get; private set; } = "Jogador";

    private string[] linhasHistoria;
    private int indiceAtual = 0;

    void Start()
    {
        painelNome.SetActive(true);
        painelHistoria.SetActive(false);
        botaoConfirmarNome.onClick.AddListener(ConfirmarNome);
        botaoAvancar.onClick.AddListener(AvancarHistoria);
    }

    public void ConfirmarNome()
    {
        if (!string.IsNullOrWhiteSpace(inputNome.text))
        {
            NomeJogador = inputNome.text.Trim();
            PlayerPrefs.SetString("NomeJogador", NomeJogador);
        }

        painelNome.SetActive(false);
        IniciarNarrativa();
    }

    void IniciarNarrativa()
    {
        linhasHistoria = new string[]
        {
            $"{NomeJogador} é um aluno que está participando de um tour na fábrica da Arcor com a sua escola.",
            "O guia leva a turma para a parte interna e começa a apresentar o local.",
            "Ao passar pelas salas de máquinas, você avista uma porta de acesso restrito entreaberta...",
            "Você se aproxima para fechá-la, mas de repente algo te puxa para dentro!",
            "Tudo fica escuro..."
        };

        painelHistoria.SetActive(true);
        ExibirLinha();
    }

    void ExibirLinha()
    {
        if (indiceAtual < linhasHistoria.Length)
        {
            textoTitulo.text = "Introdução";
            textoHistoria.text = linhasHistoria[indiceAtual];
        }
        else
        {
            SceneManager.LoadScene("Fase_0");
        }
    }

    public void AvancarHistoria()
    {
        indiceAtual++;
        ExibirLinha();
    }
}