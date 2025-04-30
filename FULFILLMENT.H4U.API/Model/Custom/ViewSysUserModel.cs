using FULFILLMENT.H4U.API.Model;

namespace FULFILLMENT.H4U.API.Model.Custom
{
    public class ViewSysUserModel : SysUser
    {
        #region Vendor Master

        public string? VendorName { get; set; }

        public string? ContactName { get; set; }

        public string? ContactTel { get; set; }

        public string? ContactAddress { get; set; }

        public string? LineId { get; set; }

        public string? Website { get; set; }

        public string? vendorEmail { get; set; }
        public float? vendorBalance { get; set; }

        #endregion

        public List<SysMenuGroup> groupAccess { get; set; }
        public List<SysMenuList> menuAccess { get; set; }

        public List<ViewSysRoleListModel> roleAccess { get; set; }

        public ViewSysUserModel()
        {
            groupAccess = new List<SysMenuGroup>();

            menuAccess = new List<SysMenuList>();

            roleAccess = new List<ViewSysRoleListModel>();
        }
    }
}
