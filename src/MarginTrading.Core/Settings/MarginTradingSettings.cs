﻿using Lykke.SettingsReader.Attributes;

namespace MarginTrading.Core.Settings
{
	public class MtBackendSettings
	{
		public MarginTradingSettings MtBackend { get; set; }
		public EmailSenderSettings EmailSender { get; set; }
		public SlackSettings SlackNotifications { get; set; }
	}

	public class MarginTradingSettings
	{
		public MarginSettings MarginTradingLive { get; set; }
		public MarginSettings MarginTradingDemo { get; set; }
	}

	public class MarginSettings
	{
		public string MetricLoggerLine { get; set; }
		public string ApiRootUrl { get; set; }
		public string ApiKey { get; set; }
		public string DemoAccountIdPrefix { get; set; }
		public bool RemoteConsoleEnabled { get; set; }
		public string ClientAccountServiceApiUrl { get; set; }

		#region from Env variables

		[Optional]
		public string Env { get; set; }

		[Optional]
		public bool IsLive { get; set; }

		#endregion

		public Db Db { get; set; }
		public RabbitMqQueues RabbitMqQueues { get; set; }
		public RabbitMqSettings RabbitMqSettings { get; set; }
		public string MtRabbitMqConnString { get; set; }
	}

	public class SlackSettings
	{
		public AzureQueueSettings AzureQueue { get; set; }
		public string ThrottlingLimitSeconds { get; set; }
	}

	public class AzureQueueSettings
	{
		public string ConnectionString { get; set; }
		public string QueueName { get; set; }
	}

	public class EmailSenderSettings
	{
		public AzureQueueSettings AzureQueue { get; set; }
		public string ServiceUrl { get; set; }
	}

	public class Db
	{
		public string LogsConnString { get; set; }
		public string MarginTradingConnString { get; set; }
		public string ClientPersonalInfoConnString { get; set; }
		public string DictsConnString { get; set; }
		public string SharedStorageConnString { get; set; }
		public string HistoryConnString { get; set; }
		public string StateConnString { get; set; }
	}

	public class RabbitMqQueues
	{
		public RabbitMqQueueInfo AccountHistory { get; set; }
		public RabbitMqQueueInfo OrderHistory { get; set; }
		public RabbitMqQueueInfo OrderRejected { get; set; }
		public RabbitMqQueueInfo OrderbookPrices { get; set; }
		public RabbitMqQueueInfo OrderChanged { get; set; }
		public RabbitMqQueueInfo AccountChanged { get; set; }
		public RabbitMqQueueInfo AccountStopout { get; set; }
		public RabbitMqQueueInfo UserUpdates { get; set; }
	}

	public class RabbitMqQueueInfo
	{
		public string QueueName { get; set; }
		public string ExchangeName { get; set; }
	}

	public class RabbitMqSettings
	{
		public string ConnectionString { get; set; }
		public string QueueName { get; set; }
		public string ExchangeName { get; set; }
	}
}
