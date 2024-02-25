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
            ConjuntoDePecasEmJogo = new HashSet<Peca>();
            ConjuntoDePecasCapturadas = new HashSet<Peca>();
            SeEstouEmXeque = false;
            PecaVulneravelEnPassant = null;

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
            // /*
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
            if (PecaAux is Peao && (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha +2))
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
            ColocarNovaPeca('d', 1, new Rei(Tab, CorPeca.Branca, this));
            ColocarNovaPeca('e', 1, new Dama(Tab, CorPeca.Branca));
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
            ColocarNovaPeca('d', 8, new Rei(Tab, CorPeca.Preta, this));
            ColocarNovaPeca('e', 8, new Dama(Tab, CorPeca.Preta));
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
        
        /*
        public Tabuleiro tab { get; private set; }
        public int turno { get; private set; }
        public CorPeca jogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;
        public bool xeque { get; private set; }
        public Peca vulneravelEnPassant { get; private set; }

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = CorPeca.Branca;
            terminada = false;
            xeque = false;
            vulneravelEnPassant = null;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();
        }

        public Peca executaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.RetirarPecaDaPosicao(origem);
            p.IncrementarQuantidadeDeMovimentos();
            Peca pecaCapturada = tab.RetirarPecaDaPosicao(destino);
            tab.ColocarPecaNaPosicao(p, destino);
            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }

            // #jogadaespecial roque pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = tab.RetirarPecaDaPosicao(origemT);
                T.incrementarQteMovimentos();
                tab.colocarPeca(T, destinoT);
            }

            // #jogadaespecial roque grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.linha, origem.Coluna - 1);
                Peca T = tab.RetirarPecaDaPosicao(origemT);
                T.incrementarQteMovimentos();
                tab.colocarPeca(T, destinoT);
            }

            // #jogadaespecial en passant
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == null)
                {
                    Posicao posP;
                    if (p.cor == Cor.Branca)
                    {
                        posP = new Posicao(destino.linha + 1, destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(destino.linha - 1, destino.Coluna);
                    }
                    pecaCapturada = tab.RetirarPecaDaPosicao(posP);
                    capturadas.Add(pecaCapturada);
                }
            }

            return pecaCapturada;
        }

        public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = tab.RetirarPecaDaPosicao(destino);
            p.decrementarQteMovimentos();
            if (pecaCapturada != null)
            {
                tab.colocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }
            tab.colocarPeca(p, origem);

            // #jogadaespecial roque pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.linha, origem.Coluna + 1);
                Peca T = tab.RetirarPecaDaPosicao(destinoT);
                T.decrementarQteMovimentos();
                tab.colocarPeca(T, origemT);
            }

            // #jogadaespecial roque grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.linha, origem.Coluna - 1);
                Peca T = tab.RetirarPecaDaPosicao(destinoT);
                T.decrementarQteMovimentos();
                tab.colocarPeca(T, origemT);
            }

            // #jogadaespecial en passant
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == vulneravelEnPassant)
                {
                    Peca peao = tab.RetirarPecaDaPosicao(destino);
                    Posicao posP;
                    if (p.cor == Cor.Branca)
                    {
                        posP = new Posicao(3, destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(4, destino.Coluna);
                    }
                    tab.colocarPeca(peao, posP);
                }
            }
        }

        public void realizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = executaMovimento(origem, destino);

            if (estaEmXeque(jogadorAtual))
            {
                desfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            Peca p = tab.peca(destino);

            // #jogadaespecial promocao
            if (p is Peao)
            {
                if ((p.cor == Cor.Branca && destino.linha == 0) || (p.cor == Cor.Preta && destino.linha == 7))
                {
                    p = tab.RetirarPecaDaPosicao(destino);
                    pecas.Remove(p);
                    Peca dama = new Dama(tab, p.cor);
                    tab.colocarPeca(dama, destino);
                    pecas.Add(dama);
                }
            }

            if (estaEmXeque(adversaria(jogadorAtual)))
            {
                xeque = true;
            }
            else
            {
                xeque = false;
            }

            if (testeXequemate(adversaria(jogadorAtual)))
            {
                terminada = true;
            }
            else
            {
                turno++;
                mudaJogador();
            }

            // #jogadaespecial en passant
            if (p is Peao && (destino.linha == origem.linha - 2 || destino.linha == origem.linha + 2))
            {
                vulneravelEnPassant = p;
            }
            else
            {
                vulneravelEnPassant = null;
            }

        }

        public void validarPosicaoDeOrigem(Posicao pos)
        {
            if (tab.peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            if (jogadorAtual != tab.peca(pos).cor)
            {
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }
            if (!tab.peca(pos).existeMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        public void validarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!tab.peca(origem).movimentoPossivel(destino))
            {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }

        private void mudaJogador()
        {
            if (jogadorAtual == Cor.Branca)
            {
                jogadorAtual = Cor.Preta;
            }
            else
            {
                jogadorAtual = Cor.Branca;
            }
        }

        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in capturadas)
            {
                if (x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas)
            {
                if (x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }

        private Cor adversaria(Cor cor)
        {
            if (cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }

        private Peca rei(Cor cor)
        {
            foreach (Peca x in pecasEmJogo(cor))
            {
                if (x is Rei)
                {
                    return x;
                }
            }
            return null;
        }

        public bool SeEstaEmXeque(CorPeca cor)
        {
            Peca R = rei(cor);
            if (R == null)
            {
                throw new TabuleiroException("Não tem rei da cor " + cor + " no tabuleiro!");
            }
            foreach (Peca x in pecasEmJogo(adversaria(Cor)))
            {
                bool[,] mat = x.MovimentosPossiveis();
                if (mat[R.pos.Linha, R.posicao.Coluna])
                {
                    return true;
                }
            }
            return false;
        }

        public bool testeXequemate(Cor cor)
        {
            if (!estaEmXeque(cor))
            {
                return false;
            }
            foreach (Peca x in pecasEmJogo(cor))
            {
                bool[,] mat = x.movimentosPossiveis();
                for (int i = 0; i < tab.linhas; i++)
                {
                    for (int j = 0; j < tab.colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = x.posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = executaMovimento(origem, destino);
                            bool testeXeque = estaEmXeque(cor);
                            desfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }

        private void colocarPecas()
        {
            colocarNovaPeca('a', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('b', 1, new Cavalo(tab, Cor.Branca));
            colocarNovaPeca('c', 1, new Bispo(tab, Cor.Branca));
            colocarNovaPeca('d', 1, new Dama(tab, Cor.Branca));
            colocarNovaPeca('e', 1, new Rei(tab, Cor.Branca, this));
            colocarNovaPeca('f', 1, new Bispo(tab, Cor.Branca));
            colocarNovaPeca('g', 1, new Cavalo(tab, Cor.Branca));
            colocarNovaPeca('h', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('a', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('b', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('c', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('d', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('e', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('f', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('g', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('h', 2, new Peao(tab, Cor.Branca, this));

            colocarNovaPeca('a', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('b', 8, new Cavalo(tab, Cor.Preta));
            colocarNovaPeca('c', 8, new Bispo(tab, Cor.Preta));
            colocarNovaPeca('d', 8, new Dama(tab, Cor.Preta));
            colocarNovaPeca('e', 8, new Rei(tab, Cor.Preta, this));
            colocarNovaPeca('f', 8, new Bispo(tab, Cor.Preta));
            colocarNovaPeca('g', 8, new Cavalo(tab, Cor.Preta));
            colocarNovaPeca('h', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('a', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('b', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('c', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('d', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('e', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('f', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('g', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('h', 7, new Peao(tab, Cor.Preta, this));
        }*/



    }
}
