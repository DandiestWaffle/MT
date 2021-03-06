﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureStorage;
using AzureStorage.Tables;
using Common;
using MarginTrading.Backend.Core;
using MarginTrading.Backend.Core.MatchedOrders;
using MarginTrading.Backend.Core.MatchingEngines;
using Microsoft.WindowsAzure.Storage.Table;

namespace MarginTrading.AzureRepositories
{
    public class MarginTradingOrderHistoryEntity : TableEntity, IOrderHistory
    {
        public string Id { get; set; }
        public long Code { get; set; }
        public string ClientId { get; set; }
        public string AccountId { get; set; }
        public string TradingConditionId { get; set; }
        public string AccountAssetId { get; set; }
        public string Instrument { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? OpenDate { get; set; }
        public DateTime? CloseDate { get; set; }
        decimal? IOrderHistory.ExpectedOpenPrice => (decimal?) ExpectedOpenPrice;
        public double? ExpectedOpenPrice { get; set; }
        decimal IOrderHistory.OpenPrice => (decimal) OpenPrice;
        public double OpenPrice { get; set; }
        decimal IOrderHistory.ClosePrice => (decimal) ClosePrice;
        public double ClosePrice { get; set; }
        decimal IOrderHistory.Volume => (decimal) Volume;
        public double Volume { get; set; }
        decimal IOrderHistory.MatchedVolume => (decimal) MatchedVolume;
        public double MatchedVolume { get; set; }
        decimal IOrderHistory.MatchedCloseVolume => (decimal) MatchedCloseVolume;
        public double MatchedCloseVolume { get; set; }
        decimal? IOrderHistory.TakeProfit => (decimal?) TakeProfit;
        public double? TakeProfit { get; set; }
        decimal? IOrderHistory.StopLoss => (decimal?) StopLoss;
        public double? StopLoss { get; set; }
        decimal IOrderHistory.Fpl => (decimal) Fpl;
        public double Fpl { get; set; }
        decimal IOrderHistory.PnL => (decimal) PnL;
        public double PnL { get; set; }
        decimal IOrderHistory.InterestRateSwap => (decimal) InterestRateSwap;
        public double InterestRateSwap { get; set; }
        decimal IOrderHistory.CommissionLot => (decimal) CommissionLot;
        public double CommissionLot { get; set; }
        decimal IOrderHistory.OpenCommission => (decimal) OpenCommission;
        public double OpenCommission { get; set; }
        decimal IOrderHistory.CloseCommission => (decimal) CloseCommission;
        public double CloseCommission { get; set; }
        decimal IOrderHistory.QuoteRate  => (decimal) QuoteRate;
        public double QuoteRate { get; set; }
        public int AssetAccuracy { get; set; }
        decimal IOrderHistory.MarginInit => (decimal) MarginInit;
        public double MarginInit { get; set; }
        decimal IOrderHistory.MarginMaintenance  => (decimal) MarginMaintenance;
        public double MarginMaintenance { get; set; }
        public DateTime? StartClosingDate { get; set; }
        public string Type { get; set; }
        OrderDirection IOrderHistory.Type => Type.ParseEnum(OrderDirection.Buy);
        public string Status { get; set; }
        OrderStatus IOrderHistory.Status => Status.ParseEnum(OrderStatus.Closed);
        public string CloseReason { get; set; }
        OrderCloseReason IOrderHistory.CloseReason => CloseReason.ParseEnum(OrderCloseReason.Close);
        public string FillType { get; set; }
        OrderFillType IOrderHistory.FillType => FillType.ParseEnum(OrderFillType.FillOrKill);
        public string RejectReason { get; set; }
        OrderRejectReason IOrderHistory.RejectReason => RejectReason.ParseEnum(OrderRejectReason.None);
        public string RejectReasonText { get; set; }
        public string Comment { get; set; }
        public List<MatchedOrder> MatchedOrders { get; set; } = new List<MatchedOrder>();
        public List<MatchedOrder> MatchedCloseOrders { get; set; } = new List<MatchedOrder>();
        decimal IOrderHistory.SwapCommission => (decimal) SwapCommission;
        public double SwapCommission { get; set; }
        
        public string EquivalentAsset { get; set; }
        decimal IOrderHistory.OpenPriceEquivalent => (decimal) OpenPriceEquivalent;
        public double OpenPriceEquivalent { get; set; }
        decimal IOrderHistory.ClosePriceEquivalent => (decimal) ClosePriceEquivalent;
        public double ClosePriceEquivalent { get; set; }

        public string Orders { get; set; }
        public string ClosedOrders { get; set; }
        
        OrderUpdateType IOrderHistory.OrderUpdateType => OrderUpdateType.ParseEnum(Backend.Core.OrderUpdateType.Close);
        
        public string OpenExternalOrderId { get; set; }
        public string OpenExternalProviderId { get; set; }
        public string CloseExternalOrderId { get; set; }
        public string CloseExternalProviderId { get; set; }
        public string MatchingEngineMode { get; set; }
        public string LegalEntity { get; set; }

        MatchingEngineMode IOrderHistory.MatchingEngineMode =>
            MatchingEngineMode.ParseEnum(Backend.Core.MatchingEngines.MatchingEngineMode.MarketMaker);
         
        public string OrderUpdateType { get; set; }

        public static string GeneratePartitionKey(string clientId, string accountIds)
        {
            return $"{clientId}_{accountIds}";
        }

        public static MarginTradingOrderHistoryEntity Create(IOrderHistory src)
        {
            return new MarginTradingOrderHistoryEntity
            {
                PartitionKey = GeneratePartitionKey(src.ClientId, src.AccountId),
                Id = src.Id,
                Code = src.Code,
                ClientId = src.ClientId,
                AccountId = src.AccountId,
                TradingConditionId = src.TradingConditionId,
                AccountAssetId = src.AccountAssetId,
                Instrument = src.Instrument,
                Type = src.Type.ToString(),
                CreateDate = src.CreateDate,
                OpenDate = src.OpenDate,
                CloseDate = src.CloseDate,
                ExpectedOpenPrice = (double?) src.ExpectedOpenPrice,
                OpenPrice = (double) src.OpenPrice,
                ClosePrice = (double) src.ClosePrice,
                TakeProfit = (double?) src.TakeProfit,
                StopLoss = (double?) src.StopLoss,
                Fpl = (double) src.Fpl,
                PnL = (double) src.PnL,
                InterestRateSwap = (double) src.InterestRateSwap,
                CommissionLot = (double) src.CommissionLot,
                OpenCommission = (double) src.OpenCommission,
                CloseCommission = (double) src.CloseCommission,
                QuoteRate = (double) src.QuoteRate,
                AssetAccuracy = src.AssetAccuracy,
                MarginInit = (double) src.MarginInit,
                MarginMaintenance = (double) src.MarginMaintenance,
                StartClosingDate = src.StartClosingDate,
                Status = src.Status.ToString(),
                CloseReason = src.CloseReason.ToString(),
                FillType = src.FillType.ToString(),
                Volume = (double) src.Volume,
                MatchedVolume = (double) src.MatchedVolume,
                MatchedCloseVolume = (double) src.MatchedCloseVolume,
                RejectReason = src.RejectReason.ToString(),
                RejectReasonText = src.RejectReasonText,
                Orders = src.MatchedOrders.SerializeArrayForTableStorage(),
                ClosedOrders = src.MatchedCloseOrders.SerializeArrayForTableStorage(),
                SwapCommission = (double) src.SwapCommission,
                EquivalentAsset = src.EquivalentAsset,
                OpenPriceEquivalent = (double) src.OpenPriceEquivalent,
                ClosePriceEquivalent = (double) src.ClosePriceEquivalent,
                Comment = src.Comment,
                OrderUpdateType = src.OrderUpdateType.ToString(),
                OpenExternalOrderId = src.OpenExternalOrderId,
                OpenExternalProviderId = src.OpenExternalProviderId,
                CloseExternalOrderId = src.CloseExternalOrderId,
                CloseExternalProviderId = src.CloseExternalProviderId,
                MatchingEngineMode = src.MatchingEngineMode.ToString(),
                LegalEntity = src.LegalEntity,
            };
        }
    }

    public class MarginTradingOrdersHistoryRepository : IMarginTradingOrdersHistoryRepository
    {
        private readonly INoSQLTableStorage<MarginTradingOrderHistoryEntity> _tableStorage;

        public MarginTradingOrdersHistoryRepository(INoSQLTableStorage<MarginTradingOrderHistoryEntity> tableStorage)
        {
            _tableStorage = tableStorage;
        }

        public Task AddAsync(IOrderHistory order)
        {
            var entity = MarginTradingOrderHistoryEntity.Create(order);
            // ReSharper disable once RedundantArgumentDefaultValue
            return _tableStorage.InsertAndGenerateRowKeyAsDateTimeAsync(entity, entity.CloseDate ?? entity.OpenDate ?? entity.CreateDate, RowKeyDateTimeFormat.Iso);
        }

        public async Task<IReadOnlyList<IOrderHistory>> GetHistoryAsync(string clientId, string[] accountIds, DateTime? from, DateTime? to)
        {
            return (await _tableStorage.WhereAsync(accountIds.Select(a => clientId + '_' + a),
                    from ?? DateTime.MinValue, to?.Date.AddDays(1) ?? DateTime.MaxValue, ToIntervalOption.IncludeTo))
                .OrderByDescending(entity => entity.CloseDate ?? entity.OpenDate ?? entity.CreateDate).ToList();
        }

        public async Task<IEnumerable<IOrderHistory>> GetHistoryAsync()
        {
            var entities = (await _tableStorage.GetDataAsync()).OrderByDescending(item => item.Timestamp);

            return entities;
        }
    }
}
