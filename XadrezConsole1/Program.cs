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
                PartidaDeXadrez Partida = new PartidaDeXadrez();

                while (!Partida.PartidaTerminada)
                {
                    Console.Clear();
                    TelaJogo.ImprimirTabuleiro(Partida.Tab);

                    Console.Write("Origem: ");
                    Posicao OrigemDoMovimento = TelaJogo.LerPosicaoXadrez().toPosicao();

                    bool[,] PosicaoPossiveis = Partida.Tab.PecaNaPosicao(OrigemDoMovimento).MovimentosPossiveis();// criei uma matriz pegando o tabuleiro da partida, peça na posicao e verifiquei os movimentos possiveis dela 

                    Console.Clear();// limpa a tela para mostrar os movimentos possiveis
                    TelaJogo.ImprimirTabuleiro(Partida.Tab, PosicaoPossiveis);

                    Console.WriteLine();
                    Console.Write("Destino: ");
                    Posicao DestinoDoMovimento = TelaJogo.LerPosicaoXadrez().toPosicao();

                    Partida.ExecutaMovimento(OrigemDoMovimento, DestinoDoMovimento);

                }

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }
    }
}