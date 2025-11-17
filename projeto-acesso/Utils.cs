using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_acesso
{
    internal class Utils
    {
        public static int lerInt(string entrada, int min, string msgErro)
        {
            bool converteu = int.TryParse(entrada, out int intValido);
            while (!converteu || intValido < min)
            {
                Console.Write(msgErro);
                converteu = int.TryParse(Console.ReadLine(), out intValido);
            }
            return intValido;
        }

        public static double lerDouble(string entrada, int min, string msgErro)
        {
            bool converteu = double.TryParse(entrada, out double doubleValido);
            while (!converteu || doubleValido < min)
            {
                Console.WriteLine(msgErro);
                converteu = double.TryParse(Console.ReadLine(), out doubleValido);
            }
            return doubleValido;
        }
        public static int lerMinMax(string entrada, int min, int max, string msgErro)
        {
            bool converteu = int.TryParse(entrada, out int intValido);
            while (!converteu || intValido < min || intValido > max)
            {
                Console.Write(msgErro);
                converteu = int.TryParse(Console.ReadLine(), out intValido);
            }
            return intValido;
        }

        public static void Titulo(string titulo)
        {
            Console.Clear();
            Console.WriteLine(new string('=', 70));
            Console.WriteLine($" ACESSO - {titulo}");
            Console.WriteLine(new string('=', 70));
        }

        public static void MensagemErro(string texto)
        {
            Console.WriteLine();
            Console.WriteLine($" {texto}");
            Console.WriteLine(" [Pressione qualquer tecla...]");
            Console.ReadKey();
        }

        public static void MensagemSucesso(string texto)
        {
            Console.WriteLine();
            Console.WriteLine($" {texto}");
            Console.WriteLine(" [Pressione qualquer tecla...]");
            Console.ReadKey();
        }
    }
}
