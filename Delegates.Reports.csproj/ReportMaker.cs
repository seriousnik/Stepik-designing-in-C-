using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates.Reports
{
	public static class ReportCalculation
	{
		public static object MakeStatisticsMeanAndStd(IEnumerable<double> _data)
		{
			var data = _data.ToList();
			var mean = data.Average();
			var std = Math.Sqrt(data.Select(z => Math.Pow(z - mean, 2)).Sum() / (data.Count - 1));
			return new MeanAndStd { Mean = mean, Std = std };
		}
		public static object MakeStatisticsMedian(IEnumerable<double> data)
		{
			var list = data.OrderBy(z => z).ToList();
			if (list.Count % 2 == 0)
				return (list[list.Count / 2] + list[list.Count / 2 - 1]) / 2;
			return list[list.Count / 2];
		}

	}
	public static class WrapperReportMaker
	{
		public static string HTMLReportMaker(string caption, IEnumerable<Measurement> data, Func<IEnumerable<double>, object> func)
		{
			var temperature = func(data.Select(e => e.Temperature));
			var humidity = func(data.Select(e => e.Humidity));
			return $"<h1>{caption}</h1><ul><li><b>Temperature</b>: {temperature}<li><b>Humidity</b>: {humidity}</ul>";
		}

		public static string MarkdownReportMaker(string caption, IEnumerable<Measurement> data, Func<IEnumerable<double>, object> func)
		{
			var temperature = func(data.Select(e => e.Temperature));
			var humidity = func(data.Select(e => e.Humidity));
			return $"## {caption}\n\n * **Temperature**: {temperature}\n\n * **Humidity**: {humidity}\n\n";
		}
	}
	public static class ReportMakerHelper
	{
		public static string MeanAndStdHtmlReport(IEnumerable<Measurement> data)
		{
			return WrapperReportMaker.HTMLReportMaker("Mean and Std", data, ReportCalculation.MakeStatisticsMeanAndStd);
		}

		public static string MedianMarkdownReport(IEnumerable<Measurement> data)
		{
			return WrapperReportMaker.MarkdownReportMaker("Median", data, ReportCalculation.MakeStatisticsMedian);
		}

		public static string MeanAndStdMarkdownReport(IEnumerable<Measurement> measurements)
		{
			return WrapperReportMaker.MarkdownReportMaker("Mean and Std", measurements, ReportCalculation.MakeStatisticsMeanAndStd);
		}

		public static string MedianHtmlReport(IEnumerable<Measurement> measurements)
		{
			return WrapperReportMaker.HTMLReportMaker("Median", measurements, ReportCalculation.MakeStatisticsMedian);
		}
	}
}
