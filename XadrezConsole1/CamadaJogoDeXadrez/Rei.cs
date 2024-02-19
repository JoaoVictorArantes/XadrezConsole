using CamadaTabuleiro;
using System;
using System.Runtime.ConstrainedExecution;


namespace CamadaJogoDeXadrez
{
    internal class Rei : Peca
    {
        public Rei(Tabuleiro tab, CorPeca cor): base(tab, cor)
        {

        }
        public override string ToString()
        {
            return "R";
        }
        private bool podeMover(Posicao pos)
        {
            Peca p = Tab.PecaNaPosicao(pos);
            return p == null || p.Cor != Cor;
        }
        public override bool[,] MovimentosPossiveis()
        {
            bool[,] MatrixAux = new bool[Tab.Linhas, Tab.Colunas];

            Posicao pos = new Posicao(0, 0);

            //norte
            pos.DefinirValores(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna);
            if (Tab.PosicaoValida(pos) && podeMover(pos))// se não chegar no fim do tabuleiro ou não tiver peça adversária no caminho.
            {
                MatrixAux[pos.Linha, pos.Coluna] = true;
            }
            //nordeste
            pos.DefinirValores(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna + 1);
            if (Tab.PosicaoValida(pos) && podeMover(pos))
            {
                MatrixAux[pos.Linha, pos.Coluna] = true;
            }
            //leste
            pos.DefinirValores(PosicaoPeca.Linha, PosicaoPeca.Coluna + 1);
            if (Tab.PosicaoValida(pos) && podeMover(pos))
            {
                MatrixAux[pos.Linha, pos.Coluna] = true;
            }
            //sudeste
            pos.DefinirValores(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna + 1);
            if (Tab.PosicaoValida(pos) && podeMover(pos))
            {
                MatrixAux[pos.Linha, pos.Coluna] = true;
            }
            //sul
            pos.DefinirValores(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna);
            if (Tab.PosicaoValida(pos) && podeMover(pos))
            {
                MatrixAux[pos.Linha, pos.Coluna] = true;
            }
            //sudoeste
            pos.DefinirValores(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna - 1);
            if (Tab.PosicaoValida(pos) && podeMover(pos))
            {
                MatrixAux[pos.Linha, pos.Coluna] = true;
            }
            //oeste
            pos.DefinirValores(PosicaoPeca.Linha, PosicaoPeca.Coluna - 1);
            if (Tab.PosicaoValida(pos) && podeMover(pos))
            {
                MatrixAux[pos.Linha, pos.Coluna] = true;
            }
            //noroeste
            pos.DefinirValores(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna - 1);
            if (Tab.PosicaoValida(pos) && podeMover(pos))
            {
                MatrixAux[pos.Linha, pos.Coluna] = true;
            }

            return MatrixAux;
        }




    }
}
