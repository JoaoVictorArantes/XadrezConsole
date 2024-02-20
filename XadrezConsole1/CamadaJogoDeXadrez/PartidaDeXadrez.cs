using System.Collections.Generic;
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
        private HashSet<Peca> ConjuntoDepecasEmJogo;
        private HashSet<Peca> ConjuntoDePecasCapturadas;

        public PartidaDeXadrez()
        {
            this.Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = CorPeca.Branca;
            PartidaTerminada = false;
            ConjuntoDepecasEmJogo = new HashSet<Peca>();
            ConjuntoDePecasCapturadas = new HashSet<Peca>();

            ColocarPecas();
        }

        public void ExecutaMovimento(Posicao PosicaoDeOrigem, Posicao PosicaoDeDestino)//retira a peça do lugar de origem, incrementa os movimentos,
        {
            Peca p = Tab.RetirarPecaDaPosicao(PosicaoDeOrigem);
            p.IncrementarQuantidadeDeMovimentos();
            Peca PecaCapturada = Tab.RetirarPecaDaPosicao(PosicaoDeDestino);
            Tab.ColocarPecaNaPosicao(p, PosicaoDeDestino);

            if (PecaCapturada != null)//se eu capturei uma peça ela vai ser inserida no conjunto das peças capturadas
            {
                ConjuntoDePecasCapturadas.Add(PecaCapturada);
            }
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
        public void ValidarPosicaoDeOrigem(Posicao posicao)// confere se tem uma peça, se a peça é da cor do jogador da rodada e se há movimentos possiveis.
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
        public HashSet<Peca> PecasCapturadas(CorPeca cor)//peças capturadas de acordo com a cor.
        {
            HashSet<Peca> ConjuntoAuxiliar = new HashSet<Peca>();//crio um conjunto do tipo 'Peça'

            foreach (Peca pecas in ConjuntoDePecasCapturadas)// para cada objeto do tipo Peça no ConjuntoDePecasCapturadas
            {
                if (pecas.Cor == cor)// comparo se a cor da peça no ConjuntoDePecasCapturadas é a mesma informada como parametro
                {
                    ConjuntoAuxiliar.Add(pecas);
                }
            }
            return ConjuntoAuxiliar;
        }
        public HashSet<Peca> PecasEmJogo(CorPeca cor)//peças em jogo de acordo com a cor.
        {
            HashSet<Peca> ConjuntoAuxiliar = new HashSet<Peca>();//crio um conjunto do tipo 'Peça'

            foreach (Peca pecas in ConjuntoDepecasEmJogo)// para cada objeto do tipo Peça no ConjuntoDepecasEmJogo
            {
                if (pecas.Cor == cor)// comparo se a cor da peça no ConjuntoDepecasEmJogo é a mesma informada como parametro
                {
                    ConjuntoAuxiliar.Add(pecas);
                }
            }
            ConjuntoAuxiliar.ExceptWith(PecasCapturadas(cor));//exceto as capturadas

            return ConjuntoAuxiliar;
        }


        public void ColocarNovaPeca(char coluna, int linha, Peca peca)//dada uma posicao, coloca peça lá.
        {
            Tab.ColocarPecaNaPosicao(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            ConjuntoDepecasEmJogo.Add(peca);//adiciona as peças ao conjunto da partida
        }

        private void ColocarPecas()
        {
            ColocarNovaPeca('a', 1, new Torre(Tab, CorPeca.Branca));
            ColocarNovaPeca('b', 1, new Cavalo(Tab, CorPeca.Branca));
            ColocarNovaPeca('c', 1, new Bispo(Tab, CorPeca.Branca));
            ColocarNovaPeca('d', 1, new Rei(Tab, CorPeca.Branca));
            ColocarNovaPeca('e', 1, new Dama(Tab, CorPeca.Branca));
            ColocarNovaPeca('f', 1, new Bispo(Tab, CorPeca.Branca));
            ColocarNovaPeca('g', 1, new Cavalo(Tab, CorPeca.Branca));
            ColocarNovaPeca('h', 1, new Torre(Tab, CorPeca.Branca));

            ColocarNovaPeca('a', 2, new Peao(Tab, CorPeca.Branca));
            ColocarNovaPeca('b', 2, new Peao(Tab, CorPeca.Branca));
            ColocarNovaPeca('c', 2, new Peao(Tab, CorPeca.Branca));
            ColocarNovaPeca('d', 2, new Peao(Tab, CorPeca.Branca));
            ColocarNovaPeca('e', 2, new Peao(Tab, CorPeca.Branca));
            ColocarNovaPeca('f', 2, new Peao(Tab, CorPeca.Branca));
            ColocarNovaPeca('g', 2, new Peao(Tab, CorPeca.Branca));
            ColocarNovaPeca('h', 2, new Peao(Tab, CorPeca.Branca));

            ColocarNovaPeca('a', 8, new Torre(Tab, CorPeca.Preta));
            ColocarNovaPeca('b', 8, new Cavalo(Tab, CorPeca.Preta));
            ColocarNovaPeca('c', 8, new Bispo(Tab, CorPeca.Preta));
            ColocarNovaPeca('d', 8, new Rei(Tab, CorPeca.Preta));
            ColocarNovaPeca('e', 8, new Dama(Tab, CorPeca.Preta));
            ColocarNovaPeca('f', 8, new Bispo(Tab, CorPeca.Preta));
            ColocarNovaPeca('g', 8, new Cavalo(Tab, CorPeca.Preta));
            ColocarNovaPeca('h', 8, new Torre(Tab, CorPeca.Preta));

            ColocarNovaPeca('a', 7, new Peao(Tab, CorPeca.Preta));
            ColocarNovaPeca('b', 7, new Peao(Tab, CorPeca.Preta));
            ColocarNovaPeca('c', 7, new Peao(Tab, CorPeca.Preta));
            ColocarNovaPeca('d', 7, new Peao(Tab, CorPeca.Preta));
            ColocarNovaPeca('e', 7, new Peao(Tab, CorPeca.Preta));
            ColocarNovaPeca('f', 7, new Peao(Tab, CorPeca.Preta));
            ColocarNovaPeca('g', 7, new Peao(Tab, CorPeca.Preta));
            ColocarNovaPeca('h', 7, new Peao(Tab, CorPeca.Preta));
        }
    }
}
