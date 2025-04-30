using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;
namespace FULFILLMENT.H4U.API.Repository.Interface
{
    public interface IMasterSheftRepository
    {
        Task<Response> GET_ALL();
        Task<Response> GET_ACTIVE();
        Task<Response> GET_DETAIL(int id);
        Task<Response> GET_FILTER(FilterModel param);
        Task<Response> SEARCH(string textSearch);
        Task<Response> INSERT(MasterSheft param);
        Task<Response> UPDATE(MasterSheft param);
        Task<Response> DELETE(int id);
    }
}
