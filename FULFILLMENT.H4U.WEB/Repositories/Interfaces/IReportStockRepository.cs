using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;

namespace FULFILLMENT_H4U.Repositories.Interfaces
{
    public interface IReportStockRepository
    {
        public Task<Response> GET_ALL(FilterModel param);
        Task<Response> GET_DETAIL(int id);
        Task<Response> GET_STOCK_BALANCE_ITEM(int productId);
        public Task<Response> GET_STOCK_MOVE_MENT(FilterModel param);

    }
}
