using Background.Shared.Common;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Background.Subscriber
{
    public class Subscriber : IHostedService
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;
        private readonly ILogger<Subscriber> _logger;
        public Subscriber(IConnectionMultiplexer connectionMultiplexer, ILogger<Subscriber> logger)
        {
            _connectionMultiplexer = connectionMultiplexer;
            _logger = logger;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var subscriber = _connectionMultiplexer.GetSubscriber();
            var sendEmailchannel = new RedisChannel(RedisChannels.SendEmail, RedisChannel.PatternMode.Literal);
            var createBackupchannel = new RedisChannel(RedisChannels.CreateBackup, RedisChannel.PatternMode.Literal);

            await subscriber.SubscribeAsync(sendEmailchannel, (channel, message) =>
            {
                Console.WriteLine(message);
            });

            await subscriber.SubscribeAsync(createBackupchannel, (channel, message) =>
            {
                Console.WriteLine(message);
            });
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
