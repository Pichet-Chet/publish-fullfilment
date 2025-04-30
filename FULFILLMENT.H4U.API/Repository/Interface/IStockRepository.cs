using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;

namespace FULFILLMENT.H4U.API.Repository.Interface
{
    public interface IStockRepository
    {
        Task<Response> GET_ALL(FilterModel param);
        Task<Response> GET_STOCK_MOVE_MENT(FilterModel param);
        Task<Response> GET_DETAIL(int id);
        Task<Response> GET_STOCK_BALANCE_ITEM(int productId);
        Task<Response> UPDATE(StockManualLog param);

    }
}
