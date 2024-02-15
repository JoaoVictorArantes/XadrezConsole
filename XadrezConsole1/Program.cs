using CamadaTabuleiro;
using CamadaJogoDeXadrez;

namespace XadrezConsole1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Tabuleiro Tab = new Tabuleiro(8, 8);

                Tab.ColocarPecaNaPosicao(new Torre(Tab, CorPeca.Preta), new Posicao(0, 0));
                Tab.ColocarPecaNaPosicao(new Torre(Tab, CorPeca.Preta), new Posicao(1, 3));
                Tab.ColocarPecaNaPosicao(new Rei(Tab, CorPeca.Preta), new Posicao(2, 9));

                TelaJogo.ImprimirTabuleiro(Tab);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            Console.ReadKey();
        }
    }
}
