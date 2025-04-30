namespace FULFILLMENT.H4U.API.Model.Custom
{
    public class ViewSysRoleModel
    {
        public List<ViewSysRoleGroupModel> viewSysRoleGroupModel { get; set; }
        public List<ViewSysRoleListModel> viewSysRoleListModel { get; set; }

        public ViewSysRoleModel()
        {
            viewSysRoleGroupModel = new List<ViewSysRoleGroupModel>();
            viewSysRoleListModel = new List<ViewSysRoleListModel>();
        }

    }
}
