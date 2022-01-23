// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class DataId
    {
        public string source { get; set; }
        public string symbol { get; set; }
        public int period { get; set; }
    }

    public class Prices
    {
        public int ver { get; set; }
        public DataId dataId { get; set; }
        public string terminal { get; set; }
        public string company { get; set; }
        public string server { get; set; }
        public string symbol { get; set; }
        public int period { get; set; }
        public string baseCurrency { get; set; }
        public string priceIn { get; set; }
        public int lotSize { get; set; }
        public int stopLevel { get; set; }
        public int tickValue { get; set; }
        public double minLot { get; set; }
        public int maxLot { get; set; }
        public double lotStep { get; set; }
        public int spread { get; set; }
        public double point { get; set; }
        public int digits { get; set; }
        public int bars { get; set; }
        public int swapLong { get; set; }
        public int swapShort { get; set; }
        public int swapThreeDays { get; set; }
        public int swapType { get; set; }
        public int swapMode { get; set; }
        public int commission { get; set; }
        public int timezoneShift { get; set; }
        public List<int> time { get; set; }
        public List<double> open { get; set; }
        public List<double> high { get; set; }
        public List<double> low { get; set; }
        public List<double> close { get; set; }
        public List<int> volume { get; set; }
    }

