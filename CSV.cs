using CsvHelper;
using System.Globalization;

namespace IGTrading
{
    public static class CSV
    {
        public static string WriteObject(List<PriceDto> pri)
        {
            using (var writer = new StreamWriter("..\\file.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(pri);
            }
            return "OK";
        }
    }
}