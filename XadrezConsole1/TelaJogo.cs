using System;
using System.Runtime.Intrinsics.X86;
using CamadaTabuleiro;
using CamadaJogoDeXadrez;

namespace XadrezConsole1
{
    internal class TelaJogo
    {
        public static void ImprimirTabuleiro(Tabuleiro tab)
        {
            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write(8 - i + "  ");
                for (int j = 0; j < tab.Colunas; j++)
                {
                    if (tab.PecaNaPosicao(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        //Console.Write($"{tab.PecaNaPosicao(i, j)} ");
                        ImprimirPeca(tab.PecaNaPosicao(i, j));

                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("\n   A B C D E F G H");
        }
        public static PosicaoXadrez LerPosicaoXadrez()
        {
            string StringAux = Console.ReadLine();
            StringAux.ToLower();
            char coluna = StringAux[0];
            int  Linha = int.Parse(StringAux[1] + "");
            return new PosicaoXadrez(coluna, Linha);
        }

        public static void ImprimirPeca(Peca peca)
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
        }

    }
}
