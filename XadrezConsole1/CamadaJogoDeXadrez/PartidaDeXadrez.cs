using System;
using CamadaTabuleiro;
using XadrezConsole1.CamadaJogoDeXadrez;

namespace CamadaJogoDeXadrez
{
    internal class PartidaDeXadrez
    {
        public Tabuleiro Tab { get; private set; }
        private int Turno;
        private CorPeca JogadorAtual;
        public bool PartidaTerminada { get; private set; }

        public PartidaDeXadrez()
        {
            this.Tab = new Tabuleiro(8,8);
            Turno = 1;
            JogadorAtual = CorPeca.Branca;
            PartidaTerminada = false;
            ColocarPecas();
        }

        public void ExecutaMovimento(Posicao PosicaoDeOrigem, Posicao PosicaoDeDestino)
        {
            Peca p = Tab.RetirarPecaDaPosicao(PosicaoDeOrigem);
            p.IncrementarQuantidadeDeMovimentos();
            Peca PecaCapturada = Tab.RetirarPecaDaPosicao(PosicaoDeDestino);
            Tab.ColocarPecaNaPosicao(p, PosicaoDeDestino);


        }
        private void ColocarPecas()
        {
            Tab.ColocarPecaNaPosicao(new Torre(Tab, CorPeca.Branca), new PosicaoXadrez('a',8).toPosicao());
            Tab.ColocarPecaNaPosicao(new Cavalo(Tab, CorPeca.Branca), new PosicaoXadrez('b',8).toPosicao());
            Tab.ColocarPecaNaPosicao(new Bispo(Tab, CorPeca.Branca), new PosicaoXadrez('c', 8).toPosicao());
            Tab.ColocarPecaNaPosicao(new Rei(Tab, CorPeca.Branca), new PosicaoXadrez('d', 8).toPosicao());
            Tab.ColocarPecaNaPosicao(new Dama(Tab, CorPeca.Branca), new PosicaoXadrez('e', 8).toPosicao());
            Tab.ColocarPecaNaPosicao(new Bispo(Tab, CorPeca.Branca), new PosicaoXadrez('f', 8).toPosicao());
            Tab.ColocarPecaNaPosicao(new Cavalo(Tab, CorPeca.Branca), new PosicaoXadrez('g', 8).toPosicao());
            Tab.ColocarPecaNaPosicao(new Torre(Tab, CorPeca.Branca), new PosicaoXadrez('h', 8).toPosicao());

            Tab.ColocarPecaNaPosicao(new Peao(Tab, CorPeca.Branca), new PosicaoXadrez('a', 7).toPosicao());
            Tab.ColocarPecaNaPosicao(new Peao(Tab, CorPeca.Branca), new PosicaoXadrez('b', 7).toPosicao());
            Tab.ColocarPecaNaPosicao(new Peao(Tab, CorPeca.Branca), new PosicaoXadrez('c', 7).toPosicao());
            Tab.ColocarPecaNaPosicao(new Peao(Tab, CorPeca.Branca), new PosicaoXadrez('d', 7).toPosicao());
            Tab.ColocarPecaNaPosicao(new Peao(Tab, CorPeca.Branca), new PosicaoXadrez('e', 7).toPosicao());
            Tab.ColocarPecaNaPosicao(new Peao(Tab, CorPeca.Branca), new PosicaoXadrez('f', 7).toPosicao());
            Tab.ColocarPecaNaPosicao(new Peao(Tab, CorPeca.Branca), new PosicaoXadrez('g', 7).toPosicao());
            Tab.ColocarPecaNaPosicao(new Peao(Tab, CorPeca.Branca), new PosicaoXadrez('h', 7).toPosicao());

            Tab.ColocarPecaNaPosicao(new Torre(Tab, CorPeca.Preta), new PosicaoXadrez('a', 1).toPosicao());
            Tab.ColocarPecaNaPosicao(new Cavalo(Tab, CorPeca.Preta), new PosicaoXadrez('b', 1).toPosicao());
            Tab.ColocarPecaNaPosicao(new Bispo(Tab, CorPeca.Preta), new PosicaoXadrez('c', 1).toPosicao());
            Tab.ColocarPecaNaPosicao(new Rei(Tab, CorPeca.Preta), new PosicaoXadrez('d', 1).toPosicao());
            Tab.ColocarPecaNaPosicao(new Dama(Tab, CorPeca.Preta), new PosicaoXadrez('e', 1).toPosicao());
            Tab.ColocarPecaNaPosicao(new Bispo(Tab, CorPeca.Preta), new PosicaoXadrez('f', 1).toPosicao());
            Tab.ColocarPecaNaPosicao(new Cavalo(Tab, CorPeca.Preta), new PosicaoXadrez('g', 1).toPosicao());
            Tab.ColocarPecaNaPosicao(new Torre(Tab, CorPeca.Preta), new PosicaoXadrez('h', 1).toPosicao());

            Tab.ColocarPecaNaPosicao(new Peao(Tab, CorPeca.Preta), new PosicaoXadrez('a', 2).toPosicao());
            Tab.ColocarPecaNaPosicao(new Peao(Tab, CorPeca.Preta), new PosicaoXadrez('b', 2).toPosicao());
            Tab.ColocarPecaNaPosicao(new Peao(Tab, CorPeca.Preta), new PosicaoXadrez('c', 2).toPosicao());
            Tab.ColocarPecaNaPosicao(new Peao(Tab, CorPeca.Preta), new PosicaoXadrez('d', 2).toPosicao());
            Tab.ColocarPecaNaPosicao(new Peao(Tab, CorPeca.Preta), new PosicaoXadrez('e', 2).toPosicao());
            Tab.ColocarPecaNaPosicao(new Peao(Tab, CorPeca.Preta), new PosicaoXadrez('f', 2).toPosicao());
            Tab.ColocarPecaNaPosicao(new Peao(Tab, CorPeca.Preta), new PosicaoXadrez('g', 2).toPosicao());
            Tab.ColocarPecaNaPosicao(new Peao(Tab, CorPeca.Preta), new PosicaoXadrez('h', 2).toPosicao());

        }
    }
}
