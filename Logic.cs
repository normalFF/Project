using System;

namespace Project
{
	public struct ComplexNum
	{
		public double Valid;
		public double Imaginary;

		public ComplexNum(double Valid, double Imaginary)
		{
			this.Valid = Valid;
			this.Imaginary = Imaginary;
		}
	}

	class Logic
	{
		Complex complex = new Complex();

		/// <summary>
		/// Уравнения первой степени
		/// </summary>
		/// <param name="ArrNum"></param>
		/// <returns></returns>

		private string[] StepenOne(in double[] ArrNum)
		{
			string[] StringNum = new string[1];

			StringNum[0] = Convert.ToString(-ArrNum[0] / ArrNum[1]);

			return StringNum;
		}

		/// <summary>
		/// Уравнения второй степени
		/// </summary>
		/// <param name="ArrNum"></param>
		/// <returns></returns>

		private string[] StepenTwo(in double[] ArrNum)
		{
			if (ArrNum[2] == 0)
				return StepenOne(ArrNum);

			if (ArrNum[1] == 0)
			{
				if (ArrNum[0] < 0)
				{
					string[] StringNum = new string[2];
					StringNum[0] = Convert.ToString(Math.Sqrt(-ArrNum[0] / ArrNum[2]));
					StringNum[1] = Convert.ToString(Math.Sqrt(-ArrNum[0] / ArrNum[2]));

					return StringNum;
				}

				else
				{
					double D = Math.Pow(ArrNum[1], 2) - (4 * ArrNum[0] * ArrNum[2]);
					ComplexNum num = complex.Sqrt(D);
					num = complex.Division(num, 2.0 * ArrNum[2]);

					string[] str = new string[2];
					str[0] = " + " + Convert.ToString(Math.Round(Math.Abs(num.Imaginary), 2)) + "i";
					str[1] = " - " + Convert.ToString(Math.Round(Math.Abs(num.Imaginary), 2)) + "i";
					return str;
				}
			}
			else
			{
				double D = Math.Pow(ArrNum[1], 2) - (4 * ArrNum[0] * ArrNum[2]);

				if (D == 0)
				{
					string[] StringNum = new string[1];
					StringNum[0] = Convert.ToString(-ArrNum[1] / (2 * ArrNum[2]));

					return StringNum;
				}

				if (D > 0)
				{
					string[] StringNum = new string[2];

					StringNum[0] = Convert.ToString((-ArrNum[1] - Math.Sqrt(D)) / (2 * ArrNum[2]));
					StringNum[1] = Convert.ToString((-ArrNum[1] + Math.Sqrt(D)) / (2 * ArrNum[2]));

					return StringNum;
				}

				else
				{
					ComplexNum num = complex.Sqrt(D);
					num = complex.Division(complex.Sum(num, -ArrNum[1]), 2.0 * ArrNum[2]);

					string[] str = new string[2];
					str[0] = "" + Convert.ToString(Math.Round(num.Valid, 2)) + " + " + Convert.ToString(Math.Round(Math.Abs(num.Imaginary), 2)) + "i";
					str[1] = "" + Convert.ToString(Math.Round(num.Valid, 2)) + " - " + Convert.ToString(Math.Round(Math.Abs(num.Imaginary), 2)) + "i";
					return str;
				}
			}
		}

		/// <summary>
		/// Уравнения 
		/// </summary>
		/// <param name="ArrNum"></param>
		/// <returns></returns>

		private string[] StepenThee(in double[] ArrNum)
		{
			if (ArrNum[3] == 0)
				return StepenTwo(ArrNum);

			if (ArrNum[2] == 0 && ArrNum[1] == 0)
			{
				string[] str = new string[1];

				if (ArrNum[0] * ArrNum[3] > 0)
					str[0] = Convert.ToString(-Math.Pow(ArrNum[0] / ArrNum[3], 1d / 3d));
				else
					str[0] = Convert.ToString(Math.Pow(-ArrNum[0] / ArrNum[3], 1d / 3d));

				return str;
			}

			if (ArrNum[0] == 0)
			{
				double[] TNum = new double[] { ArrNum[1], ArrNum[2], ArrNum[3] };
				string[] TimeNum = StepenTwo(TNum);

				for (int i = 0; i < TimeNum.Length; i++)
				{
					if (TimeNum[i] == "0")
						return TimeNum;
				}

				string[] TimeNum2 = new string[TimeNum.Length + 1];
				TimeNum2[0] = "0";

				for (int i = 1; i < TimeNum2.Length; i++)
					TimeNum2[i] = TimeNum[i - 1];

				return TimeNum2;
			}

			else
			{
				double[] TimeArr = new double[4] { 1.0 * ArrNum[0] / ArrNum[3], 1.0 * ArrNum[1] / ArrNum[3], 1.0 * ArrNum[2] / ArrNum[3], 1 };
				double Q = (Math.Pow(TimeArr[2], 2) - 3.0 * TimeArr[1]) / 9;
				double R = (2 * Math.Pow(TimeArr[2], 3) - 9 * TimeArr[2] * TimeArr[1] + 27 * TimeArr[0]) / 54;
				double S = Math.Pow(Q, 3) - Math.Pow(R, 2);

				if (S > 0)
				{
					double Fi = Math.Round(Math.Acos(R / Math.Sqrt(Math.Pow(Q, 3))) / 3, 3);
					string[] str = new string[3] { "" + (-2 * Math.Sqrt(Q) * Math.Cos(Fi) - (1.0 * TimeArr[2] / 3)), "" + (-2 * Math.Sqrt(Q) * Math.Cos(Fi + 2.0 * Math.PI / 3) - (1.0 * TimeArr[2] / 3)), "" + (-2 * Math.Sqrt(Q) * Math.Cos(Fi - 2.0 * Math.PI / 3) - (1.0 * TimeArr[2] / 3)) };
					return str;
				}

				if (S < 0)
				{
					if (Q > 0)
					{
						double z = Math.Abs(R) / Math.Sqrt(Math.Pow(Q, 3));
						double Fi = Math.Round(Math.Log(z + Math.Sqrt(z + 1) * Math.Sqrt(z - 1)) / 3, 3);

						string[] str = new string[3];
						
						str[0] = "" + (-2.0 * Math.Sign(R) * Math.Sqrt(Q) * Math.Cosh(Fi) - 1.0 * TimeArr[2] / 3);

						ComplexNum num = new ComplexNum(Math.Sign(R) * Math.Sqrt(Q) * Math.Cosh(Fi) - 1.0 * TimeArr[2] / 3, Math.Sqrt(3) * Math.Sqrt(Q) * Math.Sinh(Fi));

						str[1] = "" + Math.Round(num.Valid, 2) + " + " + Math.Abs(Math.Round(num.Imaginary, 2)) + "i";
						str[2] = "" + Math.Round(num.Valid, 2) + " - " + Math.Abs(Math.Round(num.Imaginary, 2)) + "i";
						return str;
					}
					else
					{
						double z = Math.Abs(R) / Math.Sqrt(Math.Abs(Math.Pow(Q, 3)));
						double Fi = Math.Round(Math.Log(z + Math.Sqrt(z + 1) * Math.Sqrt(z - 1)) / 3, 3);

						string[] str = new string[3];

						str[0] = "" + (-2.0 * Math.Sign(R) * Math.Sqrt(Math.Abs(Q)) * Math.Sinh(Fi) - 1.0 * TimeArr[2] / 3);

						ComplexNum num = new ComplexNum(Math.Sign(R) * Math.Sqrt(Math.Abs(Q)) * Math.Sinh(Fi) - 1.0 * TimeArr[2] / 3, Math.Sqrt(3) * Math.Sqrt(Math.Abs(Q)) * Math.Cosh(Fi));

						str[1] = "" + Math.Round(num.Valid, 2) + " + " + Math.Abs(Math.Round(num.Imaginary, 2)) + "i";
						str[2] = "" + Math.Round(num.Valid, 2) + " - " + Math.Abs(Math.Round(num.Imaginary, 2)) + "i";
						return str;
					}
				}
				else
				{
					string[] str = new string[] { "" + (-2.0 * Math.Pow(R, 1d / 3d) - 1.0 * TimeArr[2] / 3), "" + (Math.Pow(R, 1d / 3d) - 1.0 * TimeArr[2] / 3) };
					return str;
				}
			}
		}


/*		private string[] StepenFour(in double[] ArrNum)
		{
			if (ArrNum[4] == 0)
				return StepenThee(ArrNum);

			if (ArrNum[0] == 0)
			{
				double[] TimeNum = new double[] { ArrNum[1], ArrNum[2], ArrNum[3], ArrNum[4] };
				string[] TimeString = StepenThee(TimeNum);
				string[] str = new string[TimeString.Length + 1];
				str[0] = "0";

				for (int i = 1; i < str.Length; i++)
					str[i] = TimeString[i - 1];

				return str;
			}

			if (ArrNum[1] == 0 && ArrNum[2] == 0 && ArrNum[3] == 0)
			{
				double x = -ArrNum[0] / ArrNum[4];

				if (x < 0)
				{
					ComplexNum[] num = complex.Sqrt(x, 4);
					string[] str = new string[num.Length];

					for (int i = 0; i < str.Length; i++)
					{
						if (num[i].Imaginary == 0)
						{
							str[i] = "" + (num[i].Valid);
							continue;
						}
						if (num[i].Valid == 0)
						{
							str[i] = "" + (num[i].Imaginary) + "i";
							continue;
						}
						else
						{
							if (num[i].Imaginary >= 0)
								str[i] = "" + Math.Round(num[i].Valid, 2) + " + " + Math.Round(num[i].Imaginary, 2) + "i";
							else
								str[i] = "" + Math.Round(num[i].Valid, 2) + " - " + Math.Abs(Math.Round(num[i].Imaginary, 2)) + "i";
						}
					}
					return str;
				}
				x = Math.Pow(x, 1d / 4d);
				return new string[] { "" + x, "-" + x, "" + Math.Round(x, 2) + "i", "-" + Math.Round(x, 2) + "i" };
			}

			else
			{
				double Koren = -4 * (Math.Pow(ArrNum[3], 2) - 3 * ArrNum[4] * ArrNum[2] + 12 * ArrNum[1]) + Math.Pow(2 * Math.Pow(ArrNum[3], 3) - 9 * ArrNum[4] * ArrNum[3] * ArrNum[2] + 27 * Math.Pow(ArrNum[2], 2) + 27 * Math.Pow(ArrNum[4], 2) * ArrNum[1] - 72 * ArrNum[3] * ArrNum[1], 2);
				double Znam = Math.Pow(2, 1d / 3d) * (Math.Pow(ArrNum[3], 2) - 3 * ArrNum[4] * ArrNum[2] + 12 * ArrNum[1]);


				if (Koren < 0)
				{
					ComplexNum koren = complex.Sqrt(Koren);
					koren = complex.Sum(koren, 2 * Math.Pow(ArrNum[3], 3) - 9 * ArrNum[4] * ArrNum[3] * ArrNum[2] + 27 * Math.Pow(ArrNum[2], 2) + 27 * Math.Pow(ArrNum[4], 2) * ArrNum[1] - 72 * ArrNum[3] * ArrNum[1]);
					
				}

			}
		} */


		public string[] CalculateFunc(in double[] ArrNum)
		{
			switch (ArrNum.Length)
			{
				case 2:
					return StepenOne(ArrNum);
				case 3:
					return StepenTwo(ArrNum);
				case 4:
					return StepenThee(ArrNum);
				case 5:
				//	return StepenFour(ArrNum);
				case 6:
				//					StepenFive(Num);
				//					return;

				default:
					throw new Exception();
			}
		}


		public double ReturnValue(in double[] ArrayValue, in double Result)
		{
			double TimeNum = ArrayValue[0];

			for (int i = 1; i < ArrayValue.Length; i++)
			{
				TimeNum += ArrayValue[i] * Math.Pow(Result, i);
			}

			return TimeNum;
		}
	}
}