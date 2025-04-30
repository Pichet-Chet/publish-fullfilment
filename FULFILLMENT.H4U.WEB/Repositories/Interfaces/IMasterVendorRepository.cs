using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;

namespace FULFILLMENT_H4U.Repositories.Interfaces
{
    public interface IMasterVendorRepository
    {
        public Task<Response> GET_ALL(FilterModel param);
        public Task<Response> GET_BALANCE_MOVEMENT(FilterModel param);
        public Task<Response> GET_ACTIVE();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(MasterVendor param);
        Task<Response> UPDATE(MasterVendor param);
        Task<Response> DELETE(int id);
    }
}
