using System;
using static System.Console;
using System.Threading;
using System.Globalization;
using System.Text;
using System.Runtime.ConstrainedExecution;

namespace Forca
{
    class Program
    {
        static void Main(string[] args)
        {

            int numOpc = 0;
            // Faz enquanto a opção digitada for diferente de 1 ou 2
            while (numOpc != 1 || numOpc != 2)
            {
                janela(0, 0, 79, 24, 3, 0, 'd');
                janela(1, 1, 78, 3, 3, 0, 'd');
                janela(1, 21, 78, 23, 3, 0, 'd');
                SetCursorPosition(30, 2);
                Write("* * * Forca * * *");
                SetCursorPosition(30, 22); Write("Digite uma opção: ");

                SetCursorPosition(32, 8); Write("[1] Contra a máquina");
                SetCursorPosition(32, 9); Write("[2] Contra um jogador");
                SetCursorPosition(32, 10); Write("[3] Sair");
                SetCursorPosition(48, 22); string opc = ReadLine();

                if (int.TryParse(opc, out numOpc))
                {
                    switch (numOpc)
                    {
                        case 1:
                            Bot();
                            break;

                        case 2:
                            Versus();
                            break;

                        case 3:
                            SetCursorPosition(30, 22); Write("ENCERRANDO O PROGRAMA...");
                            Environment.Exit(2000);
                            break;

                        default:
                            WriteLine("Digite uma opção valida");
                            break;
                    }
                }

                // Se usuário digitou alguma letra ele receberá um aviso
                else if (char.TryParse(opc, out char charOpc))
                {
                    janela(1, 21, 78, 23, 3, 0, 'd');
                    SetCursorPosition(30, 22); Write("ESCOLHA UMA OPÇÃO VALIDA");
                    Thread.Sleep(1000);
                }

                // Se o usuario digitou uma string ele também receberá um aviso
                else
                {
                    janela(1, 21, 78, 23, 3, 0, 'd');
                    SetCursorPosition(30, 22); Write("ESCOLHA UMA OPÇÃO VALIDA");
                    Thread.Sleep(1000);
                }
            }
        }



        static void janela(int c1, int l1, int c2, int l2, int corl, int corf, char borda)
        {
            char lv = ' ', csd = ' ', cid = ' ', cie = ' ', cse = ' ', lh = ' ';
            switch (borda)
            {
                case 'd':
                    lv = '║';
                    csd = '╗';
                    cid = '╝';
                    cie = '╚';
                    cse = '╔';
                    lh = '═';
                    break;
                case 's':
                    lv = '│';
                    csd = '┐';
                    cid = '┘';
                    cie = '└';
                    cse = '┌';
                    lh = '─';
                    break;
                default:
                    break;
            }

            ForegroundColor = (ConsoleColor)corl;
            BackgroundColor = (ConsoleColor)corf;

            for (int x = l1; x <= l2; x++)
            {
                SetCursorPosition(c1, x); Write(lv);
                SetCursorPosition(c2, x); Write(lv);
            }

            SetCursorPosition(c1, l1); Write(new string(lh, c2 - c1 + 1));
            SetCursorPosition(c1, l2); Write(new string(lh, c2 - c1 + 1));

            SetCursorPosition(c1, l1); Write(cse);
            SetCursorPosition(c2, l1); Write(csd);
            SetCursorPosition(c1, l2); Write(cie);
            SetCursorPosition(c2, l2); Write(cid);

            for (int x = l1 + 1; x < l2; x++)
            {
                SetCursorPosition(c1 + 1, x);
                Write(new string(' ', c2 - c1 - 1));
            }
        }


        static void Bot()
        {
            string vResp = "nao";
            // Palavras permitidas para o usuário tentar acertar
            string[] palavras = new string[] {
                "Mesa", "Cadeira", "Computador", "Telefone", "Livro", "Janela", "Porta", "Lampada", "Carro", "Relogio",
                "Televisao", "Copo", "Teclado", "Mouse", "Ventilador", "Espelho", "Armario", "Cama", "Quadro", "Tapete",
                "Pizza", "Hamburguer", "Sushi", "Macarrao", "Salada", "Feijoada", "Taco", "Bolo", "Sorvete", "Churrasco",
                "Lasagna", "Pao", "Pastel", "Coxinha", "Empada", "Donuts", "Panqueca", "Batata frita", "Pipoca", "Torta",
                "Brasil", "Canada", "Japao", "Alemanha", "Franca", "Italia", "Argentina", "Australia", "Egito", "India",
                "China", "Espanha", "Mexico", "Russia", "Coreia do Sul", "Grecia", "Portugal", "Suecia", "Africa do Sul", "Chile"
            };

            // Dicas na ordem das respectivas palavras
            string[] dicas = new string[] {
                "Móvel usado para colocar objetos", "Móvel para sentar", "Dispositivo para processar informações", "Aparelho usado para fazer chamadas", "Objeto com páginas para ler",
                "Abertura na parede para entrada de luz", "Fechamento de uma passagem", "Fonte de luz artificial", "Veículo para transporte", "Dispositivo para medir o tempo",
                "Aparelho de exibição de imagens", "Recipiente para beber", "Dispositivo para digitar", "Dispositivo de entrada de computador", "Aparelho que gera vento",
                "Objeto que reflete imagens", "Móvel para guardar roupas", "Móvel para dormir", "Objeto decorativo para a parede", "Cobertura de chão",
                "Prato italiano feito com massa e molho", "Lanche com pão, carne e queijo", "Prato japonês feito com arroz e peixe", "Prato de massa com molho", "Prato saudável com vegetais",
                "Prato típico brasileiro com feijão", "Prato mexicano feito com tortilha", "Sobremesa assada, normalmente doce", "Doce gelado de diversos sabores", "Carne assada na grelha",
                "Prato italiano com camadas de massa", "Alimento feito com farinha e fermento", "Comida frita típica de feiras", "Salgado frito recheado", "Massa salgada recheada",
                "Doce frito em formato de anel", "Massa frita com recheio doce ou salgado", "Tiras de batata fritas", "Lanche crocante com milho", "Doce assado recheado ou coberto",
                "País da América do Sul conhecido pelo futebol", "País da América do Norte conhecido por seu inverno rigoroso", "País asiático famoso por sua cultura tecnológica",
                "País europeu famoso por sua história", "País europeu conhecido pela Torre Eiffel", "País europeu famoso por sua culinária e arte", "País sul-americano famoso pelo tango",
                "País da Oceania conhecido por suas paisagens", "País africano famoso pelas pirâmides", "País asiático com uma das maiores populações do mundo",
                "País asiático com uma das maiores economias do mundo", "País europeu conhecido por suas touradas", "País da América do Norte famoso por suas praias",
                "País europeu conhecido por sua vasta extensão", "País asiático famoso por sua tecnologia e K-pop", "País europeu conhecido por seus filósofos antigos",
                "País europeu famoso por seu vinho e litoral", "País europeu conhecido pelo design e inovação", "País africano com uma diversidade cultural rica", "País sul-americano famoso pelos Andes e pelo vinho"
            };

            // Declaração das variaveis
            Random aleatorio = new Random();
            int index = aleatorio.Next(palavras.Length);
            string palavraSorteada = palavras[index].ToUpper();
            string dicaSorteada = dicas[index];
            char[] letrasSorteadas = palavraSorteada.ToCharArray();
            int vidas = 6;
            bool vitoria = false;
            char[] progresso = new char[letrasSorteadas.Length];
            char letraDigitada;
            List<char> letras = new List<char>();
            string dica = "DICA";

            // Converte a palavra sorteada em uma cadeia de char's
            for (int i = 0; i < palavraSorteada.Length; i++)
            {
                progresso[i] = '_';
            }

            janela(0, 0, 79, 24, 3, 0, 'd');
            janela(1, 1, 78, 3, 3, 0, 'd');
            janela(1, 21, 78, 23, 3, 0, 'd');

            // Faz enquanto o usuário tem vidas e não ganhou
            while (vidas > 0 && !vitoria)
            {
                // Mostra a quantidade de vidas
                SetCursorPosition(55, 5); Write("Vidas restantes: " + vidas);

                // Desenha a forca
                SetCursorPosition(25, 11); Write("┌"); SetCursorPosition(26, 11); Write("─"); SetCursorPosition(27, 11); Write("─");
                SetCursorPosition(25, 12); Write("│");
                SetCursorPosition(25, 13); Write("│");
                SetCursorPosition(25, 14); Write("│");

                // Escreve os "_" de acordo com o tamanho da palavra sorteada 
                SetCursorPosition(30, 14); Write(progresso);

                // Mostra as letras digitadas pelo usuário
                SetCursorPosition(2, 5);
                WriteLine("Letras digitadas: " + string.Join(", ", letras));

                // Input
                janela(1, 21, 78, 23, 3, 0, 'd');
                SetCursorPosition(2, 20); WriteLine("Digite 'dica' para dica: ");
                SetCursorPosition(30, 22); WriteLine("Digite uma letra: ");
                SetCursorPosition(47, 22); string input = ReadLine().ToUpper();

                bool acertou = false;

                // Verifica se o usuário digitou uma variavel char
                if (char.TryParse(input, out letraDigitada))
                {
                    // Verifica se essa variavel char era um numero de 1 digito
                    if (letraDigitada == '0' || letraDigitada == '1' || letraDigitada == '2' || letraDigitada == '3' || letraDigitada == '4' || letraDigitada == '5' || letraDigitada == '6' || letraDigitada == '7' || letraDigitada == '8' || letraDigitada == '9')
                    {
                        janela(1, 21, 78, 23, 3, 0, 'd');
                        SetCursorPosition(20, 22); Write("NÚMEROS NÃO SÃO PERMITIDOS NESTE MODO!!");
                        Thread.Sleep(2000);
                    }

                    // Verifica se a letra digitada não esta na lista de letras
                    else if (!letras.Contains(letraDigitada))
                    {
                        letras.Add(letraDigitada);
                    }

                    // Avisa se a letra já foi digitada
                    else
                    {
                        SetCursorPosition(30, 22);
                        Write("Esta letra já foi digitada");
                        Thread.Sleep(1200);
                    }

                    // Verifica se a letra digitada esta dentre as letras sorteadas
                    for (int i = 0; i < palavraSorteada.Length; i++)
                    {
                        if (letrasSorteadas[i] == letraDigitada)
                        {
                            // Substitui p "_" pela letra digitada
                            progresso[i] = letraDigitada;
                            acertou = true;
                        }
                    }

                    if (acertou == false)
                    {
                        vidas--;
                    }

                }

                // Caso a variavel input seja um número ele dará um erro
                else if (int.TryParse(input, out int num) || double.TryParse(input, out double num2))
                {
                    janela(1, 21, 78, 23, 3, 0, 'd');
                    SetCursorPosition(20, 22); Write("NÚMEROS NÃO SÃO PERMITIDOS NESTE MODO!!");
                    Thread.Sleep(2000);
                }
                // Se o usuário digitou "dica"
                else if (input.Equals(dica))
                {
                    SetCursorPosition(27, 20);
                    Write(dicaSorteada);
                }
                // Caso o usuário tenha digitado alguma string
                else
                {
                    janela(1, 21, 78, 23, 3, 0, 'd');
                    SetCursorPosition(20, 22); Write("SOMENTE LETRAS SÃO PERMITIDAS NESTE MODO");
                    Thread.Sleep(2000);
                }

                // Verifica se ha algum "_" no progresso do usuário
                if (!progresso.Contains('_') && vidas >= 1)
                {
                    vitoria = true;
                    SetCursorPosition(30, 14);
                    Write(palavraSorteada);
                    string venceu = "VOCÊ VENCEU!!!";
                    for (int i = 0; i < venceu.Length; i++) // Letreiro
                    {
                        string lettersWin = venceu.Substring(i, 1);
                        for (int e = 50; e > i; e--)
                        {
                            SetCursorPosition(e + 25, 17);
                            Write(lettersWin + " ");
                            Thread.Sleep(1);
                        }

                    }

                    // Muda a cor do texto
                    for (int i = 1; i < 12; i++)
                    {
                        janela(23, 16, 44, 18, 3, 15, 'd');
                        ForegroundColor = (ConsoleColor)i;
                        SetCursorPosition(25, 17);
                        Write(venceu.ToUpper() + "!!!");
                        Thread.Sleep(400);

                    }

                    // Altera as cores para o que já estava antes
                    BackgroundColor = ConsoleColor.Black;
                    ForegroundColor = ConsoleColor.Blue;

                    SetCursorPosition(30, 22);
                    Write("Deseja continuar jogando? (S/N)");
                    vResp = ReadLine().ToLower();

                    if (vResp == "sim" || vResp == "s")
                    {
                        Clear();
                        Bot();
                    }
                    else if (vResp == "nao" || vResp == "n")
                    {
                        Clear();
                        break;
                    }


                }

                // Analisa se as vidas são iguais a 0
                if (vidas == 0)
                {
                    SetCursorPosition(30, 17);
                    WriteLine("VOCÊ PERDEU!!!");
                    SetCursorPosition(30, 22);
                    Write("Deseja continuar jogando? ");
                    vResp = ReadLine().ToLower();

                    if (vResp == "sim" || vResp == "s")
                    {
                        Clear();
                        Bot();
                    }
                    else
                    {
                        Clear();
                        WriteLine("Encerrando o programa...");
                        Environment.Exit(2000);
                    }
                }

                // Desenha o boneco
                switch (vidas)
                {
                    case 5:
                        SetCursorPosition(27, 12);
                        Write("0");
                        break;
                    case 4:
                        SetCursorPosition(27, 13);
                        Write("|");
                        break;
                    case 3:
                        SetCursorPosition(26, 14);
                        Write("/");
                        break;
                    case 2:
                        SetCursorPosition(28, 14);
                        Write("\\");
                        break;
                    case 1:
                        SetCursorPosition(26, 13);
                        Write("/");
                        break;
                    case 0:
                        SetCursorPosition(28, 13);
                        Write("\\");
                        break;
                }
            }
        }

        static void Versus()
        {
            janela(0, 0, 79, 24, 3, 0, 'd');
            janela(1, 1, 78, 3, 3, 0, 'd');
            janela(1, 21, 78, 23, 3, 0, 'd');

            string wordP1 = "";
            string wordP2 = "";

            // Não permite o player 1 passar sem digitar nada
            while (wordP1 == "")
            {
                SetCursorPosition(30, 2); Write("Player 1");
                SetCursorPosition(30, 22); Write("Digite uma palavra: ");
                wordP1 = ReadLine().ToUpper();
            }

            janela(1, 21, 78, 23, 3, 0, 'd');

            // Não permite o player 2 passar sem digitar nada
            while (wordP2 == "")
            {
                SetCursorPosition(30, 2); Write("Player 2");
                SetCursorPosition(30, 22); Write("Digite uma palavra: ");
                wordP2 = ReadLine().ToUpper();
            }

            GameState(wordP1, wordP2);

        }

        static void GameState(string wordP1, string wordP2)
        {
            //Variaveis Player 1
            char[] sortLettersP2 = wordP2.ToCharArray();
            char[] guessedLettersP1 = new char[sortLettersP2.Length];
            int lifesP1 = 6;
            char typedLetterP1;
            bool acertouP1 = false;
            string LettersP1 = "";

            //Variaveis Player 2
            char[] sortLettersP1 = wordP1.ToCharArray();
            char[] guessedLettersP2 = new char[sortLettersP1.Length];
            int lifesP2 = 6;
            char typedLetterP2;
            bool acertouP2 = false;
            string LettersP2 = "";

            //Variaveis Globais
            bool gameWin = false;

            // Passa a palavra digitada pelo player 2 em uma cadeia de char's
            for (int i = 0; i < wordP2.Length; i++)
            {
                guessedLettersP1[i] = '_';
            }

            // Passa a palavra digitada pelo player 1 em uma cadeia de char's
            for (int i = 0; i < wordP1.Length; i++)
            {
                guessedLettersP2[i] = '_';
            }

            while (gameWin == false)
            {
                // Limpa a tela toda vez que o Player 2 erra
                Clear();
                janela(0, 0, 79, 24, 3, 0, 'd');
                janela(1, 1, 78, 3, 3, 0, 'd');

                while (lifesP1 > 0 && acertouP1 == true || gameWin == false)
                {
                    switch (lifesP1)
                    {
                        case 5:
                            SetCursorPosition(27, 12);
                            Write("0");
                            break;
                        case 4:
                            SetCursorPosition(27, 12);
                            Write("0");
                            SetCursorPosition(27, 13);
                            Write("|");
                            break;
                        case 3:
                            SetCursorPosition(27, 12);
                            Write("0");
                            SetCursorPosition(27, 13);
                            Write("|");
                            SetCursorPosition(26, 14);
                            Write("/");
                            break;
                        case 2:
                            SetCursorPosition(27, 12);
                            Write("0");
                            SetCursorPosition(27, 13);
                            Write("|");
                            SetCursorPosition(26, 14);
                            Write("/");
                            SetCursorPosition(28, 14);
                            Write("\\");
                            break;
                        case 1:
                            SetCursorPosition(27, 12);
                            Write("0");
                            SetCursorPosition(27, 13);
                            Write("|");
                            SetCursorPosition(26, 14);
                            Write("/");
                            SetCursorPosition(28, 14);
                            Write("\\");
                            SetCursorPosition(26, 13);
                            Write("/");
                            break;
                        case 0:
                            SetCursorPosition(27, 12);
                            Write("0");
                            SetCursorPosition(27, 13);
                            Write("|");
                            SetCursorPosition(26, 14);
                            Write("/");
                            SetCursorPosition(28, 14);
                            Write("\\");
                            SetCursorPosition(26, 13);
                            Write("/");
                            SetCursorPosition(28, 13);
                            Write("\\");
                            break;
                    }

                    SetCursorPosition(30, 2); Write("Player 1");

                    // Desenha a Forca
                    SetCursorPosition(25, 11); Write("┌"); SetCursorPosition(26, 11); Write("─"); SetCursorPosition(27, 11); Write("─");
                    SetCursorPosition(25, 12); Write("│");
                    SetCursorPosition(25, 13); Write("│");
                    SetCursorPosition(25, 14); Write("│");

                    // Mostra a quantidade de letras para o usuário acertar
                    SetCursorPosition(30, 14); Write(guessedLettersP1);

                    // Mostra as letras digitadas pelo usuário
                    SetCursorPosition(3, 5);
                    Write("Letras digitadas: " + LettersP1);

                    SetCursorPosition(50, 5);
                    Write("Vidas: " + lifesP1); // Vidas do jogador 1

                    janela(1, 21, 78, 23, 3, 0, 'd');
                    SetCursorPosition(30, 22); WriteLine("Digite uma letra: ");
                    SetCursorPosition(49, 22); string input = ReadLine().ToUpper();


                    if (char.TryParse(input, out typedLetterP1)) // Verifica se o usuário digitou um char
                    {
                        for (int i = 0; i < wordP2.Length; i++)
                        {
                            if (sortLettersP2[i] == typedLetterP1)
                            {
                                guessedLettersP1[i] = typedLetterP1;
                                acertouP1 = true;
                            }
                        }

                        if (!LettersP1.Contains(typedLetterP1))
                        {
                            LettersP1 += (" " + typedLetterP1);
                        }

                        else if (LettersP1.Contains(typedLetterP1))
                        {
                            LettersP1 += ("" + null);
                            janela(1, 21, 78, 23, 3, 0, 'd');
                            SetCursorPosition(20, 22); Write("ESTA LETRA JA FOI DIGITADA");
                            Thread.Sleep(500);
                        }

                        if (!guessedLettersP1.Contains(typedLetterP1))
                        {
                            lifesP1--;
                            Thread.Sleep(500);
                            break;
                        }

                    }
                    else // Verifica se o usuário não digitou um char
                    {
                        janela(1, 21, 78, 23, 3, 0, 'd');
                        SetCursorPosition(20, 22); Write("SOMENTE LETRAS SÃO PERMITIDAS NESTE MODO");
                        Thread.Sleep(1000);
                    }

                    if (!guessedLettersP1.Contains('_')) // Verifica se não ha mais nenhum espaço vazio 
                    {
                        SetCursorPosition(30, 14);
                        Write(wordP2);
                        acertouP1 = false;
                        gameWin = true;
                        string winP1 = "Player 1 Venceu";
                        for (int i = 0; i < winP1.Length; i++) // Letreiro
                        {
                            string lettersWin = winP1.Substring(i, 1);
                            for (int e = 50; e > i; e--)
                            {
                                SetCursorPosition(e + 25, 17);
                                Write(lettersWin + " ");
                                Thread.Sleep(1);
                            }

                        }

                        // Muda a cor do texto
                        for (int i = 1; i < 12; i++)
                        {
                            janela(23, 16, 44, 18, 3, 15, 'd');
                            ForegroundColor = (ConsoleColor)i;
                            SetCursorPosition(25, 17);
                            Write(winP1.ToUpper() + "!!!");
                            Thread.Sleep(400);

                        }
                        Clear();
                        BackgroundColor = ConsoleColor.Black; // Altera a cor de fundo para Preto, novamente
                        break;
                    }


                }

                Clear();
                janela(0, 0, 79, 24, 3, 0, 'd');
                janela(1, 1, 78, 3, 3, 0, 'd');

                while (lifesP2 > 0 && acertouP2 == true || gameWin == false)
                {
                    if (!guessedLettersP1.Contains('_'))
                    {
                        SetCursorPosition(30, 14);
                        Write(wordP2);
                        acertouP1 = false;
                        gameWin = true;
                        string winP1 = "Player 1 Venceu";
                        for (int i = 0; i < winP1.Length; i++)
                        {
                            string lettersWin = winP1.Substring(i, 1);
                            for (int e = 50; e > i; e--)
                            {
                                SetCursorPosition(e + 25, 17);
                                Write(lettersWin + " ");
                                Thread.Sleep(1);
                            }

                        }

                        for (int i = 1; i < 12; i++)
                        {
                            janela(23, 16, 44, 18, 3, 15, 'd');
                            ForegroundColor = (ConsoleColor)i;
                            SetCursorPosition(25, 17);
                            Write(winP1.ToUpper() + "!!!");
                            Thread.Sleep(400);

                        }
                        Clear();
                        BackgroundColor = ConsoleColor.Black;
                        break;
                    }
                    switch (lifesP2)
                    {
                        case 5:
                            SetCursorPosition(27, 12);
                            Write("0");
                            break;
                        case 4:
                            SetCursorPosition(27, 12);
                            Write("0");
                            SetCursorPosition(27, 13);
                            Write("|");
                            break;
                        case 3:
                            SetCursorPosition(27, 12);
                            Write("0");
                            SetCursorPosition(27, 13);
                            Write("|");
                            SetCursorPosition(26, 14);
                            Write("/");
                            break;
                        case 2:
                            SetCursorPosition(27, 12);
                            Write("0");
                            SetCursorPosition(27, 13);
                            Write("|");
                            SetCursorPosition(26, 14);
                            Write("/");
                            SetCursorPosition(28, 14);
                            Write("\\");
                            break;
                        case 1:
                            SetCursorPosition(27, 12);
                            Write("0");
                            SetCursorPosition(27, 13);
                            Write("|");
                            SetCursorPosition(26, 14);
                            Write("/");
                            SetCursorPosition(28, 14);
                            Write("\\");
                            SetCursorPosition(26, 13);
                            Write("/");
                            break;
                        case 0:
                            SetCursorPosition(27, 12);
                            Write("0");
                            SetCursorPosition(27, 13);
                            Write("|");
                            SetCursorPosition(26, 14);
                            Write("/");
                            SetCursorPosition(28, 14);
                            Write("\\");
                            SetCursorPosition(26, 13);
                            Write("/");
                            SetCursorPosition(28, 13);
                            Write("\\");
                            break;
                    }

                    SetCursorPosition(30, 2); Write("Player 2");

                    SetCursorPosition(25, 11); Write("┌"); SetCursorPosition(26, 11); Write("─"); SetCursorPosition(27, 11); Write("─");
                    SetCursorPosition(25, 12); Write("│");
                    SetCursorPosition(25, 13); Write("│");
                    SetCursorPosition(25, 14); Write("│");

                    SetCursorPosition(30, 14); Write(guessedLettersP2);

                    SetCursorPosition(3, 5);
                    Write("Letras digitadas: " + LettersP2);

                    SetCursorPosition(50, 5);
                    Write("Vidas: " + lifesP2);

                    janela(1, 21, 78, 23, 3, 0, 'd');
                    SetCursorPosition(30, 22); WriteLine("Digite uma letra: ");
                    SetCursorPosition(49, 22); string input = ReadLine().ToUpper();

                    if (char.TryParse(input, out typedLetterP2))
                    {
                        for (int i = 0; i < wordP1.Length; i++)
                        {
                            if (sortLettersP1[i] == typedLetterP2)
                            {
                                guessedLettersP2[i] = typedLetterP2;
                                acertouP2 = true;
                            }
                        }

                        if (!LettersP2.Contains(typedLetterP2))
                        {
                            LettersP2 += (" " + typedLetterP2);
                        }

                        else if (LettersP2.Contains(typedLetterP2))
                        {
                            janela(1, 21, 78, 23, 3, 0, 'd');
                            SetCursorPosition(20, 22); Write("ESTA LETRA JA FOI DIGITADA");
                            Thread.Sleep(500);
                        }


                        if (!guessedLettersP2.Contains(typedLetterP2))
                        {
                            lifesP2--;
                            Thread.Sleep(500);
                            break;
                        }

                    }
                    else
                    {
                        janela(1, 21, 78, 23, 3, 0, 'd');
                        SetCursorPosition(20, 22); Write("SOMENTE LETRAS SÃO PERMITIDAS NESTE MODO");
                        Thread.Sleep(1000);
                    }


                    if (!guessedLettersP2.Contains('_'))
                    {
                        SetCursorPosition(30, 14);
                        Write(wordP1);
                        acertouP2 = false;
                        gameWin = true;
                        string winP2 = "Player 2 Venceu";
                        for (int i = 0; i < winP2.Length; i++)
                        {
                            string lettersWin = winP2.Substring(i, 1);
                            for (int e = 50; e > i; e--)
                            {
                                SetCursorPosition(e + 25, 17);
                                Write(lettersWin + " ");
                                Thread.Sleep(1);
                            }

                        }

                        for (int i = 1; i < 12; i++)
                        {
                            janela(23, 16, 44, 18, 3, 15, 'd');
                            ForegroundColor = (ConsoleColor)i;
                            SetCursorPosition(25, 17);
                            Write(winP2.ToUpper() + "!!!");
                            Thread.Sleep(400);

                        }
                        Clear();
                        BackgroundColor = ConsoleColor.Black;
                        break;
                    }

                }

                if (lifesP1 == 0 && lifesP2 == 0)
                {
                    for (int i = 1; i < 6; i++)
                    {
                        string draw = "EMPATE";
                        janela(23, 16, 44, 18, 3, 15, 'd');
                        ForegroundColor = (ConsoleColor)i;
                        SetCursorPosition(25, 17);
                        Write(draw.ToUpper() + "!!!");
                        Thread.Sleep(400);

                    }

                    Clear();
                    BackgroundColor = ConsoleColor.Black;
                    break;
                }
            }



        }



    }

}