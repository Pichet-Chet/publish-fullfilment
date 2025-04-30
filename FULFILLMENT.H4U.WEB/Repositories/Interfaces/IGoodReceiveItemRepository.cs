using System;
using FULFILLMENT.H4U.API.Model.Reponse;

namespace FULFILLMENT_H4U.Repositories.Interfaces
{
	public interface IGoodReceiveItemRepository
	{
        Task<Response> GET_ALL_BY_HEADER(int headerId);

        Task<Response> GET_DETAIL(int id);
    }
}

