using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Reponse;

namespace FULFILLMENT.H4U.API.Repository.Interface
{
    public interface ISysAccessLogRepository
    {
        Task<Response> INSERT(SysAccess param);


    }
}
