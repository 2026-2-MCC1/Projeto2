// O jogador herda tudo de PersonagemStats.
// Aqui ficam só as coisas exclusivas do jogador.
public class PlayerStats: PersonagemStats
{
    // Efeito do 7Belo: aumenta o ataque até o fim da batalha
    public void ComerSeteBelo()
    {
        ataque = ataque + 5;
    }
}