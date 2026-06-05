using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Normative.Data;
using Normative.Models.Table;

namespace Normative.Controllers.API
{
    [ApiController]
    [Route("API/Preparation")]
    public class PreparationController : ControllerBase
    {
        private readonly NormativeContext _context;

        public PreparationController(NormativeContext context)
        {
            _context = context;
        }


        [HttpGet("GetValues/{ProductSize}/{PreparationTypeId}")]
        public IResult GetValues2(string ProductSize, int PreparationTypeId)
        {

            var list = _context.Preparations.Include(o => o.ProductSize).Include(o => o.PreparationType).ToList();


            Preparation preparation = list.FirstOrDefault(f => f.ProductSize.Size == ProductSize && f.PreparationType.Id == PreparationTypeId);

            if (preparation == null)
            {
                return Results.BadRequest("not found");
            }
            else
            {
                var ret = JsonConvert.SerializeObject(preparation);
                Console.WriteLine(ret);
                return Results.Ok(ret);
            }
        }
    }
}
