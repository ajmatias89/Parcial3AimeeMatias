using Newtonsoft.Json;

namespace EjecutarScript
{
    public class Program
    {
        static void Main(string[] args)
        {
            var persona = new
            {
                Nombre = "Cristell",
                Edad = 20,
                Carrera = "Ingeniería en Sistemas"
            };

            string json = JsonConvert.SerializeObject(persona);

            Console.WriteLine(json);
        }
    }
}