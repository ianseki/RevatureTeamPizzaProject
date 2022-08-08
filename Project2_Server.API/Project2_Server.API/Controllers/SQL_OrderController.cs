using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project2_Server.Data;
using Project2_Server.Model;

namespace Project2_Server.API.Controllers
{
    [Route("api/SQL_Order")]
    [ApiController]
    public class SQL_OrderController : ControllerBase
    {
        private readonly SQL_Order _repo;

        public SQL_OrderController(SQL_Order repo) => _repo = repo;

        [HttpGet]
        public async Task<IEnumerable<DMODEL_Order>> Get()
            => await _repo.DMODEL_Order.ToListAsync();

        [HttpGet("order_id")]
        [ProducesResponseType(typeof(DMODEL_Order), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int order_id)
        {
            var Order = await _repo.DMODEL_Order.FindAsync(order_id);
            return Order == null ? NotFound() : Ok(Order);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(DMODEL_Order Order)
        {
            await _repo.DMODEL_Order.AddAsync(Order);
            await _repo.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = Order.order_id }, Order);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int order_id, DMODEL_Order Order)
        {
            if (order_id != Order.order_id) return BadRequest();

            _repo.Entry(Order).State = EntityState.Modified;
            await _repo.SaveChangesAsync();

            return NoContent();


        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int Order_id)
        {
            var OrderToDelete = await _repo.DMODEL_Order.FindAsync(Order_id);
            if (OrderToDelete == null) return NotFound();

            _repo.DMODEL_Order.Remove(OrderToDelete);
            await _repo.SaveChangesAsync();

            return NoContent();
        }


    }
}
