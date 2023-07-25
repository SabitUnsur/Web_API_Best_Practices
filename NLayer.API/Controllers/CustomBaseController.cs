using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;

namespace NLayer.API.Controllers
{
    public class CustomBaseController : ControllerBase
    {
        //APIde dönüş sağlarken her seferinde OK(...) yazmamak için burası tasarlandı.

        [NonAction] //Swagger'a endpoint olmadığını bildirdik
        public IActionResult CreateActionResult<T>(CustomResponseDto<T> response)
        {
            if (response.StatusCode == 204)
            {
                return new ObjectResult(null) { StatusCode = response.StatusCode };
            }

            return new ObjectResult(response) { StatusCode = response.StatusCode };
        }
    }
}
