using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using CoffeeManager.Api.Mappers;
using CoffeeManager.Models;
using CoffeeManager.Models.Data.DTO.Category;
using CoffeeManager.Models.Data.DTO.User;

namespace CoffeeManager.Api.Controllers
{
    [Authorize]
    public class CategoriesController : ApiController
    {
        [Route(RoutesConstants.GetCategories)]
        [HttpGet]
        public HttpResponseMessage GetCategories([FromUri]int coffeeroomno)
        {
            var categories = new  CoffeeRoomEntities().Categories.ToArray().Select(u => u.ToDTO());
            return new HttpResponseMessage() { Content = new ObjectContent<IEnumerable<CategoryDTO>>(categories, new JsonMediaTypeFormatter())};
        }
        
        [Route(RoutesConstants.GetCategory)]
        [HttpGet]
        public HttpResponseMessage GetCategory([FromUri]int coffeeroomno, [FromUri]int categoryId)
        {
            var category = new CoffeeRoomEntities().Categories.FirstOrDefault(c => c.Id = categoryId);
            return new HttpResponseMessage() { Content = new ObjectContent<CategoryDTO>(category.ToDTO(), new JsonMediaTypeFormatter())};
        }
        
        [Route(RoutesConstants.AddCategory)]
        [HttpPut]
        public HttpResponseMessage AddCategory([FromUri]int coffeeroomno, [FromBody]CategoryDTO category)
        {
            var entites = new  CoffeeRoomEntities();
            var categoryDb = category.Map();
            entites.Categories.Add(categoryDb);
            entites.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, categoryDb.Id);
        }
        
        [Route(RoutesConstants.UpdateCategory)]
        [HttpPost]
        public HttpResponseMessage UpdateCategory([FromUri]int coffeeroomno, [FromBody]CategoryDTO category)
        {
            var entites = new CoffeeRoomEntities();
            var categoryDb = entites.Categories.First(u => u.Id == category.Id);
            categoryDb =  DbMapper.Update(category, categoryDb);
            entites.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        
        [Route(RoutesConstants.DeleteCategory)]
        [HttpPost]
        public HttpResponseMessage DeleteCategory([FromUri]int coffeeroomno,[FromUri]int categoryId)
        {
            var entites = new CoffeeRoomEntities();
            var categoryDb = entites.Categories.First(u => u.Id == categoryId);
            entites.Categories.Remove(categoryDb);
            entites.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}