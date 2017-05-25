using Newtonsoft.Json;

namespace SignalRServiceHub
{
    public class ClockModel
    {
        [JsonProperty("categoryName")]
        public string CategoryName { get; set; }

        [JsonProperty("value")]
        public double Value { get; set; }
    }
}