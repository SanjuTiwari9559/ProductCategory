using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCategory.Model;
using System.Runtime.InteropServices;

namespace ProductCategory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AplicationDbContext aplicationDb;

        public CategoryController(AplicationDbContext aplicationDb)
        {
            this.aplicationDb = aplicationDb;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> getCategoryes()
        {
            return await aplicationDb.Categories1
                                           .Include(c => c.Products) // Include related Products
                                           .ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> getCategory(int id)
        {
               var category= await aplicationDb.Categories1.FindAsync(id);
            if(category == null)
            {
                return NotFound();
            }
            return category;
        }
        [HttpPost]
        public async Task<ActionResult<Responce>>createCategory(Category category)
        {
             await aplicationDb.Categories1.AddAsync(category);
            aplicationDb.SaveChangesAsync();
            return new Responce { message = "Add Succesfully" };
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Responce>>UpdateCategory(int id,Category category)
        {
            var existingcat=await aplicationDb.Categories1.FindAsync(id);
            if(existingcat!=null)
            {
                 existingcat.CategoryName=category.CategoryName;
                existingcat.Products=category.Products;

              await aplicationDb.SaveChangesAsync();
                return new Responce { message = "Updated Succesfully" };
            }
            return new Responce
            {
                message = "No Data Found"
            };
        }


    }
}
