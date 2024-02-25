using CamadaTabuleiro;
using System;
using System.Runtime.ConstrainedExecution;


namespace CamadaJogoDeXadrez
{
    internal class Rei : Peca
    {
        /*
        private PartidaDeXadrez partidaDeXadrez;

        public Rei(Tabuleiro tab, CorPeca cor, PartidaDeXadrez partidaDeXadrez) : base(tab, cor)
        {
            this.partidaDeXadrez = partidaDeXadrez;
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

        private bool TesteTorreParaRoque(Posicao posicao)// Testa se a torre na posiçao é elegivel para roque pequeno
        {
            Peca peca = Tab.PecaNaPosicao(posicao);

            return peca != null && peca is Torre && peca.Cor == Cor && peca.QuantidadeMovimentos == 0;
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

            //JOGADA ESPECIAL ROQUE PEQUENO GRANDE
            if (QuantidadeMovimentos == 0 && !partidaDeXadrez.SeEstouEmXeque)
            {
                // Roque pequeno
                Posicao PosicaoTorre1 = new Posicao(PosicaoPeca.Linha, PosicaoPeca.Coluna + 3);

                if (TesteTorreParaRoque(PosicaoTorre1))
                {
                    Posicao Posicao1 = new Posicao(PosicaoPeca.Linha, PosicaoPeca.Coluna + 1);

                    Posicao Posicao2 = new Posicao(PosicaoPeca.Linha, PosicaoPeca.Coluna + 2);

                    if (Tab.PecaNaPosicao(Posicao1) == null && Tab.PecaNaPosicao(Posicao2) == null)
                    {
                        MatrixAux[PosicaoPeca.Linha, PosicaoPeca.Coluna + 2] = true;
                    }
                }
                // Roque Grande
                Posicao PosicaoTorre2 = new Posicao(PosicaoPeca.Linha, PosicaoPeca.Coluna - 4);

                if (TesteTorreParaRoque(PosicaoTorre2))
                {
                    Posicao Posicao1 = new Posicao(PosicaoPeca.Linha, PosicaoPeca.Coluna - 1);

                    Posicao Posicao2 = new Posicao(PosicaoPeca.Linha, PosicaoPeca.Coluna - 2);

                    Posicao Posicao3 = new Posicao(PosicaoPeca.Linha, PosicaoPeca.Coluna - 3);

                    if (Tab.PecaNaPosicao(Posicao1) == null && Tab.PecaNaPosicao(Posicao2) == null && Tab.PecaNaPosicao(Posicao3) == null)
                    {
                        MatrixAux[PosicaoPeca.Linha, PosicaoPeca.Coluna - 2] = true;
                    }
                }
            }
            return MatrixAux;
        }*/

        private PartidaDeXadrez partida;

        public Rei(Tabuleiro tab, CorPeca cor, PartidaDeXadrez partida) : base(tab, cor)
        {
            this.partida = partida;
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

        private bool testeTorreParaRoque(Posicao pos)
        {
            Peca p = Tab.PecaNaPosicao(pos);
            return p != null && p is Torre && p.Cor == Cor && p.QuantidadeMovimentos == 0;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];

            Posicao pos = new Posicao(0, 0);

            // acima
            pos.DefinirValores(pos.Linha - 1, pos.Coluna);
            if (Tab.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            // ne
            pos.DefinirValores(pos.Linha - 1, pos.Coluna + 1);
            if (Tab.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            // direita
            pos.DefinirValores(pos.Linha, pos.Coluna + 1);
            if (Tab.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            // se
            pos.DefinirValores(pos.Linha + 1, pos.Coluna + 1);
            if (Tab.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            // abaixo
            pos.DefinirValores(pos.Linha + 1, pos.Coluna);
            if (Tab.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            // so
            pos.DefinirValores(pos.Linha + 1, pos.Coluna - 1);
            if (Tab.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            // esquerda
            pos.DefinirValores(pos.Linha, pos.Coluna - 1);
            if (Tab.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            // no
            pos.DefinirValores(pos.Linha - 1, pos.Coluna - 1);
            if (Tab.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // #jogadaespecial roque
            if (QuantidadeMovimentos == 0 && !partida.SeEstouEmXeque)
            {
                // #jogadaespecial roque pequeno
                Posicao posT1 = new Posicao(pos.Linha, pos.Coluna + 3);
                if (testeTorreParaRoque(posT1))
                {
                    Posicao p1 = new Posicao(pos.Linha, pos.Coluna + 1);
                    Posicao p2 = new Posicao(pos.Linha, pos.Coluna + 2);
                    if (Tab.PecaNaPosicao(p1) == null && Tab.PecaNaPosicao(p2) == null)
                    {
                        mat[pos.Linha, pos.Coluna + 2] = true;
                    }
                }
                // #jogadaespecial roque grande
                Posicao posT2 = new Posicao(pos.Linha, pos.Coluna - 4);
                if (testeTorreParaRoque(posT2))
                {
                    Posicao p1 = new Posicao(pos.Linha, pos.Coluna - 1);
                    Posicao p2 = new Posicao(pos.Linha, pos.Coluna - 2);
                    Posicao p3 = new Posicao(pos.Linha, pos.Coluna - 3);
                    if (Tab.PecaNaPosicao(p1) == null && Tab.PecaNaPosicao(p2) == null && Tab.PecaNaPosicao(p3) == null)
                    {
                        mat[pos.Linha, pos.Coluna - 2] = true;
                    }
                }
            }
            return mat;
        }
    }
}
