﻿using Autofac;
using AzureStorage.Tables;
using Common.Log;
using MarginTrading.AzureRepositories;
using MarginTrading.AzureRepositories.Clients;
using MarginTrading.AzureRepositories.Logs;
using MarginTrading.AzureRepositories.Reports;
using MarginTrading.Backend.Core;
using MarginTrading.Backend.Core.Clients;
using MarginTrading.Backend.Core.MatchingEngines;
using MarginTrading.Backend.Core.Settings;
using MarginTrading.Common.Settings.Repositories;
using MarginTrading.Common.Settings.Repositories.Azure;
using MarginTrading.Common.Settings.Repositories.Azure.Entities;
using MarginTrading.Services;
using MarginTrading.Services.MatchingEngines;

namespace MarginTrading.Backend.Modules
{
	public class BackendRepositoriesModule : Module
	{
		private readonly MarginSettings _settings;
		private readonly ILog _log;

		public BackendRepositoriesModule(MarginSettings settings, ILog log)
		{
			_settings = settings;
			_log = log;
		}

		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterInstance(_log)
				.As<ILog>()
				.SingleInstance();

		    builder.Register<IMarginTradingOperationsLogRepository>(ctx =>
		        new MarginTradingOperationsLogRepository(
		            AzureTableStorage<OperationLogEntity>.Create(() => _settings.Db.LogsConnString,
		                "MarginTradingBackendOperationsLog", _log))
		    ).SingleInstance();

			builder.Register<IClientSettingsRepository>(ctx =>
				new ClientSettingsRepository(
					AzureTableStorage<ClientSettingsEntity>.Create(
						() => _settings.Db.ClientPersonalInfoConnString, "TraderSettings", _log)));

			builder.Register<IClientAccountsRepository>(ctx =>
				AzureRepoFactories.Clients.CreateClientsRepository(_settings.Db.ClientPersonalInfoConnString, _log)
			).SingleInstance();

			builder.Register<IMarginTradingAccountsRepository>(ctx =>
				AzureRepoFactories.MarginTrading.CreateAccountsRepository(_settings.Db.MarginTradingConnString, _log)
			).SingleInstance();

			builder.Register<IMarginTradingAccountStatsRepository>(ctx =>
				AzureRepoFactories.MarginTrading.CreateAccountStatsRepository(_settings.Db.HistoryConnString, _log)
			).SingleInstance();

			builder.Register<IMarginTradingOrdersHistoryRepository>(ctx =>
				AzureRepoFactories.MarginTrading.CreateOrdersHistoryRepository(_settings.Db.HistoryConnString, _log)
			).SingleInstance();

			builder.Register<IMarginTradingAccountHistoryRepository>(ctx =>
				AzureRepoFactories.MarginTrading.CreateAccountHistoryRepository(_settings.Db.HistoryConnString, _log)
			).SingleInstance();

			builder.Register<IMatchingEngineRoutesRepository>(ctx =>
				AzureRepoFactories.MarginTrading.CreateMatchingEngineRoutesRepository(_settings.Db.MarginTradingConnString, _log)
			).SingleInstance();

			builder.Register<IMarginTradingConditionRepository>(ctx =>
				AzureRepoFactories.MarginTrading.CreateTradingConditionsRepository(_settings.Db.MarginTradingConnString, _log)
			).SingleInstance();

			builder.Register<IMarginTradingAccountGroupRepository>(ctx =>
				AzureRepoFactories.MarginTrading.CreateAccountGroupRepository(_settings.Db.MarginTradingConnString, _log)
			).SingleInstance();

			builder.Register<IAccountAssetPairsRepository>(ctx =>
				AzureRepoFactories.MarginTrading.CreateAccountAssetsRepository(_settings.Db.MarginTradingConnString, _log)
			).SingleInstance();

			builder.Register<IAssetPairsRepository>(ctx =>
				AzureRepoFactories.MarginTrading.CreateAssetsRepository(_settings.Db.DictsConnString, _log)
			).SingleInstance();

			builder.Register<IMarginTradingBlobRepository>(ctx =>
				AzureRepoFactories.MarginTrading.CreateBlobRepository(_settings.Db.StateConnString)
			).SingleInstance();

			builder.Register<IAppGlobalSettingsRepositry>(ctx =>
				new AppGlobalSettingsRepository(AzureTableStorage<AppGlobalSettingsEntity>.Create(
					() => _settings.Db.ClientPersonalInfoConnString, "Setup", _log)));

			builder.Register<IAccountsStatsReportsRepository>(ctx =>
				AzureRepoFactories.MarginTrading.CreateAccountsStatsReportsRepository(_settings.Db.ReportsConnString, _log)
			).SingleInstance();

			builder.Register<IAccountsReportsRepository>(ctx =>
				AzureRepoFactories.MarginTrading.CreateAccountsReportsRepository(_settings.Db.ReportsConnString, _log)
			).SingleInstance();
			
			builder.Register<IRiskSystemCommandsLogRepository>(ctx =>
				AzureRepoFactories.MarginTrading.CreateRiskSystemCommandsLogRepository(_settings.Db.LogsConnString, _log)
			).SingleInstance();

			builder.RegisterType<MatchingEngineInMemoryRepository>()
				.As<IMatchingEngineRepository>()
				.SingleInstance();
		}
	}
}
