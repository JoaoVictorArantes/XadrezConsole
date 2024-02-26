using System;


namespace CamadaTabuleiro
{
    class Tabuleiro
    {
        public int Linhas { get; set; }
        public int Colunas { get; set; }
        private Peca[,] MatrizPecas { get; set; }

        public Tabuleiro(int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;
            MatrizPecas = new Peca[linhas, colunas];
        }
        public Peca PecaNaPosicao(int linha, int coluna)
        {
            return MatrizPecas[linha, coluna];
        }
        public Peca PecaNaPosicao(Posicao pos)
        {
            //Console.WriteLine(pos);
            return MatrizPecas[pos.Linha, pos.Coluna];
        }

        public bool ExistePeca(Posicao pos)
        {
            ValidarPosicao(pos);
            return PecaNaPosicao(pos) != null;
        }

        public void ColocarPecaNaPosicao(Peca p, Posicao Pos)
        {
            if (ExistePeca(Pos))
            {
                throw new TabuleiroException("Já existe uma peça nessa posição! :(");
            }

            MatrizPecas[Pos.Linha, Pos.Coluna] = p;
            p.PosicaoPeca = Pos;
        }

        public Peca RetirarPecaDaPosicao(Posicao pos)
        {
            if (PecaNaPosicao(pos) == null)//se n tem nada, retorna nada 
            {
                return null;
            }
            //caso contrario pega a peça na posiçao e transforma em nula
            Peca aux = PecaNaPosicao(pos);
            aux.PosicaoPeca = null;
            MatrizPecas[pos.Linha, pos.Coluna] = null;
            return aux;
        }

        public bool PosicaoValida(Posicao pos)//verifica se ta dentro do tabuleiro
        {
            if (pos.Linha < 0 || pos.Linha >= Linhas || pos.Coluna < 0 || pos.Coluna >= Colunas)
            {
                return false;
            }
            return true;
        }

        public void ValidarPosicao(Posicao Pos)
        {
            if (!PosicaoValida(Pos))
            {
                throw new TabuleiroException("Posicao Inválida! :(");
            }
        }
    }
}
