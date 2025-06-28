using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroApi.Models
{
    public class UserRole
    {
        public int? ID { get; set; }
        public string UserRoles { get; set; }
        public bool? IsInactive { get; set; }
        public DateTime? LastModifiedTime { get; set; }
        public DateTime? CreateTime { get; set; }
        public bool CanAdd { get; set; }
        public bool CanView { get; set; }
        public  bool CanEdit { get; set; }
        public bool CanPrint { get; set; }
        public bool CanDelete { get; set; }
        public List<UserMenuList> usermenulist { get; set; }
    }
    public class UserRoleResponse
    {
        public int flag { get; set; }
        public string message { get; set; }
        public List<UserRole> data { get; set; }
       
    } 
    public class UserMenuGroup
    {
        public int MenuGroupId { get; set; }
        public string text { get; set; }
        public string icon { get; set; }
        public string MenuGroupOrder { get; set; }
        public List<UserMenuList> Menus { get; set; }
    }
    public class UserMenuList
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuOrder { get; set; }
        public bool? Selected { get; set; }
        public bool CanAdd { get; set; }
        public bool CanView { get; set; }
        public bool CanPrint { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
    }
    public class UserMenuResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public string ID { get; set; }
        public string UserRoles { get; set; }
        public string IsInactive { get; set; }
        public List<UserMenuGroup> Data { get; set; }
    }


}
