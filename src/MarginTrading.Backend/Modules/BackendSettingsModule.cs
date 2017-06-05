﻿using Autofac;
using MarginTrading.Core.Settings;

namespace MarginTrading.Backend.Modules
{
    public class BackendSettingsModule : Module
    {
        private readonly MtBackendSettings _mtSettings;
        private readonly MarginSettings _settings;

        public BackendSettingsModule(MtBackendSettings mtSettings, MarginSettings settings)
        {
            _mtSettings = mtSettings;
            _settings = settings;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(_mtSettings.EmailSender).SingleInstance();
            builder.RegisterInstance(_mtSettings.SlackNotifications).SingleInstance();
            builder.RegisterInstance(_settings).SingleInstance();
        }
    }
}
