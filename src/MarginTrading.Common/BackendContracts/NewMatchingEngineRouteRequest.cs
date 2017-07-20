﻿using System;
using MarginTrading.Core;

namespace MarginTrading.Common.BackendContracts
{
    public class NewMatchingEngineRouteRequest 
    {        
        public int Rank { get; set; }
        public string TradingConditionId { get; set; }
        public string ClientId { get; set; }
        public string Instrument { get; set; }
        public OrderDirection? Type { get; set; }
        public string MatchingEngineId { get; set; }
        public string Asset { get; set; }
        public AssetType? AssetType { get; set; }

        public static IMatchingEngineRoute Create(NewMatchingEngineRouteRequest request)
        {
            return new MatchingEngineRoute()
            {
                Id = Guid.NewGuid().ToString().ToUpper(),
                Rank = request.Rank,
                TradingConditionId = request.TradingConditionId,
                ClientId = request.ClientId,
                Instrument = request.Instrument,
                Type = request.Type,
                MatchingEngineId = request.MatchingEngineId,
                Asset = request.Asset,
                AssetType = request.AssetType
            };
        }

    }
}
