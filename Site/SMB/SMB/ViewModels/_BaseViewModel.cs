using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Helpers;
using Models;

namespace ViewModels
{

    public class _BaseViewModel
    {
        private BaseViewModelHelper baseviewmodel = new BaseViewModelHelper();

        //public List<Service> MenuItems { get { return baseviewmodel.GetMenuService(); } }
        public List<ServiceMenuItem> ServiceMenuItems { get { return baseviewmodel.GetMenuServiceItems(); } }
        
        public string FooterAddress { get { return baseviewmodel.GetTextItemByName("footeraddress"); } }
        public string FooterPhone { get { return baseviewmodel.GetTextItemByName("footerphone"); } }
        public string FooterEmail { get { return baseviewmodel.GetTextItemByName("footeremail"); } }
        public string FooterAbout { get { return baseviewmodel.GetTextItemByName("footerabout"); } }
        public List<Blog> FooterBlogs { get { return baseviewmodel.GetFooterBlogs(); } }
        public List<BlogGroup> SidebarBlogGroups { get { return baseviewmodel.GetSidebarBlogGroups(); } }
    }

    public class ServiceMenuItem
    {
        public ServiceGroup ServiceGroup { get; set; }
        public List<Service> Services { get; set; }
    }
}