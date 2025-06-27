namespace MicroApi.Models
{
    public class UserMenuPermission
    {
        public bool CanAdd { get; set; }
        public bool CanView { get; set; }
        public bool CanEdit { get; set; }
        public bool CanApprove { get; set; }
        public bool CanDelete { get; set; }
        public bool CanPrint { get; set; }
    }
    public class UserMenuPermissionRequest
    {
        public int UserId { get; set; }
        public int MenuId { get; set; }
    }
}
