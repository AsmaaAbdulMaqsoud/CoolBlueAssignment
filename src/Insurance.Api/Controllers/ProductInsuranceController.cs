using Insurance.Application.Common.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Insurance.Api.Controllers;

public class ProductInsuranceController : Controller
{
    private readonly IProductInsuranceService _productInsuranceService;

    public ProductInsuranceController(IProductInsuranceService productInsuranceService)
    {
        _productInsuranceService = productInsuranceService;
    }

    [HttpGet]
    [Route("api/insurance/product")]
    public async Task<ActionResult<decimal>> CalculateInsurance(int productId)
    {

        if (productId == 0)
            return BadRequest("No product id found");

        return await _productInsuranceService.GetProductInsurance(productId);

    }
}
