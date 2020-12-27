using System;

namespace Project
{
	public class Complex
	{
		/// <summary>
		/// сумма комплексного числа num1 и комплексного числа num2
		/// </summary>
		/// <param name="num1"></param>
		/// <param name="num2"></param>
		/// <returns></returns>

		public ComplexNum Sum(in ComplexNum num1, in ComplexNum num2)
		{
			return new ComplexNum(num1.Valid + num2.Valid, num1.Imaginary + num2.Imaginary);
		}

		/// <summary>
		/// сумма комплексного числа num1 и числа num2
		/// </summary>
		/// <param name="num1"></param>
		/// <param name="num2"></param>
		/// <returns></returns>

		public ComplexNum Sum(in ComplexNum num1, in double num2)
		{
			return new ComplexNum(num1.Valid + num2, num1.Imaginary);
		}

		/// <summary>
		/// разность комплексного числа num1 и комплексного числа num2
		/// </summary>
		/// <param name="num1"></param>
		/// <param name="num2"></param>
		/// <returns></returns>

		public ComplexNum Difference(in ComplexNum num1, in ComplexNum num2)
		{
			return new ComplexNum(num1.Valid - num2.Valid, num1.Imaginary - num2.Imaginary);
		}

		/// <summary>
		/// разность комплексного числа num1 и числа num2
		/// </summary>
		/// <param name="num1"></param>
		/// <param name="num2"></param>
		/// <returns></returns>

		public ComplexNum Difference(in ComplexNum num1, in double num2)
		{
			return new ComplexNum(num1.Valid - num2, num1.Imaginary);
		}

		/// <summary>
		/// деление комплексного числа num1 на комплексное число num2
		/// </summary>
		/// <param name="num1"></param>
		/// <param name="num2"></param>
		/// <returns></returns>
		
		public ComplexNum Division(in ComplexNum num1, in ComplexNum num2)
		{
			return new ComplexNum((num1.Valid * num2.Valid + num1.Imaginary * num2.Imaginary) / (Math.Pow(num2.Valid, 2) + Math.Pow(num2.Imaginary, 2)), (num1.Imaginary * num2.Valid - num1.Valid * num2.Imaginary) / (Math.Pow(num2.Valid, 2) + Math.Pow(num2.Imaginary, 2)));
		}

		/// <summary>
		/// деление комплексного числа num1 на число num2
		/// </summary>
		/// <param name="num1"></param>
		/// <param name="num2"></param>
		/// <returns></returns>
		
		public ComplexNum Division(in ComplexNum num1, in double num2)
		{
			return new ComplexNum(1.0 * num1.Valid / num2, 1.0 * num1.Imaginary / num2);
		}

		/// <summary>
		/// умножение комплексного числа num1 на комплексное число num2
		/// </summary>
		/// <param name="num1"></param>
		/// <param name="num2"></param>
		/// <returns></returns>
		
		public ComplexNum Addition(in ComplexNum num1, in ComplexNum num2)
		{
			return new ComplexNum(num1.Valid * num2.Valid - num1.Imaginary * num2.Imaginary, num1.Valid * num2.Imaginary + num2.Valid * num1.Imaginary);
		}

		/// <summary>
		/// умножение комплексного числа num1 на число num2
		/// </summary>
		/// <param name="num1"></param>
		/// <param name="num2"></param>
		/// <returns></returns>
		
		public ComplexNum Addition(in ComplexNum num1, in double num2)
		{
			return new ComplexNum(1.0 * num1.Valid * num2, 1.0 * num1.Imaginary * num2);
		}

		/// <summary>
		/// Возвращает корень из отрицательного числа
		/// </summary>
		/// <param name="num"></param>
		/// <returns></returns>
		
		public ComplexNum Sqrt(in double num)
		{
			if (num >= 0)
				return new ComplexNum(Math.Sqrt(num), 0);

			return new ComplexNum(0, Math.Sqrt(Math.Abs(num)));
		}

		/// <summary>
		/// Возвращает массив, который представляет собой результат вычисления корня n-ой степени из числа.
		/// </summary>
		/// <param name="num"></param>
		/// <param name="Stepen"></param>
		/// <returns></returns>

		public ComplexNum[] Sqrt(in double num, int Stepen)
		{
			if (num < 0)
			{
				double Mz = Math.Abs(num);
				ComplexNum[] Cnum = new ComplexNum[Stepen];

				for (int i = 0; i < Cnum.Length; i++)
					Cnum[i] = new ComplexNum(Math.Pow(Mz, 1d / Stepen) * Math.Cos((Math.PI + 2.0 * Math.PI * i) / Stepen), Math.Pow(Mz, 1d / Stepen) * Math.Sin((Math.PI + 2.0 * Math.PI * i) / Stepen));

				return Cnum;
			}
			else
				return null;
		}
	}
}