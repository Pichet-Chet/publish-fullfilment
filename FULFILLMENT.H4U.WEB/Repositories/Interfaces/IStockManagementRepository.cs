using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;

namespace FULFILLMENT_H4U.Repositories.Interfaces
{
    public interface IStockManagementRepository
    {
        public Task<Response> GET_ALL(FilterModel param);
        Task<Response> GET_DETAIL(int id);

        Task<Response> UPDATE(StockManualLog param);

    }
}
