using System.Text.Json;
using System.Globalization;
using System.Text.Json.Serialization;

namespace IGTrading
{
    public static class Importing
    {
        public static Prices JsonReadFile(string filepath)
        {
            var pricesJson = File.ReadAllText(filepath);
            var pricesRaw = JsonSerializer.Deserialize<Prices>(pricesJson);

            return pricesRaw;
        }
        public static List<PriceDto> MapToDto(Prices pricesRaw, int timeShift)
        {

            var prices = new List<PriceDto>();
            foreach (var p in pricesRaw.time)
            {
                var i = pricesRaw.time.IndexOf(p);
                prices.Add(new PriceDto()
                {
                    timeStamp = DateTimeOffset.FromUnixTimeSeconds(p * 60 + timeShift).DateTime,
                    open = pricesRaw.open.ElementAt(i),
                    close = pricesRaw.close.ElementAt(i),
                    high = pricesRaw.high.ElementAt(i),
                    low = pricesRaw.low.ElementAt(i),
                    //sma1 = sma20[i],
                    //sma2 = sma200[i]
                });
            }
            return prices;
        }
    }
}