using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Reponse;

namespace FULFILLMENT_H4U.Repositories.Interfaces
{
    public interface ISysAccessLogRepository
    {
        Task<Response> INSERT(SysAccess param);

    }
}
