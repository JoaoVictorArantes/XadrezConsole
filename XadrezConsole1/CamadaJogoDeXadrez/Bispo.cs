using CamadaTabuleiro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XadrezConsole1.CamadaJogoDeXadrez
{
    internal class Bispo : Peca
    {
        public Bispo(Tabuleiro tab, CorPeca cor) : base(tab, cor)
        {

        }
        public override string ToString()
        {
            return "B";
        }
    }
}
