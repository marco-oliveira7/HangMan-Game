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
            while (numOpc != 1 || numOpc != 2)
            {
                janela(0, 0, 79, 24, 3, 0, 'd');
                janela(1, 1, 78, 3, 3, 0, 'd');
                janela(1, 21, 78, 23, 3, 0, 'd');
                SetCursorPosition(30, 2); Write("* * * Forca * * *");
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

                else if (char.TryParse(opc, out char charOpc))
                {
                    janela(1, 21, 78, 23, 3, 0, 'd');
                    SetCursorPosition(30, 22); Write("ESCOLHA UMA OPÇÃO VALIDA");
                    Thread.Sleep(1000);
                }

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
            string[] palavras = new string[] {
                "Mesa", "Cadeira", "Computador", "Telefone", "Livro", "Janela", "Porta", "Lampada", "Carro", "Relogio",
                "Televisao", "Copo", "Teclado", "Mouse", "Ventilador", "Espelho", "Armario", "Cama", "Quadro", "Tapete",
                "Pizza", "Hamburguer", "Sushi", "Macarrao", "Salada", "Feijoada", "Taco", "Bolo", "Sorvete", "Churrasco",
                "Lasagna", "Pao", "Pastel", "Coxinha", "Empada", "Donuts", "Panqueca", "Batata frita", "Pipoca", "Torta",
                "Brasil", "Canada", "Japao", "Alemanha", "Franca", "Italia", "Argentina", "Australia", "Egito", "India",
                "China", "Espanha", "Mexico", "Russia", "Coreia do Sul", "Grecia", "Portugal", "Suecia", "Africa do Sul", "Chile"
            };


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


            for (int i = 0; i < palavraSorteada.Length; i++)
            {
                progresso[i] = '_';
            }
            janela(0, 0, 79, 24, 3, 0, 'd');
            janela(1, 1, 78, 3, 3, 0, 'd');
            janela(1, 21, 78, 23, 3, 0, 'd');

            while (vidas > 0 && !vitoria)
            {

                SetCursorPosition(55, 5); Write("Vidas restantes: " + vidas);

                SetCursorPosition(25, 11); Write("┌"); SetCursorPosition(26, 11); Write("─"); SetCursorPosition(27, 11); Write("─");
                SetCursorPosition(25, 12); Write("│");
                SetCursorPosition(25, 13); Write("│");
                SetCursorPosition(25, 14); Write("│");

                SetCursorPosition(30, 14); Write(progresso);

                if (letras.Count > 0)
                {
                    SetCursorPosition(2, 5);
                    WriteLine("Letras digitadas: " + string.Join(", ", letras));
                }

                janela(1, 21, 78, 23, 3, 0, 'd');
                SetCursorPosition(2, 20); WriteLine("Digite 'dica' para dica: ");
                SetCursorPosition(30, 22); WriteLine("Digite uma letra: ");
                SetCursorPosition(47, 22); string input = ReadLine().ToUpper();

                bool acertou = false;


                if (char.TryParse(input, out letraDigitada))
                {
                    if (letraDigitada == '0' || letraDigitada == '1' || letraDigitada == '2' || letraDigitada == '3' || letraDigitada == '4' || letraDigitada == '5' || letraDigitada == '6' || letraDigitada == '7' || letraDigitada == '8' || letraDigitada == '9')
                    {
                        janela(1, 21, 78, 23, 3, 0, 'd');
                        SetCursorPosition(20, 22); Write("NÚMEROS NÃO SÃO PERMITIDOS NESTE MODO!!");
                        Thread.Sleep(2000);
                    }

                    else if (!letras.Contains(letraDigitada))
                    {
                        letras.Add(letraDigitada);
                    }

                    else
                    {
                        SetCursorPosition(30, 22);
                        Write("Esta letra já foi digitada");
                        Thread.Sleep(1500);
                    }

                    for (int i = 0; i < palavraSorteada.Length; i++)
                    {
                        if (letrasSorteadas[i] == letraDigitada && progresso[i] == '_')
                        {
                            progresso[i] = letraDigitada;
                            acertou = true;
                        }
                    }
                    if (acertou == false)
                    {
                        vidas--;
                    }

                }

                else if (int.TryParse(input, out int num) || double.TryParse(input, out double num2))
                {
                    janela(1, 21, 78, 23, 3, 0, 'd');
                    SetCursorPosition(20, 22); Write("NÚMEROS NÃO SÃO PERMITIDOS NESTE MODO!!");
                    Thread.Sleep(2000);
                }
                else if (input.Equals(dica))
                {
                    SetCursorPosition(27, 20);
                    Write(dicaSorteada);
                }
                else
                {
                    janela(1, 21, 78, 23, 3, 0, 'd');
                    SetCursorPosition(20, 22); Write("SOMENTE LETRAS SÃO PERMITIDAS NESTE MODO");
                    Thread.Sleep(2000);
                }



                if (!progresso.Contains('_') && vidas >= 1)
                {
                    vitoria = true;
                    SetCursorPosition(30, 14);
                    Write(palavraSorteada);
                    string venceu = "VOCÊ VENCEU!!!";
                    for (int i = 0; i < venceu.Length; i++)
                    {
                        SetCursorPosition(95, 17);
                        string vLetra = venceu.Substring(i, 1);
                        for (int e = 75; e > i + 30; e--)
                        {
                            SetCursorPosition(e - 1, 17);
                            Write(vLetra + " ");
                            System.Threading.Thread.Sleep(10);
                        }

                    }
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


            while (wordP1 == "")
            {
                SetCursorPosition(30, 2); Write("Player 1");
                SetCursorPosition(30, 22); Write("Digite uma palavra: ");
                wordP1 = ReadLine();
            }



            janela(1, 21, 78, 23, 3, 0, 'd');

            while (wordP2 == "")
            {
                SetCursorPosition(30, 2); Write("Player 2");
                SetCursorPosition(30, 22); Write("Digite uma palavra: ");
                wordP2 = ReadLine();
            }

            Player1(wordP1, wordP2);

        }

        static void Player1(string wordP1, string wordP2)
        {
            char[] sortLettersP2 = wordP2.ToCharArray();
            char[] progressoP2 = new char[sortLettersP2.Length];
            List<char> lettersP2 = new List<char>();
            int lifesP1 = 6;
            char typedLetterP1;
            int vLifesP1 = 7;
            vLifesP1--;

            for (int i = 0; i < wordP2.Length; i++)
            {
                progressoP2[i] = '_';
            }

            SetCursorPosition(30, 2); Write("Player 1");

            while (vLifesP1 == lifesP1)
            {
                SetCursorPosition(55, 5); Write("Vidas restantes: " + lifesP1);

                SetCursorPosition(25, 11); Write("┌"); SetCursorPosition(26, 11); Write("─"); SetCursorPosition(27, 11); Write("─");
                SetCursorPosition(25, 12); Write("│");
                SetCursorPosition(25, 13); Write("│");
                SetCursorPosition(25, 14); Write("│");

                SetCursorPosition(30, 14); Write(progressoP2);

                janela(1, 21, 78, 23, 3, 0, 'd');
                SetCursorPosition(30, 22); WriteLine("Digite uma letra: ");
                SetCursorPosition(60, 22); string input = ReadLine();

                bool acertou = false;

                if (char.TryParse(input, out typedLetterP1))
                {
                    for (int i = 0; i < wordP2.Length; i++)
                    {
                        if (sortLettersP2[i] == typedLetterP1)
                        {
                            progressoP2[i] = typedLetterP1;
                            acertou = true;
                        }
                    }
                    if (acertou == false)
                    {
                        lifesP1--;
                        Thread.Sleep(1500);
                        string completedWordP2 = new string(progressoP2);
                        SetCursorPosition(30, 14); Write(completedWordP2);
                        Clear();
                        janela(0, 0, 79, 24, 3, 0, 'd');
                        janela(1, 1, 78, 3, 3, 0, 'd');
                        janela(1, 21, 78, 23, 3, 0, 'd');
                        Player2(wordP1, wordP2);
                    }
                    else
                    {

                    }
                }

                else if (int.TryParse(input, out int num))
                {
                    janela(1, 21, 78, 23, 3, 0, 'd');
                    SetCursorPosition(20, 22); Write("NÚMEROS NÃO SÃO PERMITIDOS NESTE MODO!!");
                    Thread.Sleep(2000);
                }

                else
                {
                    janela(1, 21, 78, 23, 3, 0, 'd');
                    SetCursorPosition(20, 22); Write("SOMENTE LETRAS SÃO PERMITIDAS NESTE MODO");
                    Thread.Sleep(2000);
                }
            }
        }

        static void Player2(string wordP1, string wordP2)
        {
            char[] sortLettersP1 = wordP1.ToCharArray();
            char[] progressoP1 = new char[sortLettersP1.Length];
            List<char> lettersP1 = new List<char>();
            int lifesP2 = 6;
            char typedLetterP2;
            int vLifesP2 = 7;
            vLifesP2--;

            for (int i = 0; i < wordP1.Length; i++)
            {
                progressoP1[i] = '_';
            }

            SetCursorPosition(30, 2); Write("Player 2");

            while (vLifesP2 == lifesP2)
            {

                SetCursorPosition(55, 5); Write("Vidas restantes: " + lifesP2);

                SetCursorPosition(25, 11); Write("┌"); SetCursorPosition(26, 11); Write("─"); SetCursorPosition(27, 11); Write("─");
                SetCursorPosition(25, 12); Write("│");
                SetCursorPosition(25, 13); Write("│");
                SetCursorPosition(25, 14); Write("│");

                SetCursorPosition(30, 14); Write(progressoP1);

                janela(1, 21, 78, 23, 3, 0, 'd');
                SetCursorPosition(30, 22); WriteLine("Digite uma letra: ");
                SetCursorPosition(60, 22); string input = ReadLine();

                bool acertou = false;

                if (char.TryParse(input, out typedLetterP2))
                {
                    for (int i = 0; i < wordP1.Length; i++)
                    {
                        if (sortLettersP1[i] == typedLetterP2)
                        {
                            progressoP1[i] = typedLetterP2;
                            acertou = true;
                        }
                    }
                    if (acertou == false)
                    {
                        lifesP2--;
                        Thread.Sleep(1500);
                        Clear();
                        janela(0, 0, 79, 24, 3, 0, 'd');
                        janela(1, 1, 78, 3, 3, 0, 'd');
                        janela(1, 21, 78, 23, 3, 0, 'd');
                        Player1(wordP1, wordP2);
                    }
                    else
                    {

                    }
                }

                else if (int.TryParse(input, out int num))
                {
                    janela(1, 21, 78, 23, 3, 0, 'd');
                    SetCursorPosition(20, 22); Write("NÚMEROS NÃO SÃO PERMITIDOS NESTE MODO!!");
                    Thread.Sleep(2000);
                }

                else
                {
                    janela(1, 21, 78, 23, 3, 0, 'd');
                    SetCursorPosition(20, 22); Write("SOMENTE LETRAS SÃO PERMITIDAS NESTE MODO");
                    Thread.Sleep(2000);
                }
            }

        }

    }

}