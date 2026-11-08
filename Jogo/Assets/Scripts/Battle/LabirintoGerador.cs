using UnityEngine;

// Constrói o labirinto a partir de um desenho em texto.
// # = parede   . = chão livre   S = início   E = saída
public class LabirintoGerador : MonoBehaviour
{
    [TextArea(8, 20)]
    public string mapa =
        "###############\n" +
        "#S..#...#.....#\n" +
        "###.#.#.###.#.#\n" +
        "#...#.#.#...#.#\n" +
        "#.###.#.#.###.#\n" +
        "#.#...#.#.#...#\n" +
        "#.#.###.#.#.###\n" +
        "#...#...#.#.#.#\n" +
        "#####.###.#.#.#\n" +
        "#.........#..E#\n" +
        "###############";

    public float tamanhoCelula = 4f;   // largura de cada corredor
    public float alturaParede = 4f;

    public Material materialParede;
    public Material materialChao;

    public Transform jogador;
    public Transform tortuguita;
    public Transform zonaSaida;        // objeto que será colocado na saída (E)

    void Awake()
    {
        Construir();
    }

    // Centro de uma célula do mapa (coluna x, linha z)
    Vector3 CentroDaCelula(int coluna, int linha)
    {
        return transform.position + new Vector3(coluna * tamanhoCelula, 0f, -linha * tamanhoCelula);
    }

    void Construir()
    {
        string[] linhas = mapa.Split('\n');
        int totalLinhas = linhas.Length;
        int totalColunas = 0;

        for (int i = 0; i < totalLinhas; i++)
        {
            linhas[i] = linhas[i].Trim();
            if (linhas[i].Length > totalColunas)
            {
                totalColunas = linhas[i].Length;
            }
        }

        // --- Chão: um bloco grande embaixo de tudo ---
        GameObject chao = GameObject.CreatePrimitive(PrimitiveType.Cube);
        chao.name = "Chao";
        chao.transform.parent = transform;
        chao.transform.localScale = new Vector3(totalColunas * tamanhoCelula, 1f, totalLinhas * tamanhoCelula);
        chao.transform.position = transform.position + new Vector3(
            (totalColunas - 1) * tamanhoCelula / 2f,
            -0.5f,
            -(totalLinhas - 1) * tamanhoCelula / 2f);
        if (materialChao != null)
        {
            chao.GetComponent<Renderer>().sharedMaterial = materialChao;
        }

        // --- Paredes, início e saída ---
        for (int z = 0; z < totalLinhas; z++)
        {
            for (int x = 0; x < linhas[z].Length; x++)
            {
                char letra = linhas[z][x];
                Vector3 centro = CentroDaCelula(x, z);

                if (letra == '#')
                {
                    GameObject parede = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    parede.name = "Parede";
                    parede.transform.parent = transform;
                    parede.transform.position = centro + new Vector3(0f, alturaParede / 2f, 0f);
                    parede.transform.localScale = new Vector3(tamanhoCelula, alturaParede, tamanhoCelula);
                    if (materialParede != null)
                    {
                        parede.GetComponent<Renderer>().sharedMaterial = materialParede;
                    }
                }
                else if (letra == 'S')
                {
                    ColocarJogador(centro);
                }
                else if (letra == 'E')
                {
                    if (zonaSaida != null)
                    {
                        zonaSaida.position = centro + new Vector3(0f, 1f, 0f);
                    }
                }
            }
        }
    }

    void ColocarJogador(Vector3 centro)
    {
        if (jogador != null)
        {
            // CharacterController "briga" com mudança de posição, então desliga um instante
            CharacterController controle = jogador.GetComponent<CharacterController>();
            if (controle != null)
            {
                controle.enabled = false;
            }

            jogador.position = centro + new Vector3(0f, 0.1f, 0f);

            if (controle != null)
            {
                controle.enabled = true;
            }
        }

        if (tortuguita != null)
        {
            tortuguita.position = centro + new Vector3(1.2f, 0.1f, 0f);
        }
    }
}
