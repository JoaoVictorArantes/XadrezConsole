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

        public abstract bool[,] MovimentosPossiveis();





    }
}
