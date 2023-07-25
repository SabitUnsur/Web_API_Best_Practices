using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayer.Core.DTOs;

namespace NLayer.API.Filters
{
    public class ValidateFilterAttribute : ActionFilterAttribute
    {
        //Filters : Bir request'i biz apide metoda girmeden önce girdiği anda girdikten sonra aralarda business kodları eklemeye yarar.
        //Bunlara Interceptors denir.
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //Fluent Validation hataları buraya eklenir.
            //Biz CustomResponse adında bir dönüş yaptığımız için fluentValidationun kendi dönüşü yerine buna entegre ettik. 
            if (!context.ModelState.IsValid)               
            {
                 var errors = context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                context.Result = new BadRequestObjectResult(CustomResponseDto<NoContentDto>.Fail(400, errors));
            }
        }
    }
}
