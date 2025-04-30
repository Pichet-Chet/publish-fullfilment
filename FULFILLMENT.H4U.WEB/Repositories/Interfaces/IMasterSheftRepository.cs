using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;
namespace FULFILLMENT_H4U.Repositories.Interfaces
{
    public interface IMasterSheftRepository
    {
        public Task<Response> GET_ALL(FilterModel param);
        public Task<Response> GET_ACTIVE();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(MasterSheft param);
        Task<Response> UPDATE(MasterSheft param);
        Task<Response> DELETE(int id);
    }
}
