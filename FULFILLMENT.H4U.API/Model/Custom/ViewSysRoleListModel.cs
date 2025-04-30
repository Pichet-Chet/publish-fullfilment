namespace FULFILLMENT.H4U.API.Model.Custom
{
    public class ViewSysRoleListModel : SysRoleList
    {
        public string? roleGroupName { get; set; }
        public string? menuGroupName { get; set; }
        public int? menuGroupSeq { get; set; }
        public string? menuName { get; set; }
        public int? menuSeq { get; set; }
    }
}
