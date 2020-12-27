using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Project
{
	public partial class MainWindow : Window
	{
		private int NumArray;
		private double[] ArrNum;
		private string[] StringNum;
		readonly Logic Log = new Logic();
		private int[] ValueDrow;
		TextBox[] TB;
		TextBlock[] TBlock;

		private ChartStyleGridlines cs;
		private DataCollection dc;
		private DataSeries ds;

		public MainWindow()
		{
			InitializeComponent();
			TB = new TextBox[] { Text_Box_0, Text_Box_1, Text_Box_2, Text_Box_3, Text_Box_4, Text_Box_5 };
			TBlock = new TextBlock[] { Text_Block_2, Text_Block_3, Text_Block_4, Text_Block_5 };
			ValueDrow = new int[] { -5, 5, -3, 6, 5 };
		}

		/// <summary>
		/// Нахождение результата
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>

		private void SolutionClick(object sender, RoutedEventArgs e)
		{
			ArrNum = ReturnArray(NumArray);

			StringNum = Log.CalculateFunc(ArrNum);
			ReturnResult();

			CreateValueDrow();
			textCanvas.Width = chartGrid.ActualWidth;
			textCanvas.Height = chartGrid.ActualHeight;
			chartCanvas.Children.Clear();
			textCanvas.Children.RemoveRange(1, textCanvas.Children.Count - 1);
			AddChart(ValueDrow);
		}

		/// <summary>
		/// ComboBox
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>

		private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			NumArray = 1 + Convert.ToInt32(((ComboBoxItem)((ComboBox)sender).SelectedValue).Content);

			VisibilityComponent(NumArray - 1);
		}

		/// <summary>
		/// Получение значений из TextBox
		/// </summary>
		/// <param name="Num"></param>
		/// <returns></returns>

		private double[] ReturnArray(int Num)
		{
			double[] TimeArray = new double[Num];

			for (int i = 0; i < Num; i++)
			{
				try
				{
					TimeArray[i] = Convert.ToDouble(((TextBox)TB[i]).Text);
				}
				catch (Exception)
				{
					TimeArray[i] = 0;
					((TextBox)TB[i]).Text = "0";
				}
			}

			return TimeArray;
		}

		/// <summary>
		/// Вывод результата
		/// </summary>

		private void ReturnResult()
		{
			OutputDecision.Text = "";
			OutputDecision.Height = 18;

			StringBuilder TimeString = new StringBuilder();

			try
			{
				for (int i = 0; i < StringNum.Length; i++)
				{
					TimeString.Append("X" + Convert.ToString(i + 1) + " = " + StringNum[i] + "\n");
				}
			}
			catch (NullReferenceException)
			{
				OutputDecision.Text = "Нет корней";
			}

			OutputDecision.Height *= StringNum.Length;
			OutputDecision.Text = TimeString.ToString();
		}

		/// <summary>
		/// Отображение компонентов окна
		/// </summary>
		/// <param name="Num"></param>

		private void VisibilityComponent(int Num)
		{
			if (Text_Block_2 == null)
				return;

			for (int i = 1; i < 5; i++)
			{
				if (i < Num)
				{
					TB[i + 1].Visibility = Visibility.Visible;
					TBlock[i - 1].Visibility = Visibility.Visible;
				}
				else
				{
					TB[i + 1].Visibility = Visibility.Hidden;
					TBlock[i - 1].Visibility = Visibility.Hidden;
				}
			}
		}

		/// <summary>
		/// Проверка результатов
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>

		private void CheckResult(object sender, RoutedEventArgs e)
		{
			if (StringNum == null)
				return;

			StringBuilder TimeString = new StringBuilder();

			for (int i = 0; i < StringNum.Length; i++)
			{
				try
				{
					double TimeNum = Convert.ToDouble(StringNum[i]);

					TimeString.Append($"F({StringNum[i]}) = {Log.ReturnValue(ArrNum, TimeNum)}\n");
				}
				catch (Exception)
				{
					break;
				}
			}

			OutputCheck.Text = "" + TimeString;
		}

		/// <summary>
		/// Генерация графика уравнения
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>

		private void CreateValueDrow()
		{
			xMaxValue.Text = "" + Convert.ToInt32(xMaxValue.Text);
			xMinValue.Text = "" + Convert.ToInt32(xMinValue.Text);
			yMaxValue.Text = "" + Convert.ToInt32(yMaxValue.Text);
			yMinValue.Text = "" + Convert.ToInt32(yMinValue.Text);
			ValuePoint.Text = "" + Convert.ToInt32(ValuePoint.Text);

			ValueDrow = new int[] { Convert.ToInt32(xMaxValue.Text), Convert.ToInt32(xMinValue.Text), Convert.ToInt32(yMaxValue.Text), Convert.ToInt32(yMinValue.Text), Convert.ToInt32(ValuePoint.Text) };

			if (ValueDrow[0] < ValueDrow[1] + 9)
			{
				ValueDrow[0] = ValueDrow[1] + 10;
				xMaxValue.Text = "" + ValueDrow[0];
			}
			if (ValueDrow[0] > ValueDrow[1] + 19)
			{
				ValueDrow[0] = ValueDrow[1] + 20;
				xMaxValue.Text = "" + ValueDrow[0];
			}
			if (ValueDrow[2] < ValueDrow[3] + 6)
			{
				ValueDrow[2] = ValueDrow[3] + 7;
				yMaxValue.Text = "" + ValueDrow[2];
			}
			if (ValueDrow[2] > ValueDrow[3] + 19)
			{
				ValueDrow[2] = ValueDrow[3] + 20;
				yMaxValue.Text = "" + ValueDrow[2];
			}
			if (ValueDrow[4] > 10)
			{
				ValueDrow[4] = 10;
				ValuePoint.Text = "" + ValueDrow[4];
			}
			if (ValueDrow[4] < 5)
			{
				ValueDrow[4] = 5;
				ValuePoint.Text = "" + ValueDrow[4];
			}
		}

		private void Drow_Click(object sender, RoutedEventArgs e)
		{
			CreateValueDrow();
			textCanvas.Width = chartGrid.ActualWidth;
			textCanvas.Height = chartGrid.ActualHeight;
			chartCanvas.Children.Clear();
			textCanvas.Children.RemoveRange(1, textCanvas.Children.Count - 1);
			AddChart(ValueDrow);
		}

		private void AddChart(int[] Arr)
		{
			cs = new ChartStyleGridlines();
			dc = new DataCollection();
			cs.ChartCanvas = chartCanvas;
			cs.TextCanvas = textCanvas;
			cs.Title = "График уравнения";
			cs.Xmin = Arr[1];
			cs.Xmax = Arr[0];
			cs.Ymin = Arr[3];
			cs.Ymax = Arr[2];
			cs.YTick = 0.5;
			cs.XTick = 0.5;
			cs.GridlinePattern = ChartStyleGridlines.GridlinePatternEnum.Dot;
			cs.GridlineColor = Brushes.Black;
			cs.AddChartStyle(tbTitle, tbXLabel, tbYLabel);

			if (Arr[1] < 0 && Arr[0] > 0)
			{
				ds = new DataSeries();
				ds.LineColor = Brushes.Red;
				ds.LineThickness = 2;

				for (int i = Arr[3]; i <= Arr[2]; i++)
				{
					double x = 0;
					double y = i;
					ds.LineSeries.Points.Add(new Point(x, y));
				}

				dc.DataList.Add(ds);
			}

			if (Arr[3] < 0 && Arr[2] > 0)
			{
				ds = new DataSeries();
				ds.LineColor = Brushes.Blue;
				ds.LineThickness = 2;

				for (int i = Arr[1]; i <= Arr[0]; i++)
				{
					double x = i;
					double y = 0;
					ds.LineSeries.Points.Add(new Point(x, y));
				}
				dc.DataList.Add(ds);
			}

			if (ArrNum != null)
			{
				ds = new DataSeries();
				ds.LineColor = Brushes.Red;
				ds.LinePattern = DataSeries.LinePatternEnum.DashDot;
				ds.LineThickness = 2;

				int points = Arr[4] * (Arr[0] - Arr[1]);
				for (int i = Arr[4] * Arr[1]; i <= points; i++)
				{
					double x = i * 1.0 / Arr[4];
					double y = Log.ReturnValue(ArrNum, x);
					ds.LineSeries.Points.Add(new Point(x, y));
				}
				dc.DataList.Add(ds);
			}

			dc.AddLines(cs);
		}

		private void chartGrid_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			textCanvas.Width = chartGrid.ActualWidth;
			textCanvas.Height = chartGrid.ActualHeight;
			chartCanvas.Children.Clear();
			textCanvas.Children.RemoveRange(1, textCanvas.Children.Count - 1);
			AddChart(ValueDrow);
		}

		private void Value_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			((TextBox)sender).SelectionStart = ((TextBox)sender).Text.Length;

			if (!(Char.IsDigit(e.Text, 0) || (e.Text == "-") && !((TextBox)sender).Text.Contains("-") && ((TextBox)sender).Text.Length == 0))
			{
				e.Handled = true;
			}
		}

		private void Text_Box_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			((TextBox)sender).SelectionStart = ((TextBox)sender).Text.Length;

			if (!(Char.IsDigit(e.Text, 0) || (e.Text == "-") && !((TextBox)sender).Text.Contains("-") && ((TextBox)sender).Text.Length == 0 ||
				(e.Text == ",") && !((TextBox)sender).Text.Contains("-") && !((TextBox)sender).Text.Contains(",") && ((TextBox)sender).Text.Length != 0 ||
				(e.Text == ",") && ((TextBox)sender).Text.Contains("-") && ((TextBox)sender).CaretIndex > 1 && !((TextBox)sender).Text.Contains(",") && ((TextBox)sender).Text.Length > 1))
			{
				e.Handled = true;
			}
		}
	}
}