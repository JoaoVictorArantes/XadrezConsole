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
                    try
                    {
                        Console.Clear();
                        TelaJogo.ImprimirPartida(Partida);

                        Console.Write("\nOrigem: ");
                        Posicao OrigemDoMovimento = TelaJogo.LerPosicaoXadrez().toPosicao();

                        Partida.ValidarPosicaoDeOrigem(OrigemDoMovimento);

                        bool[,] PosicaoPossiveis = Partida.Tab.PecaNaPosicao(OrigemDoMovimento).MovimentosPossiveis();// criei uma matriz pegando o tabuleiro da partida, peça na posicao e verifiquei os movimentos possiveis dela 

                        Console.Clear();// limpa a tela para mostrar os movimentos possiveis
                        TelaJogo.ImprimirTabuleiro(Partida.Tab, PosicaoPossiveis);

                        Console.WriteLine();
                        Console.Write("\nDestino: ");
                        Posicao DestinoDoMovimento = TelaJogo.LerPosicaoXadrez().toPosicao();
                        Partida.ValidarPosicaoDeDestino(OrigemDoMovimento, DestinoDoMovimento);


                        Partida.RealizaJogada(OrigemDoMovimento, DestinoDoMovimento);
                    }

                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.ReadKey();
                    }
                }
                Console.Clear();
                TelaJogo.ImprimirPartida(Partida);
            }

            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }
    }
}