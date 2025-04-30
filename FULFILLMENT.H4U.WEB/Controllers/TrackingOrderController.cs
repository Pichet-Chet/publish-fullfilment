using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;
using FULFILLMENT_H4U.Repositories;
using FULFILLMENT_H4U.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FULFILLMENT_H4U.Controllers
{
    public class TrackingOrderController : Controller
    {
        ITrackingOrderRepository _repoCollection;

        public TrackingOrderController(IConfiguration configuration)
        {
            _repoCollection = new TrackingOrderRepository(configuration);
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<JsonResult> GET_ALL([FromQuery] FilterModel param)
        {
            Response resp = new Response();

            try
            {
                resp = await Task.Run(() => _repoCollection.GET_ALL(param));
            }
            catch (Exception ex)
            {
                resp.status = false;
                resp.message = ex.Message;
            }

            return new JsonResult(resp);
        }

    }
}
