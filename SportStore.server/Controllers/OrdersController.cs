using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportStore.server.Data.Infrastructure;
using SportStore.server.Data.Models;

namespace SportStore.server.Controllers;

[Route("api/orders")]
[ApiController]
//[Authorize(Roles = "admin")]
public class OrdersController(DataManager dataManager) : ControllerBase
{

    [HttpGet]
    [Route("list")]
    public async Task<IActionResult> Orders()
    {
        var orders = 
            await dataManager.Orders.Query().AsNoTracking().ToListAsync();
        return Ok(orders);
    }

    [HttpGet]
    [Route("item/{id}")]
    public async Task<IActionResult> Get(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Id должен быть больше 0");
        }
        var product = await dataManager.Orders.FirstOrDefaultAsync(id);
        if (product is null)
        {
            return NotFound($"Заказ с id={id} не найден.");
        }
        return Ok(product);
    }

    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> Create([FromBody] Order order)
    {
        await dataManager.Orders.CreateAsync(order);
        return CreatedAtAction(nameof(Create), new { id = order.Id }, order);
    }

    [HttpPut]
    [Route("update/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Order order)
    {
        if (id != order.Id)
        {
            return BadRequest();
        }
        await dataManager.Orders.UpdateAsync(order);
        return NoContent();
    }

    [HttpDelete]
    [Route("remove/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await dataManager.Orders.DeleteAsync(id);
        return NoContent();
    }
}