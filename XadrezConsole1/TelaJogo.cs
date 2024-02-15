using System;
using CamadaTabuleiro;

namespace XadrezConsole1
{
    internal class TelaJogo
    {
        public static void ImprimirTabuleiro(Tabuleiro tab)
        {
            for (int i = 0; i < tab.Linhas; i++)
            {
                for (int j = 0; j < tab.Colunas; j++)
                {
                    if (tab.PecaNaPosicao(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        Console.Write($"{tab.PecaNaPosicao(i, j)} ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
