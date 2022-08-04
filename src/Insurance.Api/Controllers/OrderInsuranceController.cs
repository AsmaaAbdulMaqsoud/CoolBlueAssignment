using Insurance.Application.Common.Interfaces.Services;
using Insurance.Application.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Insurance.Api.Controllers;

public class OrderInsuranceController : Controller
{
    private readonly IOrderInsuranceService _orderInsuranceService;

    public OrderInsuranceController(IOrderInsuranceService orderInsuranceService)
    {
        _orderInsuranceService = orderInsuranceService;
    }

    [HttpPost]
    [Route("api/insurance/order")]
    public async Task<ActionResult<decimal>> CalculateOrderInsurance([FromBody] OrderDto order)
    {
        if (order == null || order.Products.Count == 0)
            return BadRequest("No products found!");

        if (order.Products.Any(x => x.Quantity == 0))
            return BadRequest("Can't insert product with quantity of zero!");
        if (order.Products.Any(x => x.ProductId == 0))
            return BadRequest("can't accept product Id to be equal zero!");

        return await _orderInsuranceService.GetOrderInsurance(order);

    }
}
