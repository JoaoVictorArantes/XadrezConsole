using CamadaTabuleiro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace XadrezConsole1.CamadaJogoDeXadrez
{
    internal class Bispo : Peca
    {
        public Bispo(Tabuleiro tab, CorPeca cor) : base(tab, cor)
        {

        }
        public override string ToString()
        {
            return "B";
        }
        private bool PodeMover(Posicao Pos)
        {
            Peca p = Tab.PecaNaPosicao(Pos);
            return p == null || p.Cor != Cor;
        }
        public override bool[,] MovimentosPossiveis()
        {
            bool[,] MatrixAux = new bool[Tab.Linhas, Tab.Colunas];

            Posicao pos = new Posicao(0, 0);
            
            // NOROESTE
            pos.DefinirValores(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna - 1);
            while (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                MatrixAux[pos.Linha, pos.Coluna] = true;
                if (
                    Tab.PecaNaPosicao(pos) != null && Tab.PecaNaPosicao(pos).Cor != Cor)
                {
                    break;
                }
                pos.DefinirValores(pos.Linha - 1, pos.Coluna - 1);
            }

            // NORDESTE
            pos.DefinirValores(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna + 1);
            while (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                MatrixAux[pos.Linha, pos.Coluna] = true;
                if (Tab.PecaNaPosicao(pos) != null && Tab.PecaNaPosicao(pos).Cor != Cor)
                {
                    break;
                }
                pos.DefinirValores(pos.Linha - 1, pos.Coluna + 1);
            }

            // SUDESTE
            pos.DefinirValores(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna + 1);
            while (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                MatrixAux[pos.Linha, pos.Coluna] = true;
                if (Tab.PecaNaPosicao(pos) != null && Tab.PecaNaPosicao(pos).Cor != Cor)
                {
                    break;
                }
                pos.DefinirValores(pos.Linha + 1, pos.Coluna + 1);
            }

            // SUDOESTE
            pos.DefinirValores(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna - 1);
            while (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                MatrixAux[pos.Linha, pos.Coluna] = true;
                if (Tab.PecaNaPosicao(pos) != null && Tab.PecaNaPosicao(pos).Cor != Cor)
                {
                    break;
                }
                pos.DefinirValores(pos.Linha + 1, pos.Coluna - 1);
            }
            return MatrixAux;
        }
    }
}
