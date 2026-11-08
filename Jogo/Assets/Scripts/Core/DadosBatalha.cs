using UnityEngine;

// Guarda os dados que precisam "viajar" entre as cenas
// (Fase 0 -> 03_Batalha -> Fase 0).
// Como é "static", não precisa ficar em nenhum objeto da cena.
public static class DadosBatalha
{
    // ---------- Jogador ----------
    public static string nomeJogador = "Jogador";   // depois: preencher na tela de nome
    public static int vidaMaximaJogador = 50;
    public static int ataqueJogador = 10;
    public static int defesaJogador = 4;

    // ---------- Itens ----------
    public static int qtdSeteBelo = 0;            // 7Belos no inventário
    public static int seteBeloAntesDaBatalha = 0; // para restaurar se perder
    public static int sacoSeteBelo = 0;           // recompensa da batalha
    public static bool coletouSeteBeloFase0 = false;

    // ---------- Controle da batalha ----------
    public static string cenaDeRetorno = "02Fase_0";      // cena para voltar depois
    public static Vector3 posicaoRetorno;         // onde o jogador estava
    public static bool temPosicaoRetorno = false;
    public static bool inimigoDerrotado = false;
    public static bool chegadaLabirintoMostrada = false;
    public static bool saidaLabirintoMostrada = false;

    // ---------- Progresso da Fase 0 ----------
    public static bool chegadaMostrada = false;           // fala da chegada
    public static bool dialogoPosBatalhaMostrado = false; // fala depois da vitória
    public static bool portaAberta = false;

    // Chame isso quando começar um jogo novo (botão Jogar do menu)
    public static void Resetar()
    {
        nomeJogador = "Jogador";
        vidaMaximaJogador = 50;
        ataqueJogador = 10;
        defesaJogador = 4;

        qtdSeteBelo = 0;
        seteBeloAntesDaBatalha = 0;
        sacoSeteBelo = 0;
        coletouSeteBeloFase0 = false;

        cenaDeRetorno = "";
        posicaoRetorno = Vector3.zero;
        temPosicaoRetorno = false;
        inimigoDerrotado = false;

        chegadaMostrada = false;
        dialogoPosBatalhaMostrado = false;
        portaAberta = false;

        chegadaLabirintoMostrada = false;
        saidaLabirintoMostrada = false;
    }
}
