using Newtonsoft.Json;

    public sealed class SpeedBotConfig
    {
        [JsonProperty("token")]
        public string Token { get; private set; }
    }

