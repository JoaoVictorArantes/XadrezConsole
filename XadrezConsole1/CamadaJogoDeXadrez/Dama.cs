﻿using CamadaTabuleiro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XadrezConsole1.CamadaJogoDeXadrez
{
    internal class Dama : Peca
    {
        public Dama(Tabuleiro tab, CorPeca cor) : base(tab, cor)
        {

        }
        public override string ToString()
        {
            return "D";
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
            while (Tab.PosicaoValida(pos) && podeMover(pos))// enquanto nao estiver no fim do tabuleiro e nao houver peças inimigas no caminho;
            {
                MatrixAux[pos.Linha, pos.Coluna] = true;
                if (Tab.PecaNaPosicao(pos) != null && Tab.PecaNaPosicao(pos).Cor != Cor) // se houver alguma peça no caminho e ela for de outra cor(adversária)
                {
                    break;
                }
                pos.Linha = pos.Linha - 1;

            }
            //nordeste
            pos.DefinirValores(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna + 1);
            while (Tab.PosicaoValida(pos) && podeMover(pos))// enquanto nao estiver no fim do tabuleiro e nao houver peças inimigas no caminho;
            {
                MatrixAux[pos.Linha, pos.Coluna] = true;
                if (Tab.PecaNaPosicao(pos) != null && Tab.PecaNaPosicao(pos).Cor != Cor) // se houver alguma peça no caminho e ela for de outra cor(adversária)
                {
                    break;
                }
                
                pos.DefinirValores(pos.Linha - 1, pos.Coluna + 1);
            }

            //leste 
            pos.DefinirValores(PosicaoPeca.Linha, PosicaoPeca.Coluna + 1);
            while (Tab.PosicaoValida(pos) && podeMover(pos))// enquanto nao estiver no fim do tabuleiro e nao houver peças inimigas no caminho;
            {
                MatrixAux[pos.Linha, pos.Coluna] = true;
                if (Tab.PecaNaPosicao(pos) != null && Tab.PecaNaPosicao(pos).Cor != Cor) // se houver alguma peça no caminho e ela for de outra cor(adversária)
                {
                    break;
                }
                pos.Coluna = pos.Coluna + 1;
            }
            //sudeste
            pos.DefinirValores(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna + 1);
            while (Tab.PosicaoValida(pos) && podeMover(pos))// enquanto nao estiver no fim do tabuleiro e nao houver peças inimigas no caminho;
            {
                MatrixAux[pos.Linha, pos.Coluna] = true;
                if (Tab.PecaNaPosicao(pos) != null && Tab.PecaNaPosicao(pos).Cor != Cor) // se houver alguma peça no caminho e ela for de outra cor(adversária)
                {
                    break;
                }
                
                pos.DefinirValores(pos.Linha + 1, pos.Coluna + 1);
            }

            //sul
            pos.DefinirValores(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna);
            while (Tab.PosicaoValida(pos) && podeMover(pos))// enquanto nao estiver no fim do tabuleiro e nao houver peças inimigas no caminho;
            {
                MatrixAux[pos.Linha, pos.Coluna] = true;
                if (Tab.PecaNaPosicao(pos) != null && Tab.PecaNaPosicao(pos).Cor != Cor) // se houver alguma peça no caminho e ela for de outra cor(adversária)
                {
                    break;
                }
                pos.Linha = pos.Linha + 1;
            }

            //sudoeste
            pos.DefinirValores(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna - 1);
            while (Tab.PosicaoValida(pos) && podeMover(pos))// enquanto nao estiver no fim do tabuleiro e nao houver peças inimigas no caminho;
            {
                MatrixAux[pos.Linha, pos.Coluna] = true;
                if (Tab.PecaNaPosicao(pos) != null && Tab.PecaNaPosicao(pos).Cor != Cor) // se houver alguma peça no caminho e ela for de outra cor(adversária)
                {
                    break;
                }
                
                pos.DefinirValores(pos.Linha + 1, pos.Coluna - 1);
            }
            //oeste
            pos.DefinirValores(PosicaoPeca.Linha, PosicaoPeca.Coluna - 1);
            while (Tab.PosicaoValida(pos) && podeMover(pos))// enquanto nao estiver no fim do tabuleiro e nao houver peças inimigas no caminho;
            {
                MatrixAux[pos.Linha, pos.Coluna] = true;
                if (Tab.PecaNaPosicao(pos) != null && Tab.PecaNaPosicao(pos).Cor != Cor) // se houver alguma peça no caminho e ela for de outra cor(adversária)
                {
                    break;
                }
                pos.Coluna = pos.Coluna - 1;
            }
            //noroeste
            pos.DefinirValores(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna - 1);
            while (Tab.PosicaoValida(pos) && podeMover(pos))// enquanto nao estiver no fim do tabuleiro e nao houver peças inimigas no caminho;
            {
                MatrixAux[pos.Linha, pos.Coluna] = true;
                if (Tab.PecaNaPosicao(pos) != null && Tab.PecaNaPosicao(pos).Cor != Cor) // se houver alguma peça no caminho e ela for de outra cor(adversária)
                {
                    break;
                }
                
                pos.DefinirValores(pos.Linha - 1, pos.Coluna - 1);
            }

            return MatrixAux;
        }
    }
}
