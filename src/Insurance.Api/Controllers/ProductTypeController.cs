using Insurance.Application.Common.Interfaces.Services;
using Insurance.Application.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Insurance.Api.Controllers;

public class ProductTypeController : Controller
{
    readonly IProductTypeService _productTypeService;
    public ProductTypeController(IProductTypeService productTypeService)
    {
        _productTypeService = productTypeService;
    }

    [HttpPost]
    [Route("api/ProductType/SurchargeRate")]
    public async Task<ActionResult<bool>> AddSurchargeRate([FromBody] SurchargeRateDto surchargeRate)
    {
        if (surchargeRate == null)
            return BadRequest("No surcharge Rate found!");

        var result = await _productTypeService.AddSurcharge(surchargeRate);

        if (result)
            return Ok(result);
        return BadRequest("The Surcharge couldn't be added!");
    }
}
