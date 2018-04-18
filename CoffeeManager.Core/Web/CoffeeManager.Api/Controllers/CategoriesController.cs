﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;
using CoffeeManager.Api.Mappers;
using CoffeeManager.Models;
using CoffeeManager.Models.Data.DTO.Category;

namespace CoffeeManager.Api.Controllers
{
    [Authorize]
    public class CategoriesController : ApiController
    {
        [Route(RoutesConstants.GetCategories)]
        [HttpGet]
        public HttpResponseMessage GetCategories([FromUri]int coffeeroomno)
        {
            var categories = new  CoffeeRoomEntities().Categories.Include(i => i.EnabledCategories).ToArray().Select(u => u.ToDTO());
            return new HttpResponseMessage() { Content = new ObjectContent<IEnumerable<CategoryDTO>>(categories, new JsonMediaTypeFormatter())};
        }
        
        [Route(RoutesConstants.GetCategory)]
        [HttpGet]
        public HttpResponseMessage GetCategory([FromUri]int coffeeroomno, [FromUri]int categoryId)
        {
            var ctx = new CoffeeRoomEntities();
            var categoryDb = ctx.Categories.Include(i => i.EnabledCategories).FirstOrDefault(c => c.Id == categoryId);
            var category = categoryDb.ToDTO();
            var subs = ctx.Categories.Where(c => c.ParentId != null && c.ParentId.Value == categoryId).ToList().Select(s => s.ToDTO());
            category.SubCategories = subs.ToArray();
            return new HttpResponseMessage() { Content = new ObjectContent<CategoryDTO>(category, new JsonMediaTypeFormatter())};
        }
        
        [Route(RoutesConstants.AddCategory)]
        [HttpPut]
        public HttpResponseMessage AddCategory([FromUri]int coffeeroomno, [FromBody]CategoryDTO category)
        {
            var entites = new  CoffeeRoomEntities();
            var categoryDb = category.Map();
            entites.Categories.Add(categoryDb);
            entites.SaveChanges();
            foreach (var cr in entites.CoffeeRooms)
            {
                entites.EnabledCategories.Add(new EnabledCategory()
                {
                    CategoryId = categoryDb.Id,
                    CoffeeRoomNo = cr.Id,
                    IsEnabled = true
                });
                entites.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.OK, categoryDb.Id);
        }
        
        [Route(RoutesConstants.UpdateCategory)]
        [HttpPost]
        public HttpResponseMessage UpdateCategory([FromUri]int coffeeroomno, [FromBody]CategoryDTO category)
        {
            var entites = new CoffeeRoomEntities();
            var categoryDb = entites.Categories.First(u => u.Id == category.Id);
            DbMapper.Update(category, categoryDb);
           
            var newSubsIds = category.SubCategories?.Select(s => s.Id);
            if (newSubsIds == null || !newSubsIds.Any())
            {
                var subs = entites.Categories.Where(s => s.ParentId != null && s.ParentId == category.Id);
                foreach (var sub in subs)
                {
                    sub.ParentId = null;
                }
            }
            else
            {
                foreach (var subCategory in entites.Categories.Where(c => c.ParentId != null && c.ParentId.Value == category.Id))
                {
                    if (!newSubsIds.Contains(subCategory.Id))
                    {
                        subCategory.ParentId = null;
                    }
                }
                foreach (var cat in entites.Categories)
                {
                    if (newSubsIds.Contains(cat.Id))
                    {
                        cat.ParentId = category.Id;
                    }
                }
            }

            entites.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        
        [Route(RoutesConstants.DeleteCategory)]
        [HttpDelete]
        public HttpResponseMessage DeleteCategory([FromUri]int coffeeroomno,[FromUri]int categoryId)
        {
            var entites = new CoffeeRoomEntities();
            var categoryDb = entites.Categories.First(u => u.Id == categoryId);
            entites.Categories.Remove(categoryDb);
            entites.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route(RoutesConstants.ToggleCategoryEnabled)]
        [HttpPost]
        public async Task<HttpResponseMessage> ToggleCategoryEnabled([FromUri]int coffeeroomno, int id, HttpRequestMessage message)
        {
            var entities = new CoffeeRoomEntities();
            var category = entities.Categories
                .Include(c => c.CoffeeCounterForCoffeeRooms)
                .Include(s => s.CoffeeCounterForCoffeeRooms
                .Select(cc => cc.EnabledCoffeeCounters))
                .FirstOrDefault(t => t.Id == id);
            if (category != null)
            {
                var isEnabledDb =
                    entities.EnabledCategories.FirstOrDefault(c =>
                        c.CategoryId == id && c.CoffeeRoomNo == coffeeroomno);
                if (isEnabledDb == null)
                {
                    isEnabledDb = new EnabledCategory()
                    {
                        CategoryId = id,
                        CoffeeRoomNo = coffeeroomno
                    };
                    entities.EnabledCategories.Add(isEnabledDb);
                }
                isEnabledDb.IsEnabled = !isEnabledDb.IsEnabled;

                var counter = category.CoffeeCounterForCoffeeRooms.FirstOrDefault();
                if (counter != null)
                {
                    var enabledDb = counter.EnabledCoffeeCounters.FirstOrDefault(c => c.CoffeeRoomNo == coffeeroomno);
                    if (enabledDb != null)
                    {
                        enabledDb.IsEnabled = isEnabledDb.IsEnabled;
                    }
                }
                entities.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}