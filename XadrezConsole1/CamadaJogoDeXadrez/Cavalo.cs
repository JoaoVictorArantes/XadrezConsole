using CamadaTabuleiro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XadrezConsole1.CamadaJogoDeXadrez
{
    internal class Cavalo : Peca
    {
        public Cavalo(Tabuleiro tab, CorPeca cor) : base(tab, cor)
        {

        }
        public override string ToString()
        {
            return "C";
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

            pos.DefinirValores(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna - 2);
            if (Tab.PosicaoValida(pos) && podeMover(pos))
            {
                MatrixAux[pos.Linha, pos.Coluna] = true;
            }
            pos.DefinirValores(PosicaoPeca.Linha - 2, PosicaoPeca.Coluna - 1);
            if (Tab.PosicaoValida(pos) && podeMover(pos))
            {
                MatrixAux[pos.Linha, pos.Coluna] = true;
            }
            pos.DefinirValores(PosicaoPeca.Linha - 2, PosicaoPeca.Coluna + 1);
            if (Tab.PosicaoValida(pos) && podeMover(pos))
            {
                MatrixAux[pos.Linha, pos.Coluna] = true;
            }
            pos.DefinirValores(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna + 2);
            if (Tab.PosicaoValida(pos) && podeMover(pos))
            {
                MatrixAux[pos.Linha, pos.Coluna] = true;
            }
            pos.DefinirValores(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna + 2);
            if (Tab.PosicaoValida(pos) && podeMover(pos))
            {
                MatrixAux[pos.Linha, pos.Coluna] = true;
            }
            pos.DefinirValores(PosicaoPeca.Linha + 2, PosicaoPeca.Coluna + 1);
            if (Tab.PosicaoValida(pos) && podeMover(pos))
            {
                MatrixAux[pos.Linha, pos.Coluna] = true;
            }
            pos.DefinirValores(PosicaoPeca.Linha + 2, PosicaoPeca.Coluna - 1);
            if (Tab.PosicaoValida(pos) && podeMover(pos))
            {
                MatrixAux[pos.Linha, pos.Coluna] = true;
            }
            pos.DefinirValores(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna - 2);
            if (Tab.PosicaoValida(pos) && podeMover(pos))
            {
                MatrixAux[pos.Linha, pos.Coluna] = true;
            }


            return MatrixAux;
        }
    }
}
