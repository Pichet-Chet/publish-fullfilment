using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;

namespace FULFILLMENT_H4U.Repositories.Interfaces
{
    public interface IMasterProductRepository
    {
        public Task<Response> GET_ALL(FilterModel param);
        public Task<Response> GET_ACTIVE();
        public Task<Response> GET_DETAIL(int id);
        public Task<Response> INSERT(MasterProduct param);
        public Task<Response> UPDATE(MasterProduct param);
        public Task<Response> DELETE(int id);
    }
}
