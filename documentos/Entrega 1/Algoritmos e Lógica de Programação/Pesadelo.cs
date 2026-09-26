Console.WriteLine("================================");
Console.WriteLine("Bem-vindo ao jogo!");
Console.WriteLine("================================");

Console.WriteLine("Digite o seu nome:");

string nome = Console.ReadLine() ?? "";

int energia = 100;
int pontos = 0;

Console.WriteLine("Olá, " + nome + "! Que bom que você acordou! Você está em uma sala desconhecida, e precisará responder charadas e problemas para encontrar o caminho de volta para casa.");

Console.WriteLine("Você está pronto para começar? (sim/não)");

string resposta1 = (Console.ReadLine() ?? "").ToLower();

while (resposta1 != "sim" && resposta1 != "não")
{
    Console.WriteLine("Resposta inválida. Por favor, digite 'sim' ou 'não'.");
    Console.WriteLine("Você está pronto para começar? (sim/não)");

    resposta1 = (Console.ReadLine() ?? "").ToLower();
}

if (resposta1 == "não")
{
    Console.WriteLine("Os desafios do jogo não podem ser enfrentados sem coragem. Volte quando estiver pronto!");

    Console.WriteLine("Pressione qualquer tecla para sair...");
    Console.ReadKey();
}
else if (resposta1 == "sim")
{
    Console.Clear();

    Console.WriteLine("Ótimo! Vamos começar!");

    Console.WriteLine("Clique em qualquer tecla para continuar...");
    Console.ReadKey();

    Console.Clear();

    while (true)
    {
        energia = 100;
        pontos = 0;

        Fase1(nome, ref energia, ref pontos);

        Console.Clear();

        Console.WriteLine("Deseja jogar novamente? (sim/não)");

        string resposta10 = (Console.ReadLine() ?? "").ToLower();

        while (resposta10 != "sim" && resposta10 != "não")
        {
            Console.WriteLine("Resposta inválida. Por favor, digite 'sim' ou 'não'.");
            Console.WriteLine("Deseja jogar novamente? (sim/não)");

            resposta10 = (Console.ReadLine() ?? "").ToLower();
        }

        if (resposta10 == "não")
        {
            Console.WriteLine("Obrigado por jogar! Até a próxima!");
            break;
        }

        Console.Clear();
        Console.WriteLine("Vamos jogar novamente!");
        Console.WriteLine("Pressione qualquer tecla para continuar...");
        Console.ReadKey();

        Console.Clear();
    }
}


// FASE 1

void Fase1(string nome, ref int energia, ref int pontos)
{
    Console.WriteLine("Para o primeiro desafio, você vai precisar colocar seus conhecimentos de lógica em prática. Boa sorte!");

    Console.WriteLine("O que é, o que é: Quanto mais se tira, maior fica?");

    string resposta2 = (Console.ReadLine() ?? "").ToLower();

    while (resposta2 != "buraco" && energia > 0)
    {
        Console.WriteLine("Resposta incorreta. Você perdeu 10 de energia!");

        energia -= 10;

        Console.WriteLine("Energia atual: " + energia);

        if (energia > 0)
        {
            Console.WriteLine("Tente novamente.");
            Console.WriteLine("O que é, o que é: Quanto mais se tira, maior fica?");

            resposta2 = (Console.ReadLine() ?? "").ToLower();
        }
    }

    if (energia <= 0)
    {
        Console.WriteLine("Sua energia acabou!");
        Console.WriteLine("GAME OVER!");

        Console.WriteLine("Pressione qualquer tecla para sair...");
        Console.ReadKey();

        return;
    }

    Console.WriteLine("Parabéns! Você acertou!");

    pontos += 10;

    Console.WriteLine("Você ganhou 10 pontos! Pontuação atual: " + pontos);

    Console.WriteLine("Pressione qualquer tecla para continuar...");
    Console.ReadKey();

    Console.Clear();


    Console.WriteLine("Você encontrou uma porta misteriosa. Para abri-la, você precisa resolver um enigma matemático.");

    Console.WriteLine("Qual é o próximo número da sequência: 1 — 1 — 2 — 3 — 5 — 8 — 13 — ?");

    string resposta3 = (Console.ReadLine() ?? "").ToLower();

    while (resposta3 != "21" && energia > 0)
    {
        Console.WriteLine("Resposta incorreta. Você perdeu 10 de energia!");

        energia -= 10;

        Console.WriteLine("Energia atual: " + energia);

        if (energia > 0)
        {
            Console.WriteLine("Tente novamente.");

            Console.WriteLine("Qual é o próximo número da sequência: 1 — 1 — 2 — 3 — 5 — 8 — 13 — ?");

            resposta3 = (Console.ReadLine() ?? "").ToLower();
        }
    }

    if (energia <= 0)
    {
        Console.WriteLine("Sua energia acabou!");
        Console.WriteLine("GAME OVER!");

        Console.WriteLine("Pressione qualquer tecla para sair...");
        Console.ReadKey();

        return;
    }

    Console.WriteLine("Parabéns! Você acertou!");

    pontos += 10;

    Console.WriteLine("Você ganhou 10 pontos! Pontuação atual: " + pontos);

    Console.WriteLine("Pressione qualquer tecla para continuar...");
    Console.ReadKey();

    Console.Clear();

    Fase2(nome, ref energia, ref pontos);
}


// FASE 2

void Fase2(string nome, ref int energia, ref int pontos)
{
    Console.WriteLine("Você encontrou uma chave escondida. Para usá-la, você precisa resolver um enigma lógico.");

    Console.WriteLine("O que é, o que é: Tem cabeça, tem dente, mas não é bicho nem gente?");

    string resposta4 = (Console.ReadLine() ?? "").ToLower();

    while (resposta4 != "alho" && energia > 0)
    {
        Console.WriteLine("Resposta incorreta. Você perdeu 10 de energia!");

        energia -= 10;

        Console.WriteLine("Energia atual: " + energia);

        if (energia > 0)
        {
            Console.WriteLine("Tente novamente.");

            Console.WriteLine("O que é, o que é: Tem cabeça, tem dente, mas não é bicho nem gente?");

            resposta4 = (Console.ReadLine() ?? "").ToLower();
        }
    }

    if (energia <= 0)
    {
        Console.WriteLine("Sua energia acabou!");
        Console.WriteLine("GAME OVER!");

        Console.WriteLine("Pressione qualquer tecla para sair...");
        Console.ReadKey();

        return;
    }

    Console.WriteLine("Parabéns! Você acertou!");

    pontos += 10;

    Console.WriteLine("Você ganhou 10 pontos! Pontuação atual: " + pontos);

    Console.WriteLine("Pressione qualquer tecla para continuar...");
    Console.ReadKey();

    Console.Clear();


    Console.WriteLine("Você coloca a chave na fechadura e gira. A porta se abre lentamente.");

    Console.WriteLine("Você encontrou a saída da sala desconhecida!");

    Console.WriteLine("Ao abrir a porta você se depara com uma sala com uma luz brilhante e fria.");

    Console.WriteLine("Percebe que está em uma espécie de cubo, ao olhar para o lado vê uma mesa e nela há um bilhete escrito:");

    Console.WriteLine("'Parabéns " + nome + ", você concluiu 1 de 3 desafios. Agora para sair do cubo precisará responder perguntas sobre o país em que mora.'");

    Console.WriteLine("Pressione qualquer tecla para continuar...");
    Console.ReadKey();

    Console.Clear();


    Console.WriteLine("Quais são as duas cores mais relevantes na bandeira do Brasil?");

    string resposta5 = (Console.ReadLine() ?? "").ToLower();

    while (resposta5 != "amarelo e verde" && resposta5 != "verde e amarelo" && energia > 0)
    {
        Console.WriteLine("Resposta incorreta. Você perdeu 10 de energia!");

        energia -= 10;

        Console.WriteLine("Energia atual: " + energia);

        if (energia > 0)
        {
            Console.WriteLine("Tente novamente.");

            Console.WriteLine("Quais são as duas cores mais relevantes na bandeira do Brasil?");

            resposta5 = (Console.ReadLine() ?? "").ToLower();
        }
    }

    if (energia <= 0)
    {
        Console.WriteLine("Sua energia acabou!");
        Console.WriteLine("GAME OVER!");

        Console.WriteLine("Pressione qualquer tecla para sair...");
        Console.ReadKey();

        return;
    }

    Console.WriteLine("Parabéns! Você acertou!");

    pontos += 10;

    Console.WriteLine("Você ganhou 10 pontos! Pontuação atual: " + pontos);

    Console.WriteLine("Pressione qualquer tecla para continuar...");
    Console.ReadKey();

    Console.Clear();


    Console.WriteLine("Qual é a primeira capital do Brasil?");

    string resposta6 = (Console.ReadLine() ?? "").ToLower();

    while (resposta6 != "salvador" && energia > 0)
    {
        Console.WriteLine("Resposta incorreta. Você perdeu 10 de energia!");

        energia -= 10;

        Console.WriteLine("Energia atual: " + energia);

        if (energia > 0)
        {
            Console.WriteLine("Tente novamente.");

            Console.WriteLine("Qual é a primeira capital do Brasil?");

            resposta6 = (Console.ReadLine() ?? "").ToLower();
        }
    }

    if (energia <= 0)
    {
        Console.WriteLine("Sua energia acabou!");
        Console.WriteLine("GAME OVER!");

        Console.WriteLine("Pressione qualquer tecla para sair...");
        Console.ReadKey();

        return;
    }

    Console.WriteLine("Parabéns! Você acertou!");

    pontos += 10;

    Console.WriteLine("Você ganhou 10 pontos! Pontuação atual: " + pontos);

    Console.WriteLine("Pressione qualquer tecla para continuar...");
    Console.ReadKey();

    Console.Clear();

    Fase3(nome, ref energia, ref pontos);
}


// FASE 3

void Fase3(string nome, ref int energia, ref int pontos)
{
    Console.WriteLine("Assim que respondeu a última pergunta, o cubo escurece e você se perde em seus sentidos...");

    Console.WriteLine("Algo está acontecendo, você sente tudo tremer e então, ao abrir os olhos novamente, você se vê em um barco.");

    Console.WriteLine("No mastro tem um novo bilhete escrito:");

    Console.WriteLine("'Para chegar em casa " + nome + ", você irá direcionar o barco até a ilha. Para isso, você precisará responder a 3 perguntas sobre o mundo.'");

    Console.WriteLine("Pressione qualquer tecla para continuar...");
    Console.ReadKey();

    Console.Clear();


    Console.WriteLine("Qual é o maior país do mundo em extensão territorial?");

    string resposta7 = (Console.ReadLine() ?? "").ToLower();

    while (resposta7 != "russia" && energia > 0)
    {
        Console.WriteLine("Resposta incorreta. Você perdeu 10 de energia!");

        energia -= 10;

        Console.WriteLine("Energia atual: " + energia);

        if (energia > 0)
        {
            Console.WriteLine("Tente novamente.");

            Console.WriteLine("Qual é o maior país do mundo em extensão territorial?");

            resposta7 = (Console.ReadLine() ?? "").ToLower();
        }
    }

    if (energia <= 0)
    {
        Console.WriteLine("Sua energia acabou!");
        Console.WriteLine("GAME OVER!");

        Console.WriteLine("Pressione qualquer tecla para sair...");
        Console.ReadKey();

        return;
    }

    Console.WriteLine("Parabéns! Você acertou!");

    pontos += 10;

    Console.WriteLine("Você ganhou 10 pontos! Pontuação atual: " + pontos);

    Console.WriteLine("Pressione qualquer tecla para continuar...");
    Console.ReadKey();

    Console.Clear();


    Console.WriteLine("Qual é o país mais populoso do mundo?");

    string resposta8 = (Console.ReadLine() ?? "").ToLower();

    while (resposta8 != "china" && energia > 0)
    {
        Console.WriteLine("Resposta incorreta. Você perdeu 10 de energia!");

        energia -= 10;

        Console.WriteLine("Energia atual: " + energia);

        if (energia > 0)
        {
            Console.WriteLine("Tente novamente.");

            Console.WriteLine("Qual é o país mais populoso do mundo?");

            resposta8 = (Console.ReadLine() ?? "").ToLower();
        }
    }

    if (energia <= 0)
    {
        Console.WriteLine("Sua energia acabou!");
        Console.WriteLine("GAME OVER!");

        Console.WriteLine("Pressione qualquer tecla para sair...");
        Console.ReadKey();

        return;
    }

    Console.WriteLine("Parabéns! Você acertou!");

    pontos += 10;

    Console.WriteLine("Você ganhou 10 pontos! Pontuação atual: " + pontos);

    Console.WriteLine("Pressione qualquer tecla para continuar...");
    Console.ReadKey();

    Console.Clear();


    Console.WriteLine("Qual é o país conhecido como 'Terra do Sol Nascente'?");

    string resposta9 = (Console.ReadLine() ?? "").ToLower();

    while (resposta9 != "japão" && energia > 0)
    {
        Console.WriteLine("Resposta incorreta. Você perdeu 10 de energia!");

        energia -= 10;

        Console.WriteLine("Energia atual: " + energia);

        if (energia > 0)
        {
            Console.WriteLine("Tente novamente.");

            Console.WriteLine("Qual é o país conhecido como 'Terra do Sol Nascente'?");

            resposta9 = (Console.ReadLine() ?? "").ToLower();
        }
    }

    if (energia <= 0)
    {
        Console.WriteLine("Sua energia acabou!");
        Console.WriteLine("GAME OVER!");

        Console.WriteLine("Pressione qualquer tecla para sair...");
        Console.ReadKey();

        return;
    }

    Console.WriteLine("Parabéns! Você acertou!");

    pontos += 10;

    Console.WriteLine("Você ganhou 10 pontos! Pontuação atual: " + pontos);

    Console.WriteLine("Pressione qualquer tecla para continuar...");
    Console.ReadKey();

    Console.Clear();


    Console.WriteLine("Você consegue direcionar o barco à ilha e encontra uma escada que leva a um portal brilhante.");

    Console.WriteLine("Ao atravessar o portal, você se vê na sala de aula, seguro e percebe que foi apenas um sonho, durante a aula de cálculo.");

    Console.WriteLine("Parabéns " + nome + ", você completou o jogo!");

    Console.WriteLine("Pontuação final: " + pontos);
    Console.WriteLine("Energia restante: " + energia);

    while (true)
{
    Console.WriteLine("Deseja jogar novamente? (sim/não)");

    string resposta10 = (Console.ReadLine() ?? "").ToLower();

    if (resposta10 == "sim")
    {
        Console.Clear();

        energia = 100;
        pontos = 0;

        Fase1(nome, ref energia, ref pontos);

        break;
    }
    else if (resposta10 == "não")
    {
        Console.WriteLine("Obrigado por jogar! Até a próxima!");
        break;
    }
    else
    {
        Console.WriteLine("Resposta inválida. Por favor, digite 'sim' ou 'não'.");
    }
}
    }
