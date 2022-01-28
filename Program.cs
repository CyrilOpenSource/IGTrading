// See https://aka.ms/new-console-template for more information
using System.Text.Json;
using System.Globalization;
using System.Text.Json.Serialization;
using TALib;
using CsvHelper;

var pricesRaw=Importing.JsonReadFile("C:/Users/cyril.henry/source/ig-trading/IGTrading/Data/EURUSD/EURUSD_M15.json");
var EPOCH2000=946684800; // ms from 2000-01-01;
var prices=Importing.MapToDto(pricesRaw,EPOCH2000);

// ms from 2000-01-01;
int outBgd =20;

double[] sma20 = new double[pricesRaw.close.Count];
double[] sma200 = new double[pricesRaw.close.Count];

int elementCount = 20;
Core.Sma(pricesRaw.close.ToArray(), 0, pricesRaw.close.Count-1, sma20, out outBgd, out elementCount,2);
Core.Sma(pricesRaw.close.ToArray(),0, pricesRaw.close.Count-1, sma200, out outBgd, out elementCount,100);


//var debug=CSV.WriteObject(prices);
double solde = 100000;
double quantity = 10;
double contract=10000;

var lastIndex = 0;
var spread=0;
var finalResult=0;
var isLong=false;
double bought=0;
double sold=0;

for (int mmH=50;mmH<601;mmH++)
{
   for (int mmL=2;mmL<101;mmL++){
    foreach (var p in prices)
    {
           if (sma20[lastIndex] > sma200[lastIndex] && isLong==false)
            {       
                bought=p.close*contract*quantity;
                isLong=true;
               // Console.WriteLine($"{p.timeStamp} - buy:{p.close}");
            }
            if (sma20[lastIndex] < sma200[lastIndex] && isLong)
            {
                sold=(p.close-spread)*contract*quantity;
                solde=solde + (sold-bought);
                isLong=false;
               // Console.WriteLine($"{p.timeStamp} - sell:{p.close} - Gain:{(sold-bought)}");
                bought=0;
                sold=0;
            }
        lastIndex++;
    // finalResult = solde + btc * p.close;
    // buyAndHold=(1000 / p.close * df['close'].iloc[-1]
    }
    if (solde>120000) Console.WriteLine($"MMH:{mmH} - MML:{mmL} - solde restant: {solde}");
    solde = 100000;
    sma20 = new double[pricesRaw.close.Count+mmL];
    sma200 = new double[pricesRaw.close.Count+mmH];
    lastIndex=0;
    Core.Sma(pricesRaw.close.ToArray(), 1, pricesRaw.close.Count-1, sma20, out outBgd, out elementCount,mmL);
    Core.Sma(pricesRaw.close.ToArray(), 1, pricesRaw.close.Count-1, sma200, out outBgd, out elementCount,mmH);
   }
}
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
public static class Importing
{
    public static Prices JsonReadFile(string filepath) {
        
        var pricesJson=File.ReadAllText(filepath);
        var pricesRaw= JsonSerializer.Deserialize<Prices>(pricesJson);

        return pricesRaw;
    }
    public static List<PriceDto> MapToDto(Prices pricesRaw,int timeShift){

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
