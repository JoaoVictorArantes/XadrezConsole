using System;
using CamadaTabuleiro;
using XadrezConsole1.CamadaJogoDeXadrez;

namespace CamadaJogoDeXadrez
{
    internal class PartidaDeXadrez
    {
        public Tabuleiro Tab { get; private set; }
        public int Turno { get; private set; }
        public CorPeca JogadorAtual { get; private set; }
        public bool PartidaTerminada { get; private set; }

        public PartidaDeXadrez()
        {
            this.Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = CorPeca.Branca;
            PartidaTerminada = false;
            ColocarPecas();
        }

        public void ExecutaMovimento(Posicao PosicaoDeOrigem, Posicao PosicaoDeDestino)//retira a peça do lugar de origem, incrementa os movimentos,
        {
            Peca p = Tab.RetirarPecaDaPosicao(PosicaoDeOrigem);
            p.IncrementarQuantidadeDeMovimentos();
            Peca PecaCapturada = Tab.RetirarPecaDaPosicao(PosicaoDeDestino);
            Tab.ColocarPecaNaPosicao(p, PosicaoDeDestino);
        }

        public void RealizaJogada(Posicao origem, Posicao destino) //Executa um movimento, incrementa o turno e troca o jogador da vez
        {
            ExecutaMovimento(origem, destino);
            Turno++;
            MudaJogador();
        }
        private void MudaJogador()//Troca a vez de jogar
        {
            if (JogadorAtual == CorPeca.Preta)
            {
                JogadorAtual = CorPeca.Branca;
            }
            else
            {
                JogadorAtual = CorPeca.Preta;
            }

        }
        public void ValidarPsicaoDeOrigem(Posicao posicao)// confere se tem uma peça, se a peça é da cor do jogador da rodada e se há movimentos possiveis.
        {
            if (Tab.PecaNaPosicao(posicao) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            if (JogadorAtual != Tab.PecaNaPosicao(posicao).Cor)
            {
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }
            if (!Tab.PecaNaPosicao(posicao).ExisteMovimentosPossiveis())
            {
                throw new TabuleiroException("não há movimentos possíveis para a peça de origem escolhida!");
            }
        }
        public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!Tab.PecaNaPosicao(origem).PodeMoverPara(destino))
            {
                throw new TabuleiroException("Posiçao de destino inválida! ");
            }
        }


        private void ColocarPecas()
        {
            Tab.ColocarPecaNaPosicao(new Torre(Tab, CorPeca.Branca), new PosicaoXadrez('a', 1).toPosicao());
            Tab.ColocarPecaNaPosicao(new Cavalo(Tab, CorPeca.Branca), new PosicaoXadrez('b', 1).toPosicao());
            Tab.ColocarPecaNaPosicao(new Bispo(Tab, CorPeca.Branca), new PosicaoXadrez('c', 1).toPosicao());
            Tab.ColocarPecaNaPosicao(new Rei(Tab, CorPeca.Branca), new PosicaoXadrez('d', 1).toPosicao());
            Tab.ColocarPecaNaPosicao(new Dama(Tab, CorPeca.Branca), new PosicaoXadrez('e', 1).toPosicao());
            Tab.ColocarPecaNaPosicao(new Bispo(Tab, CorPeca.Branca), new PosicaoXadrez('f', 1).toPosicao());
            Tab.ColocarPecaNaPosicao(new Cavalo(Tab, CorPeca.Branca), new PosicaoXadrez('g', 1).toPosicao());
            Tab.ColocarPecaNaPosicao(new Torre(Tab, CorPeca.Branca), new PosicaoXadrez('h', 1).toPosicao());

            Tab.ColocarPecaNaPosicao(new Peao(Tab, CorPeca.Branca), new PosicaoXadrez('a', 2).toPosicao());
            Tab.ColocarPecaNaPosicao(new Peao(Tab, CorPeca.Branca), new PosicaoXadrez('b', 2).toPosicao());
            Tab.ColocarPecaNaPosicao(new Peao(Tab, CorPeca.Branca), new PosicaoXadrez('c', 2).toPosicao());
            Tab.ColocarPecaNaPosicao(new Peao(Tab, CorPeca.Branca), new PosicaoXadrez('d', 2).toPosicao());
            Tab.ColocarPecaNaPosicao(new Peao(Tab, CorPeca.Branca), new PosicaoXadrez('e', 2).toPosicao());
            Tab.ColocarPecaNaPosicao(new Peao(Tab, CorPeca.Branca), new PosicaoXadrez('f', 2).toPosicao());
            Tab.ColocarPecaNaPosicao(new Peao(Tab, CorPeca.Branca), new PosicaoXadrez('g', 2).toPosicao());
            Tab.ColocarPecaNaPosicao(new Peao(Tab, CorPeca.Branca), new PosicaoXadrez('h', 2).toPosicao());

            Tab.ColocarPecaNaPosicao(new Torre(Tab, CorPeca.Preta), new PosicaoXadrez('a', 8).toPosicao());
            Tab.ColocarPecaNaPosicao(new Cavalo(Tab, CorPeca.Preta), new PosicaoXadrez('b', 8).toPosicao());
            Tab.ColocarPecaNaPosicao(new Bispo(Tab, CorPeca.Preta), new PosicaoXadrez('c', 8).toPosicao());
            Tab.ColocarPecaNaPosicao(new Rei(Tab, CorPeca.Preta), new PosicaoXadrez('d', 8).toPosicao());
            Tab.ColocarPecaNaPosicao(new Dama(Tab, CorPeca.Preta), new PosicaoXadrez('e', 8).toPosicao());
            Tab.ColocarPecaNaPosicao(new Bispo(Tab, CorPeca.Preta), new PosicaoXadrez('f', 8).toPosicao());
            Tab.ColocarPecaNaPosicao(new Cavalo(Tab, CorPeca.Preta), new PosicaoXadrez('g', 8).toPosicao());
            Tab.ColocarPecaNaPosicao(new Torre(Tab, CorPeca.Preta), new PosicaoXadrez('h', 8).toPosicao());

            Tab.ColocarPecaNaPosicao(new Peao(Tab, CorPeca.Preta), new PosicaoXadrez('a', 7).toPosicao());
            Tab.ColocarPecaNaPosicao(new Peao(Tab, CorPeca.Preta), new PosicaoXadrez('b', 7).toPosicao());
            Tab.ColocarPecaNaPosicao(new Peao(Tab, CorPeca.Preta), new PosicaoXadrez('c', 7).toPosicao());
            Tab.ColocarPecaNaPosicao(new Peao(Tab, CorPeca.Preta), new PosicaoXadrez('d', 7).toPosicao());
            Tab.ColocarPecaNaPosicao(new Peao(Tab, CorPeca.Preta), new PosicaoXadrez('e', 7).toPosicao());
            Tab.ColocarPecaNaPosicao(new Peao(Tab, CorPeca.Preta), new PosicaoXadrez('f', 7).toPosicao());
            Tab.ColocarPecaNaPosicao(new Peao(Tab, CorPeca.Preta), new PosicaoXadrez('g', 7).toPosicao());
            Tab.ColocarPecaNaPosicao(new Peao(Tab, CorPeca.Preta), new PosicaoXadrez('h', 7).toPosicao());

        }
    }
}
