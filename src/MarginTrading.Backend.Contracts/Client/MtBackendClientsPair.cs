﻿using System;

namespace MarginTrading.Backend.Contracts.Client
{
    internal class MtBackendClientsPair : IMtBackendClientsPair
    {
        public IMtBackendClient Demo { get; }
        public IMtBackendClient Live { get; }

        public MtBackendClientsPair(IMtBackendClient demo, IMtBackendClient live)
        {
            Demo = demo ?? throw new ArgumentNullException(nameof(demo));
            Live = live ?? throw new ArgumentNullException(nameof(live));
        }
    }
}