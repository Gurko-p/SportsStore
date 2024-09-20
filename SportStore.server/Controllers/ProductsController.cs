using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportStore.server.Data.Infrastructure;
using SportStore.server.Data.Models;

namespace SportStore.server.Controllers;

[Route("api/products")]
[ApiController]
public class ProductsController(DataManager dataManager) : ControllerBase
{

    [HttpGet]
    [Route("list")]
    //[Authorize]
    public async Task<IActionResult> Products()
    {
        var products = 
            await dataManager.Products.Query()
                .Include(x => x.Category)
                .ToListAsync();
        return Ok(products);
    }

    [HttpGet]
    [Route("item/{id}")]
    public async Task<IActionResult> Get(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Id должен быть больше 0");
        }
        var product = await dataManager.Products.FirstOrDefaultAsync(id);
        if (product is null)
        {
            return NotFound($"Продукт с id={id} не найден.");
        }
        return Ok(product);
    }

    [HttpPost]
    [Route("create")]
    //[Authorize(Roles = "admin")]
    public async Task<IActionResult> Create([FromBody] Product product)
    {
        await dataManager.Products.CreateAsync(product);
        return CreatedAtAction(nameof(Create), new { id = product.Id }, product);
    }

    [HttpPut]
    [Route("update/{id}")]
    //[Authorize(Roles = "admin")]
    public async Task<IActionResult> Update(int id, [FromBody] Product product)
    {
        if (id != product.Id)
        {
            return BadRequest();
        }
        await dataManager.Products.UpdateAsync(product);
        return NoContent();
    }

    [HttpDelete]
    [Route("remove/{id}")]
    //[Authorize(Roles = "admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await dataManager.Products.DeleteAsync(id);
        return NoContent();
    }
}