using Background.Shared.Common;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Background.Publisher
{
    public class Publisher : IHostedService
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;
        private readonly ILogger<Publisher> _logger;
        public Publisher(IConnectionMultiplexer connnectionMultiplexer, ILogger<Publisher> logger)
        {
            _connectionMultiplexer = connnectionMultiplexer;
            _logger = logger;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var subscriber = _connectionMultiplexer.GetSubscriber();
            while (!cancellationToken.IsCancellationRequested)
            {
                var sendEmailchannel = new RedisChannel(RedisChannels.SendEmail, RedisChannel.PatternMode.Literal);
                var createBackupChannel = new RedisChannel(RedisChannels.CreateBackup, RedisChannel.PatternMode.Literal);
                var Users = JsonConvert.SerializeObject("");
                await subscriber.PublishAsync(sendEmailchannel, "Sending Email");

                await Task.Delay(1000);

                await subscriber.PublishAsync(createBackupChannel, "Creating Backup");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
