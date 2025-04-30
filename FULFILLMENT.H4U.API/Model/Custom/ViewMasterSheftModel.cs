
using FULFILLMENT.H4U.API.Model;
namespace FULFILLMENT.H4U.API.Model.Custom
{
    public class ViewMasterSheftModel : MasterSheft
    {
        public string? LocationName { get; set; }

        public string? LocationDescription { get; set; }

        public string? LocationType { get; set; }
    }
}
