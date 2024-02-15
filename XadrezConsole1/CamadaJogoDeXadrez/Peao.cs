using CamadaTabuleiro;
using System;


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
    }
}
