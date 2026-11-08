using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

// Estados possíveis da batalha
public enum EstadoBatalha
{
    TurnoJogador,
    TurnoInimigo,
    Vitoria,
    Derrota
}

// Controla o fluxo completo da batalha tutorial.
public class BattleManager : MonoBehaviour
{
    [Header("Personagens")]
    public PlayerStats jogador;
    public EnemyStats inimigo;
    public MonoBehaviour scriptDeAndar;   // arraste aqui o script de andar do Jogador

    [Header("Efeitos (opcional)")]
    public EfeitosBatalha efeitos;

    [Header("UI - Vida")]
    public Slider barraVidaJogador;
    public Slider barraVidaInimigo;
    public TMP_Text textoVidaJogador;
    public TMP_Text textoVidaInimigo;
    public TMP_Text textoNomeInimigo;

    [Header("UI - Mensagens")]
    public TMP_Text textoDica;

    [Header("UI - Botões de ação")]
    public Button botaoAtacar;
    public Button botaoDefender;
    public Button botaoInventario;
    public Button botaoFugir;

    [Header("UI - Inventário")]
    public GameObject painelInventario;
    public Button botaoSeteBelo;
    public TMP_Text textoSeteBelo;
    public Button botaoVoltarInventario;

    [Header("UI - Vitória")]
    public GameObject painelVitoria;
    public Button botaoContinuar;

    [Header("UI - Game Over")]
    public GameObject painelGameOver;
    public Button botaoTentarNovamente;
    public Button botaoVoltarMenu;

    private EstadoBatalha estado;
    private int passoTutorial = 1;   // 1 = atacar, 2 = defender, 3 = item, 4 = livre
    private int turnoInimigo = 0;    // conta quantas vezes o inimigo jogou
    private bool dicaFinalMostrada = false;

    void Start()
    {
        if (scriptDeAndar != null) scriptDeAndar.enabled = false;
        // Libera o mouse (a Fase 0 pode ter travado)
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        // --- Proteções para poder testar a cena 03_Batalha sozinha ---
        if (DadosBatalha.qtdSeteBelo < 1)
        {
            DadosBatalha.qtdSeteBelo = 1;
        }
        if (string.IsNullOrEmpty(DadosBatalha.cenaDeRetorno))
        {
            DadosBatalha.cenaDeRetorno = "00_MenuInicial";
        }

        // --- Configura os personagens ---
        jogador.Configurar("Você", DadosBatalha.vidaMaximaJogador, DadosBatalha.ataqueJogador, DadosBatalha.defesaJogador);
        inimigo.Iniciar();
        textoNomeInimigo.text = inimigo.nome;

        // --- Liga os botões aos métodos ---
        botaoAtacar.onClick.AddListener(Atacar);
        botaoDefender.onClick.AddListener(Defender);
        botaoInventario.onClick.AddListener(AbrirInventario);
        botaoFugir.onClick.AddListener(Fugir);
        botaoSeteBelo.onClick.AddListener(UsarSeteBelo);
        botaoVoltarInventario.onClick.AddListener(FecharInventario);
        botaoContinuar.onClick.AddListener(Continuar);
        botaoTentarNovamente.onClick.AddListener(TentarNovamente);
        botaoVoltarMenu.onClick.AddListener(VoltarMenu);

        // --- Esconde os painéis ---
        painelInventario.SetActive(false);
        painelVitoria.SetActive(false);
        painelGameOver.SetActive(false);

        AtualizarBarras();
        ComecarTurnoJogador();
    }

    // =========================================================
    //  TURNO DO JOGADOR
    // =========================================================

    void ComecarTurnoJogador()
    {
        estado = EstadoBatalha.TurnoJogador;
        jogador.defendendo = false;   // a defesa dura só 1 turno do inimigo
        AtualizarBotoes();

        if (passoTutorial == 1)
        {
            textoDica.text = "Tortuguita: É a sua vez! Escolha Atacar.";
        }
        else if (passoTutorial == 2)
        {
            textoDica.text = inimigo.nome + " está se encolhendo toda...\nTortuguita: Ela vai atacar forte! Use Defender!";
        }
        else if (passoTutorial == 3)
        {
            textoDica.text = "Tortuguita: Lembra da 7Belo? Abra o Inventário e coma ela!";
        }
        else if (dicaFinalMostrada == false)
        {
            textoDica.text = "Tortuguita: Sentiu a força? Agora acaba com ela!";
            dicaFinalMostrada = true;
        }
        else
        {
            textoDica.text = "Sua vez! Escolha uma ação.";
        }
    }

    // No tutorial, só o botão ensinado fica ativo (passos 1, 2 e 3)
    void AtualizarBotoes()
    {
        bool turnoDoJogador = (estado == EstadoBatalha.TurnoJogador);

        botaoAtacar.interactable = turnoDoJogador && (passoTutorial == 1 || passoTutorial >= 4);
        botaoDefender.interactable = turnoDoJogador && (passoTutorial == 2 || passoTutorial >= 4);
        botaoInventario.interactable = turnoDoJogador && (passoTutorial == 3 || passoTutorial >= 4);
        botaoFugir.interactable = turnoDoJogador;
    }

    // Só avança o tutorial se o jogador fez a ação do passo atual
    void AvancarTutorial(int passoEsperado)
    {
        if (passoTutorial == passoEsperado)
        {
            passoTutorial++;
        }
    }

    // ---------- Botão ATACAR ----------
    void Atacar()
    {
        if (estado != EstadoBatalha.TurnoJogador) return;

        // Jogador pode dar crítico, mas não erra (tutorial)
        string msg = FazerAtaque(jogador, inimigo, jogador.ataque, true, false);
        textoDica.text = msg;
        if (efeitos != null)
        {
            efeitos.AtaqueDoJogador();
        }

        AvancarTutorial(1);
        StartCoroutine(AposAcaoDoJogador());
    }

    // ---------- Botão DEFENDER ----------
    void Defender()
    {
        if (estado != EstadoBatalha.TurnoJogador) return;

        jogador.defendendo = true;
        textoDica.text = "Você se defendeu! Sua defesa dobrou.";

        AvancarTutorial(2);
        StartCoroutine(AposAcaoDoJogador());
    }

    // ---------- Botão INVENTÁRIO ----------
    void AbrirInventario()
    {
        if (estado != EstadoBatalha.TurnoJogador) return;

        painelInventario.SetActive(true);
        textoSeteBelo.text = "7Belo x" + DadosBatalha.qtdSeteBelo;
        botaoSeteBelo.interactable = DadosBatalha.qtdSeteBelo > 0;
    }

    void FecharInventario()
    {
        painelInventario.SetActive(false);
    }

    void UsarSeteBelo()
    {
        if (estado != EstadoBatalha.TurnoJogador) return;
        if (DadosBatalha.qtdSeteBelo <= 0) return;

        DadosBatalha.qtdSeteBelo = DadosBatalha.qtdSeteBelo - 1;
        jogador.ComerSeteBelo();

        painelInventario.SetActive(false);
        textoDica.text = "Você comeu a 7Belo! Seu ataque aumentou em 5.";

        AvancarTutorial(3);
        StartCoroutine(AposAcaoDoJogador());
    }

    // ---------- Botão FUGIR ----------
    void Fugir()
    {
        if (estado != EstadoBatalha.TurnoJogador) return;
        textoDica.text = "Tortuguita: Não dá pra fugir dessa vez!";
    }

    // =========================================================
    //  DEPOIS DA AÇÃO DO JOGADOR / TURNO DO INIMIGO
    //  (Coroutines permitem "esperar" uns segundos entre as ações)
    // =========================================================

    IEnumerator AposAcaoDoJogador()
    {
        // Bloqueia os botões enquanto a animação/mensagem acontece
        estado = EstadoBatalha.TurnoInimigo;
        AtualizarBotoes();
        AtualizarBarras();

        yield return new WaitForSeconds(1.5f);

        if (inimigo.EstaMorto())
        {
            Vencer();
            yield break;   // para a coroutine aqui
        }

        yield return StartCoroutine(TurnoDoInimigo());
    }

    IEnumerator TurnoDoInimigo()
    {
        estado = EstadoBatalha.TurnoInimigo;
        AtualizarBotoes();
        turnoInimigo++;

        int ataqueUsado = inimigo.ataque;
        string prefixo = "";

        // No 2º turno o inimigo usa o ataque pesado (ensina a Defender)
        if (turnoInimigo == 2)
        {
            ataqueUsado = inimigo.ataquePesado;
            prefixo = "Ataque pesado! ";
        }

        // Inimigo não erra nem dá crítico no tutorial
        string msg = FazerAtaque(inimigo, jogador, ataqueUsado, false, false);
        textoDica.text = prefixo + msg;
        if (efeitos != null)
        {
            efeitos.AtaqueDoInimigo();
        }
        AtualizarBarras();

        yield return new WaitForSeconds(1.5f);

        if (jogador.EstaMorto())
        {
            Perder();
            yield break;
        }

        ComecarTurnoJogador();
    }

    // =========================================================
    //  CÁLCULO DO ATAQUE (regras do GDD)
    //  sorteio 0-14  -> crítico
    //  sorteio 15-90 -> normal
    //  sorteio 91-99 -> erro
    // =========================================================

    string FazerAtaque(PersonagemStats atacante, PersonagemStats alvo, int ataqueUsado, bool podeCritar, bool podeErrar)
    {
        int sorteio = Random.Range(0, 100);   // 0 até 99

        if (podeErrar && sorteio >= 91)
        {
            return atacante.nome + " errou o ataque!";
        }

        int dano = alvo.CalcularDano(ataqueUsado);
        string extra = "";

        if (podeCritar && sorteio <= 14)
        {
            dano = Mathf.RoundToInt(dano * 1.5f);
            extra = " Acerto crítico!";
        }

        alvo.ReceberDano(dano);
        return atacante.nome + " causou " + dano + " de dano." + extra;
    }

    // =========================================================
    //  UI
    // =========================================================

    void AtualizarBarras()
    {
        barraVidaJogador.maxValue = jogador.vidaMaxima;
        barraVidaJogador.value = jogador.vidaAtual;
        textoVidaJogador.text = jogador.vidaAtual + " / " + jogador.vidaMaxima;

        barraVidaInimigo.maxValue = inimigo.vidaMaxima;
        barraVidaInimigo.value = inimigo.vidaAtual;
        textoVidaInimigo.text = inimigo.vidaAtual + " / " + inimigo.vidaMaxima;
    }

    // =========================================================
    //  FIM DA BATALHA
    // =========================================================

    void Vencer()
    {
        estado = EstadoBatalha.Vitoria;
        AtualizarBotoes();
        textoDica.text = "Vitória!";
        painelVitoria.SetActive(true);
    }

    void Perder()
    {
        estado = EstadoBatalha.Derrota;
        AtualizarBotoes();
        textoDica.text = "Você foi derrotado...";
        painelGameOver.SetActive(true);
    }

    void Continuar()
    {
        DadosBatalha.inimigoDerrotado = true;
        DadosBatalha.sacoSeteBelo = DadosBatalha.sacoSeteBelo + 1;   // recompensa
        SceneManager.LoadScene(DadosBatalha.cenaDeRetorno);
    }

    void TentarNovamente()
    {
        // Devolve o 7Belo que foi gasto e recarrega a batalha
        DadosBatalha.qtdSeteBelo = DadosBatalha.seteBeloAntesDaBatalha;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void VoltarMenu()
    {
        SceneManager.LoadScene("00_MenuInicial");
    }
}
