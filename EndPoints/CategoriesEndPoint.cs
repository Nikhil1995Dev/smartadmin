using BITS.SmartAdmin.WebUI.Extensions.UIExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartAdmin.WebUI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartAdmin.WebUI.EndPoints
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesEndPoint : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public CategoriesEndPoint(ApplicationDbContext context)
        {
            this.context = context;
        }
        [Route("getparent")]
        public ActionResult<List<JsTreeModel>> GetParent()
        {
            List<JsTreeModel> treeItems = new List<JsTreeModel>();
            var categories = context.Categories.AsQueryable();
            var parantcategories = context.Categories.Where(p => p.ParentId == 0 && p.IsActive == true).ToList();
            foreach (var item in parantcategories)
            {
                treeItems.Add(new JsTreeModel()
                {
                    id = item.Id.ToString(),
                    parent = "#",
                    children = categories.Any(c=>c.ParentId==item.Id && c.IsActive == true) ? true : false,
                    text = item.CategoryName,
                    type = "folder"
                }); 
            }
            return Ok(treeItems);
        }
        [Route("getchildren/{parentId}")]
        public ActionResult<List<JsTreeModel>> GetChildren(long  parentId)
        {
            List<JsTreeModel> treeItems = new List<JsTreeModel>();
            var categories = context.Categories.AsQueryable();
            var parantcategories = context.Categories.Where(p => p.ParentId == parentId && p.IsActive == true).ToList();
            foreach (var item in parantcategories)
            {
                treeItems.Add(new JsTreeModel()
                {
                    id = item.Id.ToString(),
                    parent = parentId.ToString(),
                    children = categories.Any(c => c.ParentId == item.Id && c.IsActive == true) ? true : false,
                    text = item.CategoryName,
                    type = "folder"
                });
            }
            return Ok(treeItems);
        }
    }
}
