using CamadaTabuleiro;
using System;


namespace CamadaJogoDeXadrez
{
    internal class Rei : Peca
    {
        public Rei(Tabuleiro tab, CorPeca cor): base(tab, cor)
        {

        }
        public override string ToString()
        {
            return "R";
        }
    }
}
