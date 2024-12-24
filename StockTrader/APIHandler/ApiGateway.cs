using StockTrader.APIHandler.Interfaces;
using StockTrader.Models;
using StockTrader.Models.ModelBuilders;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace StockTrader.APIHandler
{
    public class ApiGateway : IApiGateway
    {
        private readonly IGetRequestHandler _getRequestHandler;
        private readonly IPurchaseViewModelBuilder _modelBuilder;
        private readonly IPutRequestHandler _putRequestHandler;
        private readonly IPostRequestHandler _postRequestHandler;
        private readonly IDeleteHandler _deleteHandler;

        public ApiGateway(IGetRequestHandler getRequestHandler, IPurchaseViewModelBuilder modelBuilder,
            IPutRequestHandler putRequestHandler, IPostRequestHandler postRequestHandler,
            IDeleteHandler deleteHandler)
        {
            _getRequestHandler = getRequestHandler;
            _modelBuilder = modelBuilder;
            _putRequestHandler = putRequestHandler;
            _postRequestHandler = postRequestHandler;
            _deleteHandler = deleteHandler;
        }

        public async Task<string> PurchaseOnStock(string tradeId, int purchasePrice, string accessToken)
        {
            var purchaseResponse = await _putRequestHandler.PutPurchaseOnStock(tradeId, purchasePrice, accessToken);
            return purchaseResponse;
        }

        public async Task<string> CheckToken(string accessToken)
        {
            var response = await _getRequestHandler.CheckToken(accessToken);
            return response;
        }

        public async Task ClearExpiredStocks(string accessToken, List<PurchaseViewModel> allStocks)
        {
            await _deleteHandler.DeleteExpiredStocks(accessToken, allStocks);
        }

        public async Task<List<PurchaseViewModel>> FetchStocks(int stockId, int purchasePrice, string accessToken)
        {
            var searchList = await _getRequestHandler.SearchForSpecificStock(stockId, purchasePrice, accessToken);
            var defaultValue = new List<PurchaseViewModel> {
                new PurchaseViewModel
                {
                    Status = "expired",
                    TimeRemaining = -1,
                    PurchasePrice = 0,
                    Pending = false,
                    TradeId = "No Stocks Found"
                }
            };

            return searchList.Count == 0 ? defaultValue : searchList;
        }

        public async Task<List<PurchaseViewModel>> FetchStocksByLeague(int leagueId, int rarityId, int purchasePrice,
            string accessToken, string positionId, int nationId)
        {
            var searchList = await _getRequestHandler.SearchForLeagueRarityStocks(leagueId, rarityId, purchasePrice, accessToken,
                positionId, nationId);
            var defaultValue = new List<PurchaseViewModel> {
                new PurchaseViewModel
                {
                    Status = "expired",
                    TimeRemaining = -1,
                    PurchasePrice = 0,
                    Pending = false,
                    TradeId = "No Stocks Found"
                }
            };

            return searchList.Count == 0 ? defaultValue : searchList;
        }

        public async Task<SymbolSearchModel> GetTransferList(string accessToken)
        {
            try
            {
                var transferList = await _getRequestHandler.GetTransferList(accessToken);
                if (transferList.SymbolInfo?.Count == 0 || transferList.SymbolInfo == null)
                {
                    throw new Exception();
                }

                transferList.SymbolInfo = _modelBuilder.PopulateDefaultFieldsOfPurchaseViews(transferList.SymbolInfo);
                return transferList;
            }
            catch (Exception ex)
            {
                return new SymbolSearchModel
                {
                    SymbolInfo = new List<PurchaseViewModel>
                    {
                        new PurchaseViewModel
                        {
                            Status= "expired",
                            Pending = false
                        }
                    }
                };
            }
        }

        public async Task<SymbolSearchModel> GetSellStocks(string accessToken)
        {
            try
            {
                var targetsList = await _getRequestHandler.GetSellStocks(accessToken);
                if (targetsList.SymbolInfo?.Count == 0 || targetsList.SymbolInfo == null)
                {
                    throw new Exception();
                }

                targetsList.SymbolInfo = _modelBuilder.PopulateDefaultFieldsOfPurchaseViews(targetsList.SymbolInfo);
                return targetsList;
            }
            catch (Exception)
            {
                return new SymbolSearchModel
                {
                    SymbolInfo = new List<PurchaseViewModel>
                    {
                        new PurchaseViewModel
                        {
                            Status= "expired",
                            Pending = false
                        }
                    }
                };
            }
        }

        public async Task<SymbolSearchModel> GetUnassignedPile(string accessToken)
        {
            try
            {
                var targetsList = await _getRequestHandler.GetUnassignedPile(accessToken);
                if (targetsList.SymbolInfo?.Count == 0 || targetsList.SymbolInfo == null)
                {
                    throw new Exception();
                }

                targetsList.SymbolInfo = _modelBuilder.PopulateDefaultFieldsOfPurchaseViews(targetsList.SymbolInfo);
                return targetsList;
            }
            catch (Exception)
            {
                return new SymbolSearchModel
                {
                    SymbolInfo = new List<PurchaseViewModel>
                    {
                        new PurchaseViewModel
                        {
                            Status= "expired",
                            Pending = false
                        }
                    }
                };
            }
        }

        public async Task<string> SellStock(string tradeId, string stockId, string accessToken, int startPrice, int BinPrice)
        {
            var moveCode = await _putRequestHandler.MoveStockToTradePile(tradeId, stockId, accessToken);
            if (moveCode == HttpStatusCode.OK)
            {
                //Sell stock
                var response = await _postRequestHandler.SellStock(stockId, accessToken, startPrice, BinPrice);

                return response;
            }

            return "Error";
        }
    }
}
