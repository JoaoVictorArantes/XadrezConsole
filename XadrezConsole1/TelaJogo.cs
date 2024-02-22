using System;
using System.Runtime.Intrinsics.X86;
using System.Collections.Generic;
using CamadaTabuleiro;
using CamadaJogoDeXadrez;

namespace XadrezConsole1
{
    internal class TelaJogo
    {
        public static void ImprimirPartida(PartidaDeXadrez partida)
        {
            ImprimirTabuleiro(partida.Tab);
            Console.WriteLine();
            ImprimirPecasCaptuadas(partida);
            Console.WriteLine();
            Console.WriteLine($"Turno: {partida.Turno}");

            if (!partida.PartidaTerminada)
            {
                Console.WriteLine($"Aguardando jogada: {partida.JogadorAtual}");

                if (partida.SeEstouEmXeque)
                {
                    Console.WriteLine("XEQUE!!! >.<");
                }
            }

            else
            {
                Console.WriteLine("Xeque-Mate!!! :)");

                Console.WriteLine($"Vencedor: {partida.JogadorAtual}");
            }
        }
        public static void ImprimirPecasCaptuadas(PartidaDeXadrez partida)
        {
            Console.WriteLine("\nPeças capturadas: ");

            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("Brancas: ");
            ImprimirConjunto(partida.PecasCapturadas(CorPeca.Branca));
            Console.WriteLine();
            

            
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Pretas: ");
            ImprimirConjunto(partida.PecasCapturadas(CorPeca.Preta));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }
        public static void ImprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write("[");
            foreach (Peca peca in conjunto)
            {
                Console.Write(peca + " ");
            }
            Console.Write("]");

        }
        public static void ImprimirTabuleiro(Tabuleiro tab)
        {
            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write(8 - i + "  ");
                for (int j = 0; j < tab.Colunas; j++)
                {
                    ImprimirPeca(tab.PecaNaPosicao(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("\n   A B C D E F G H");
        }
        public static void ImprimirTabuleiro(Tabuleiro tab, bool[,] PosicoesPossiveis)
        {
            ConsoleColor FundoOriginal = Console.BackgroundColor; // peguei a cor do fundo
            ConsoleColor FundoAlterado = ConsoleColor.Cyan; // peguei a cor do fundo

            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write(8 - i + "  ");
                for (int j = 0; j < tab.Colunas; j++)
                {
                    if (PosicoesPossiveis[i,j])//se a posicao estiver marcada como possivel movimento
                    {
                        Console.BackgroundColor = FundoAlterado;// muda o fundo para 'cyan'
                    }
                    else
                    {
                        Console.BackgroundColor = FundoOriginal;// mantem a cor original em caso de movimento nao possivel
                    }
                    ImprimirPeca(tab.PecaNaPosicao(i, j));
                    Console.BackgroundColor = FundoOriginal;
                }
                Console.WriteLine();
            }
            Console.WriteLine("\n   A B C D E F G H");
            Console.BackgroundColor = FundoOriginal;

        }
        public static PosicaoXadrez LerPosicaoXadrez()
        {
            string StringAux = Console.ReadLine();
            StringAux.ToLower();
            char coluna = StringAux[0];
            int Linha = int.Parse(StringAux[1] + "");
            return new PosicaoXadrez(coluna, Linha);
        }

        public static void ImprimirPeca(Peca peca)
        {
            if (peca == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (peca.Cor == CorPeca.Branca)
                {
                    ConsoleColor aux1 = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(peca);
                    Console.ForegroundColor = aux1;
                }
                else
                {
                    ConsoleColor aux2 = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(peca);
                    Console.ForegroundColor = aux2;
                }
                Console.Write(" ");
            }
        }
    }
}
