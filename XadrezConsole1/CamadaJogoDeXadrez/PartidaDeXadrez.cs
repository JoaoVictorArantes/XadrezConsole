using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
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
        private HashSet<Peca> ConjuntoDePecasEmJogo;
        private HashSet<Peca> ConjuntoDePecasCapturadas;
        public bool SeEstouEmXeque { get; private set; }
        public Peca PecaVulneravelEnPassant { get; private set; }

        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = CorPeca.Branca;
            PartidaTerminada = false;
            SeEstouEmXeque = false;
            PecaVulneravelEnPassant = null;
            ConjuntoDePecasEmJogo = new HashSet<Peca>();
            ConjuntoDePecasCapturadas = new HashSet<Peca>();            
            
            ColocarPecas();
        }

        public bool TesteXequeMate(CorPeca cor)
        {
            if (!EstaEmXeque(cor))
            {
                return false;
            }
            foreach (Peca peca in PecasEmJogo(cor))
            {
                bool[,] MatAux = peca.MovimentosPossiveis();

                for (int i = 0; i < Tab.Linhas; i++)
                {
                    for (int j = 0; j < Tab.Colunas; j++)
                    {
                        if (MatAux[i, j])
                        {
                            Posicao Origem = peca.PosicaoPeca;

                            Posicao Destino = new Posicao(i, j);

                            Peca PecaCapturada = ExecutaMovimento(Origem, Destino);

                            bool TesteXeque = EstaEmXeque(cor);

                            DesfazerMovimento(Origem, Destino, PecaCapturada);

                            if (!TesteXeque)
                            {
                                return false;// NOT XEQUE-MATE
                            }
                        }
                    }
                }
            }
            return true;//XEQUE-MATE
        }

        public Peca ExecutaMovimento(Posicao PosicaoDeOrigem, Posicao PosicaoDeDestino)//retira a peça do lugar de origem, incrementa os movimentos e registra a captura
        {
            Peca peca = Tab.RetirarPecaDaPosicao(PosicaoDeOrigem);
            
            peca.IncrementarQuantidadeDeMovimentos();
            
            Peca PecaCapturada = Tab.RetirarPecaDaPosicao(PosicaoDeDestino);
            
            Tab.ColocarPecaNaPosicao(peca, PosicaoDeDestino);

            if (PecaCapturada != null)//se eu capturei uma peça ela vai ser inserida no conjunto das peças capturadas
            {
                ConjuntoDePecasCapturadas.Add(PecaCapturada);
            }

            //Roque pequeno
            if (peca is Rei && PosicaoDeDestino.Coluna == PosicaoDeOrigem.Coluna + 2)
            {
                Posicao OrigemTorre = new Posicao(PosicaoDeOrigem.Linha, PosicaoDeOrigem.Coluna + 3);
                Posicao DestinoTorre = new Posicao(PosicaoDeOrigem.Linha, PosicaoDeOrigem.Coluna + 1);

                Peca Torre = Tab.RetirarPecaDaPosicao(OrigemTorre);

                Torre.IncrementarQuantidadeDeMovimentos();

                Tab.ColocarPecaNaPosicao(Torre, DestinoTorre);            
            }

            //Roque grande
            if (peca is Rei && PosicaoDeDestino.Coluna == PosicaoDeOrigem.Coluna - 2)
            {
                Posicao OrigemTorre = new Posicao(PosicaoDeOrigem.Linha, PosicaoDeOrigem.Coluna - 4);
                Posicao DestinoTorre = new Posicao(PosicaoDeOrigem.Linha, PosicaoDeOrigem.Coluna - 1);

                Peca Torre = Tab.RetirarPecaDaPosicao(OrigemTorre);

                Torre.IncrementarQuantidadeDeMovimentos();

                Tab.ColocarPecaNaPosicao(Torre, DestinoTorre);
            }

            if (peca is Peao)// en passant
            {
                if (PosicaoDeOrigem.Coluna != PosicaoDeDestino.Coluna && PecaCapturada == null)
                {
                    Posicao PosicaoPeao;

                    if (peca.Cor == CorPeca.Branca)
                    {
                        PosicaoPeao = new Posicao(PosicaoDeDestino.Linha + 1, PosicaoDeDestino.Coluna);
                    }
                    else
                    {
                        PosicaoPeao = new Posicao(PosicaoDeDestino.Linha - 1, PosicaoDeDestino.Coluna);
                    }
                    PecaCapturada = Tab.RetirarPecaDaPosicao(PosicaoPeao);

                    ConjuntoDePecasCapturadas.Add(PecaCapturada);
                }
            }
            return PecaCapturada;
        }
        public void DesfazerMovimento(Posicao Origem, Posicao Destino, Peca PecaCapturada)
        {
            Peca peca = Tab.RetirarPecaDaPosicao(Destino);
            
            peca.DecrementarQuantidadeDeMovimentos();

            if (PecaCapturada != null)//se teve peca capturada
            {
                Tab.ColocarPecaNaPosicao(PecaCapturada, Destino);

                ConjuntoDePecasCapturadas.Remove(PecaCapturada);
            }
            Tab.ColocarPecaNaPosicao(peca, Origem); // Desfez o movimento, voltando a peça para a origem dela.

            //Roque Pequeno
            if (peca is Rei && Destino.Coluna == Origem.Coluna + 2)// testo se é um rei e se mecheu 2 para a direita
            {
                Posicao OrigemTorre = new Posicao(Origem.Linha, Origem.Coluna + 3);
                Posicao DestinoTorre = new Posicao(Origem.Linha, Origem.Coluna + 1);

                Peca Torre = Tab.RetirarPecaDaPosicao(DestinoTorre);//tiro o rei do destino

                Torre.DecrementarQuantidadeDeMovimentos();

                Tab.ColocarPecaNaPosicao(Torre, OrigemTorre);// volto o rei para a origem
            }
            
            //Roque Grande
            if (peca is Rei && Destino.Coluna == Origem.Coluna - 2)
            {
                Posicao OrigemTorre = new Posicao(Origem.Linha, Origem.Coluna - 4);
                Posicao DestinoTorre = new Posicao(Origem.Linha, Origem.Coluna - 1);

                Peca Torre = Tab.RetirarPecaDaPosicao(DestinoTorre);

                Torre.DecrementarQuantidadeDeMovimentos();

                Tab.ColocarPecaNaPosicao(Torre, OrigemTorre);
            }

            // En Passant
            if (peca is Peao)
            {
                if (Origem.Coluna != Destino.Coluna && PecaCapturada == PecaVulneravelEnPassant)
                {
                    Peca peao = Tab.RetirarPecaDaPosicao(Destino);
                    
                    Posicao posP;
                    
                    if (peca.Cor == CorPeca.Branca)
                    {
                        posP = new Posicao(3, Destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(4, Destino.Coluna);
                    }
                    Tab.ColocarPecaNaPosicao(peao, posP);
                }
            }
        }


        public void RealizaJogada(Posicao origem, Posicao destino) //Executa um movimento, incrementa o turno e troca o jogador da vez
        {
            Peca PecaCapturada = ExecutaMovimento(origem, destino);

            if (EstaEmXeque(JogadorAtual))
            {
                DesfazerMovimento(origem, destino, PecaCapturada);

                throw new TabuleiroException("Voce não pode se colocar em Xeque!!! :(");
            }

            Peca PecaAux = Tab.PecaNaPosicao(destino);

            // Promoção do peao            
            if (PecaAux is Peao)
            {
                if ((PecaAux.Cor == CorPeca.Branca && destino.Linha == 0) || (PecaAux.Cor == CorPeca.Preta && destino.Linha == 7))
                {//ou um branco chega na linha 0 ou um preto que chega na linha 7
                    PecaAux = Tab.RetirarPecaDaPosicao(destino);

                    ConjuntoDePecasEmJogo.Remove(PecaAux);
                    
                    Peca Dama = new Dama(Tab, PecaAux.Cor);

                    Tab.ColocarPecaNaPosicao(Dama, destino);

                    ConjuntoDePecasEmJogo.Add(Dama);                    
                }
            }

            if (EstaEmXeque(Adversaria(JogadorAtual)))//Se meu oponente esta em xeque, deixa realizar a jogada
            {
                SeEstouEmXeque = true;
            }
            else
            {
                SeEstouEmXeque = false;
            }

            if (TesteXequeMate(Adversaria(JogadorAtual)))// realizei a jogada e meu adversario esta em xeque-mate
            {
                PartidaTerminada = true;// Fim de jogo
            }
            else
            {
                Turno++;
                MudaJogador();
            }

            

            //EN PASSANT
            if (PecaAux is Peao && (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2))
            {//se essa peça movida é um peao e andou 2 linhas a mais ou a menos
                PecaVulneravelEnPassant = PecaAux;
            }
            else
            {
                PecaVulneravelEnPassant = null;
            }
        }

        private void MudaJogador()//Troca a vez de jogar
        {
            if (JogadorAtual == CorPeca.Branca)
            {
                JogadorAtual = CorPeca.Preta;
            }
            else
            {
                JogadorAtual = CorPeca.Branca;
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
            if (!Tab.PecaNaPosicao(origem).MovimentoPossivel(destino))
            {
                throw new TabuleiroException("Posiçao de destino inválida! ");
            }
        }

        private CorPeca Adversaria(CorPeca cor)//Diz quem é o adversário
        {
            if (cor == CorPeca.Branca)
            {
                return CorPeca.Preta;
            }
            else
            {
                return CorPeca.Branca;
            }
        }

        public bool EstaEmXeque(CorPeca cor)//verifica se alguma peça adversária tem movimentos possiveis em direçao ao rei(xeque)
        {
            Peca MeuRei = Rei(cor);

            if (MeuRei == null)// verifica se tem rei em jogo (deve ter)
            {
                throw new TabuleiroException($"Não tem rei da cor {cor} no tabuleiro!!! :(");
            }
            foreach (Peca peca in PecasEmJogo(Adversaria(cor)))
            {
                bool[,] MatAux = peca.MovimentosPossiveis();
                if (MatAux[MeuRei.PosicaoPeca.Linha, MeuRei.PosicaoPeca.Coluna])
                {
                    return true;
                }
            }
            return false;
        }

        private Peca Rei(CorPeca cor)//verifica se é o rei (Faz parte da logica do 'xeck')
        {
            foreach (Peca peca in PecasEmJogo(cor))
            {
                if (peca is Rei)
                {
                    return peca;
                }
            }
            return null;
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

            foreach (Peca pecas in ConjuntoDePecasEmJogo)// para cada objeto do tipo Peça no ConjuntoDepecasEmJogo
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
            ConjuntoDePecasEmJogo.Add(peca);//adiciona as peças ao conjunto da partida
        }

        private void ColocarPecas()
        {
            ColocarNovaPeca('a', 1, new Torre(Tab, CorPeca.Branca));
            ColocarNovaPeca('b', 1, new Cavalo(Tab, CorPeca.Branca));
            ColocarNovaPeca('c', 1, new Bispo(Tab, CorPeca.Branca));            
            ColocarNovaPeca('d', 1, new Dama(Tab, CorPeca.Branca));
            ColocarNovaPeca('e', 1, new Rei(Tab, CorPeca.Branca, this));
            ColocarNovaPeca('f', 1, new Bispo(Tab, CorPeca.Branca));
            ColocarNovaPeca('g', 1, new Cavalo(Tab, CorPeca.Branca));
            ColocarNovaPeca('h', 1, new Torre(Tab, CorPeca.Branca));

            ColocarNovaPeca('a', 2, new Peao(Tab, CorPeca.Branca, this));
            ColocarNovaPeca('b', 2, new Peao(Tab, CorPeca.Branca, this));
            ColocarNovaPeca('c', 2, new Peao(Tab, CorPeca.Branca, this));
            ColocarNovaPeca('d', 2, new Peao(Tab, CorPeca.Branca, this));
            ColocarNovaPeca('e', 2, new Peao(Tab, CorPeca.Branca, this));
            ColocarNovaPeca('f', 2, new Peao(Tab, CorPeca.Branca, this));
            ColocarNovaPeca('g', 2, new Peao(Tab, CorPeca.Branca, this));
            ColocarNovaPeca('h', 2, new Peao(Tab, CorPeca.Branca, this));

            ColocarNovaPeca('a', 8, new Torre(Tab, CorPeca.Preta));
            ColocarNovaPeca('b', 8, new Cavalo(Tab, CorPeca.Preta));
            ColocarNovaPeca('c', 8, new Bispo(Tab, CorPeca.Preta));
            ColocarNovaPeca('d', 8, new Dama(Tab, CorPeca.Preta));
            ColocarNovaPeca('e', 8, new Rei(Tab, CorPeca.Preta, this));
            ColocarNovaPeca('f', 8, new Bispo(Tab, CorPeca.Preta));
            ColocarNovaPeca('g', 8, new Cavalo(Tab, CorPeca.Preta));
            ColocarNovaPeca('h', 8, new Torre(Tab, CorPeca.Preta));

            ColocarNovaPeca('a', 7, new Peao(Tab, CorPeca.Preta, this));
            ColocarNovaPeca('b', 7, new Peao(Tab, CorPeca.Preta, this));
            ColocarNovaPeca('c', 7, new Peao(Tab, CorPeca.Preta, this));
            ColocarNovaPeca('d', 7, new Peao(Tab, CorPeca.Preta, this));
            ColocarNovaPeca('e', 7, new Peao(Tab, CorPeca.Preta, this));
            ColocarNovaPeca('f', 7, new Peao(Tab, CorPeca.Preta, this));
            ColocarNovaPeca('g', 7, new Peao(Tab, CorPeca.Preta, this));
            ColocarNovaPeca('h', 7, new Peao(Tab, CorPeca.Preta, this));
        }
    }
}
