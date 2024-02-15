using CamadaTabuleiro;
using System;


namespace CamadaJogoDeXadrez
{
    internal class Torre : Peca
    {

        public Torre(Tabuleiro tab, CorPeca cor) : base(tab, cor)
        {

        }
        public override string ToString()
        {
            return "T";
        }


    }
}
