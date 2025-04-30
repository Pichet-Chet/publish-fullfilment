using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;

namespace FULFILLMENT_H4U.Repositories.Interfaces
{
    public interface IMasterLocationRepository
    {
        public Task<Response> GET_ALL(FilterModel param);
        public Task<Response> GET_TYPE();
        public Task<Response> GET_ACTIVE();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(MasterLocation param);
        Task<Response> UPDATE(MasterLocation param);
        Task<Response> DELETE(int id);
    }
}
