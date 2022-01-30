// See https://aka.ms/new-console-template for more information
using System.Text.Json;
using System.Text.Json.Serialization;
using IGTrading;
using TALib;


var pricesRaw=Importing.JsonReadFile("C:/Users/cyril.henry/source/ig-trading/IGTrading/Data/EURUSD/EURUSD_M15.json");
var EPOCH2000=946684800; // ms from 2000-01-01;
var prices=Importing.MapToDto(pricesRaw,EPOCH2000);



double[] sma20 = new double[pricesRaw.close.Count];
double[] sma200 = new double[pricesRaw.close.Count];

sma20 = TA.SMA(pricesRaw.close.ToArray(), 20);
sma200 = TA.SMA(pricesRaw.close.ToArray(), 200);
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

for (int mmH = 70; mmH < 80; mmH++)
{
    for (int mmL = 70; mmL < 80; mmL++)
    {
        foreach (var p in prices)
        {
            if (sma20[lastIndex] > sma200[lastIndex] && isLong == false)
            {
                bought = p.close * contract * quantity;
                isLong = true;
                // Console.WriteLine($"{p.timeStamp} - buy:{p.close}");
            }
            if (sma20[lastIndex] < sma200[lastIndex] && isLong)
            {
                sold = (p.close - spread) * contract * quantity;
                solde = solde + (sold - bought);
                isLong = false;
                // Console.WriteLine($"{p.timeStamp} - sell:{p.close} - Gain:{(sold-bought)}");
                bought = 0;
                sold = 0;
            }
            lastIndex++;
            // finalResult = solde + btc * p.close;
            // buyAndHold=(1000 / p.close * df['close'].iloc[-1]
        }
        if (solde > 120000) Console.WriteLine($"MMH:{mmH} - MML:{mmL} - solde restant: {solde}");
        solde = 100000;
        sma20 = new double[pricesRaw.close.Count + mmL];
        sma200 = new double[pricesRaw.close.Count + mmH];
        lastIndex = 0;
        sma20 = TA.SMA(pricesRaw.close.ToArray(), mmL);
        sma200=TA.SMA(pricesRaw.close.ToArray(), mmH);

    }
}


