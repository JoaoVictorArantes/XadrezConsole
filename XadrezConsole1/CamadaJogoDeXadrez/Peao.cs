using CamadaTabuleiro;
using System;
using System.Runtime.ConstrainedExecution;


namespace CamadaJogoDeXadrez
{
    internal class Peao : Peca
    {
        public Peao(Tabuleiro tab, CorPeca cor) : base(tab, cor)
        {

        }
        public override string ToString()
        {
            return "P";
        }

        private bool existeInimigo(Posicao pos)
        {
            Peca p = Tab.PecaNaPosicao(pos);
            return p != null && p.Cor != Cor;
        }

        private bool livre(Posicao pos)
        {
            return Tab.PecaNaPosicao(pos) == null;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];

            Posicao pos = new Posicao(0, 0);

            if (Cor == CorPeca.Branca)
            {
                pos.DefinirValores(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna);
                if (Tab.PosicaoValida(pos) && livre(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(PosicaoPeca.Linha - 2, PosicaoPeca.Coluna);
                Posicao p2 = new Posicao(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna);
                if (Tab.PosicaoValida(p2) && livre(p2) && Tab.PosicaoValida(pos) && livre(pos) && QuantidadeMovimentos == 0)
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna - 1);
                if (Tab.PosicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna + 1);
                if (Tab.PosicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                /*
                // #jogadaespecial en passant
                if (PosicaoPeca.Linha == 3)
                {
                    Posicao esquerda = new Posicao(PosicaoPeca.Linha, PosicaoPeca.Coluna - 1);
                    if (Tab.PosicaoValida(esquerda) && existeInimigo(esquerda) && Tab.PecaNaPosicao(esquerda) == partida.vulneravelEnPassant)
                    {
                        mat[esquerda.Linha - 1, esquerda.Coluna] = true;
                    }
                    Posicao direita = new Posicao(PosicaoPeca.Linha, PosicaoPeca.Coluna + 1);
                    if (Tab.PosicaoValida(direita) && existeInimigo(direita) && Tab.PecaNaPosicao(direita) == partida.vulneravelEnPassant)
                    {
                        mat[direita.Linha - 1, direita.Coluna] = true;
                    }
                }*/
            }
            else
            {
                pos.DefinirValores(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna);
                if (Tab.PosicaoValida(pos) && livre(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(PosicaoPeca.Linha + 2, PosicaoPeca.Coluna);
                Posicao p2 = new Posicao(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna);
                if (Tab.PosicaoValida(p2) && livre(p2) && Tab.PosicaoValida(pos) && livre(pos) && QuantidadeMovimentos == 0)
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna - 1);
                if (Tab.PosicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna + 1);
                if (Tab.PosicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                /*
                // #jogadaespecial en passant
                if (PosicaoPeca.Linha == 4)
                {
                    Posicao esquerda = new Posicao(PosicaoPeca.Linha, PosicaoPeca.Coluna - 1);
                    if (Tab.PosicaoValida(esquerda) && existeInimigo(esquerda) && Tab.PecaNaPosicao(esquerda) == partida.vulneravelEnPassant)
                    {
                        mat[esquerda.Linha + 1, esquerda.Coluna] = true;
                    }
                    Posicao direita = new Posicao(PosicaoPeca.Linha, PosicaoPeca.Coluna + 1);
                    if (Tab.PosicaoValida(direita) && existeInimigo(direita) && Tab.PecaNaPosicao(direita) == partida.vulneravelEnPassant)
                    {
                        mat[direita.Linha + 1, direita.Coluna] = true;
                    }
                }*/
            }

            return mat;
        }
    }
}
