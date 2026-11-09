namespace Parcial3AimeeMatias
{
    public class Program
    {

        static void Main(string[] args)
        {
            Figura[] vector = new Figura[3];

            vector[0] = new Rectangulo(5, 3);
            vector[1] = new Cuadrado(4);
            vector[2] = new Triangulo(3, 4, 4, 5);

            foreach (Figura figura in vector)
            {
                Console.WriteLine("Figura: " + figura.GetType().Name);
                Console.WriteLine("Area: " + figura.CalcularArea());
                Console.WriteLine("Perimetro: " + figura.CalcularPerimetro());
            }

            Console.ReadKey();
        }


    }
}