using System;
using System.Collections.Generic;
using System.Text;

namespace Parcial3AimeeMatias
{

    public abstract class Figura
    {
        public abstract int CalcularArea();
        public abstract int CalcularPerimetro();
    }

    public class Rectangulo : Figura
    {
        int bases, altura;

        public Rectangulo(int bases, int altura)
        {
            this.bases = bases;
            this.altura = altura;
        }

        public override int CalcularArea()
        {
            return bases * altura;
        }

        public override int CalcularPerimetro()
        {
            return 2 * (bases + altura);
        }
    }

    public class Triangulo : Figura
    {
        int bases, altura, lado2, lado3;

        public Triangulo(int bases, int altura, int lado2, int lado3)
        {
            this.bases = bases;
            this.altura = altura;
            this.lado2 = lado2;
            this.lado3 = lado3;
        }

        public override int CalcularArea()
        {
            return bases * altura / 2;
        }

        public override int CalcularPerimetro()
        {
            return bases + lado2 + lado3;
        }
    }

    public class Cuadrado : Figura
    {
        int lado;

        public Cuadrado(int lado)
        {
            this.lado = lado;
        }

        public override int CalcularArea()
        {
            return lado * lado;
        }

        public override int CalcularPerimetro()
        {
            return 4 * lado;
        }
    }
}