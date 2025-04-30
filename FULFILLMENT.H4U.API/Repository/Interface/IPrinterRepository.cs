using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;
using FULFILLMENT.H4U.API.Model.Transaction;

namespace FULFILLMENT.H4U.API.Repository.Interface
{
    public interface IPrinterRepository
    {
        Task<Response> GET_ALL(FilterModel param);

    }
}
