using UnityEngine;

// Classe base de atributos. Jogador e inimigos HERDAM dela (heranca de POO).
public class PersonagemStats : MonoBehaviour
{
    public string nome = "Personagem";
    public int vidaMaxima = 50;
    public int vidaAtual;
    public int ataque = 10;
    public int defesa = 4;

    [HideInInspector] public bool defendendo = false;

    // Deixa o personagem pronto para começar a batalha
    public void Iniciar()
    {
        vidaAtual = vidaMaxima;
        defendendo = false;
    }

    // Usado pelo jogador (valores vêm do DadosBatalha)
    public void Configurar(string novoNome, int vida, int atk, int def)
    {
        nome = novoNome;
        vidaMaxima = vida;
        ataque = atk;
        defesa = def;
        Iniciar();
    }

    // Calcula o dano que ESTE personagem recebe
    // dano = ataque do atacante - defesa (mínimo 1). Defender dobra a defesa.
    public int CalcularDano(int ataqueDoAtacante)
    {
        int defesaTotal = defesa;
        if (defendendo)
        {
            defesaTotal = defesa * 2;
        }

        int dano = ataqueDoAtacante - defesaTotal;
        if (dano < 1)
        {
            dano = 1;
        }
        return dano;
    }

    public void ReceberDano(int dano)
    {
        vidaAtual = vidaAtual - dano;
        if (vidaAtual < 0)
        {
            vidaAtual = 0;
        }
    }

    public bool EstaMorto()
    {
        return vidaAtual <= 0;
    }
}
