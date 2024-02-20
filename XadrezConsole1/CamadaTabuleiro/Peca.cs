using System;
using System.Runtime.ConstrainedExecution;


namespace CamadaTabuleiro
{
    abstract class Peca
    {
        public Posicao PosicaoPeca { get; set; }
        public CorPeca Cor { get; protected set; }

        public int QuantidadeMovimentos { get; protected set; }

        public Tabuleiro Tab { get; protected set; }

        public Peca(Tabuleiro tab, CorPeca cor)
        {
            PosicaoPeca = null;
            Cor = cor;
            Tab = tab;
            QuantidadeMovimentos = 0;
        }

        public void IncrementarQuantidadeDeMovimentos()
        {
            QuantidadeMovimentos++;
        }
        public bool PodeMoverPara(Posicao posicao)
        {
            return MovimentosPossiveis()[posicao.Linha, posicao.Coluna];
        }


        public bool ExisteMovimentosPossiveis()// verifica se na matriz de  MovimentosPossiveis() existe pelomenos 1 movimento válido
        {
            bool[,] Mat = MovimentosPossiveis();

            for (int i = 0; i < Tab.Linhas; i++)
            {
                for (int j = 0; j < Tab.Colunas; j++)
                {
                    if (Mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public abstract bool[,] MovimentosPossiveis();





    }
}
