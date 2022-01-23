// See https://aka.ms/new-console-template for more information
using System.Text.Json;
using System.Text.Json.Serialization;

var filepath="C:/Users/cyril.henry/source/ig-trading/IGTrading/Data/EURUSD/EURUSD_M15.json";

var pricesJson=File.ReadAllText(filepath);


var prices= new List<PriceDto>();
var pricesRaw= JsonSerializer.Deserialize<Prices>(pricesJson);

foreach (var p in pricesRaw.time)
{
   var i=pricesRaw.time.IndexOf(p);
   
   prices.Add(new PriceDto(){
        timeStamp=DateTime.Parse(p),
        open=(decimal)pricesRaw.open.ElementAt(i)
   });
}