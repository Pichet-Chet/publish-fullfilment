using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;

namespace FULFILLMENT_H4U.Repositories.Interfaces
{
    public interface IMasterBinRepository
    {
        public Task<Response> GET_ALL(FilterModel param);
        public Task<Response> GET_ACTIVE();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(MasterBin param);
        Task<Response> UPDATE(MasterBin param);
        Task<Response> DELETE(int id);

        public Task<Response> GET_STATUS();
        public Task<Response> GET_TYPE();

    }
}
