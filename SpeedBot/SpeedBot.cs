using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Commands;
using Newtonsoft.Json;
//using Odx;

// Note that to use commands you will need to 
// build the commands module from git first.
// Then you need to reference it.
    

    public sealed class SpeedBot
    {
        private SpeedBotConfig speedBot { get; set; }
        private DiscordClient discordClient { get; set; }
        private CommandModule commandModule { get; set; }
     
    /// <summary>
    /// Constructeur de la classe SpeedBot
    /// </summary>

    public static void Main(string[] args)
        {
            if (!File.Exists("config.json"))
                throw new FileNotFoundException("Bot's configuration file (config.json) was not found.");

            var json = string.Empty;
            using (var fs = File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs))
                json = sr.ReadToEnd();

            var cfg = JsonConvert.DeserializeObject<SpeedBotConfig>(json);

            SpeedBot bot = new SpeedBot(cfg);
            bot.StartAsync().GetAwaiter().GetResult();
            
        }

        public SpeedBot(SpeedBotConfig config)
        {
            this.speedBot = config;
        }
        
        //public JoinChannel(string p_strServer, string p_strChannel)
        //{
        //    this.Client.Servers.Single(s => s.Name == p_strServer).VoiceChannels.Single(v => v.Name == p_strChannel);
        //    return this;
        //}

        public async Task StartAsync()
        {
            this.discordClient = new DiscordClient(new DiscordConfig
            {
                Token = this.speedBot.Token,
                TokenType = TokenType.Bot,
                LogLevel = LogLevel.Debug,
                AutoReconnect = true
            });

            this.discordClient.DebugLogger.LogMessageReceived += (o, e) =>
            {
                Console.WriteLine($"[{e.TimeStamp.ToString("yyyy-MM-dd HH:mm:ss")}] [{e.Level}] {e.Message}");
            };

            // Let's enable the command service and
            // configure it with # prefix.
            this.commandModule = this.discordClient.UseCommands(new CommandConfig
            {
                Prefix = "!",
                SelfBot = false
            });

            // Let's add a ping command, that responds 
            // with pong
            this.commandModule.AddCommand("ping", async e =>
            {
                await e.Message.Respond("pong");
            });

            // Now a hello command, that responds with 
            // invoker's mention.
            this.commandModule.AddCommand("hello", async e =>
            {
                await e.Message.Respond($"Hello, {e.Author.Mention}!");
            });

            // Now if you run the bot, try invoking 
            // #ping or #hello.

            await this.discordClient.Connect();

            await Task.Delay(-1);
        }

        private void DebugLogger_LogMessageReceived(object sender, DebugLogMessageEventArgs e)
        {
            Console.WriteLine($"[{e.TimeStamp.ToString("yyyy-MM-dd HH:mm:ss")}] [{e.Level}] {e.Message}");
        }
    }

