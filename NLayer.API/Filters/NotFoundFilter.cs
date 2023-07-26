using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Model;
using NLayer.Core.Services;

namespace NLayer.API.Filters
{
    //NotFoundException bu sefer Filter ile yazıldı.
    //Bazen data null olduğunda mesaj kuyruğuna meaj atsın, sms göndersin gibi işlemler için de Filter yazılır.
    public class NotFoundFilter<T> : IAsyncActionFilter where T : BaseEntity //Generic kullandığımız için Interface mirası aldık.
    {
        private readonly IService<T> _service;

        public NotFoundFilter(IService<T> service)
        {
            _service = service;
        }

        //Eğer ki bir filter constructorunda parametre alıyorsa Controller içine [ServiceFilter] ile kullanılmalıdır.
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var idValue = context.ActionArguments.Values.FirstOrDefault(); //Controllerdaki ilk propertynin (parametresi olan) ilk değerini al.
            
            if (idValue == null)
            {
                await next.Invoke();
                return;
            }

            var id = (int) idValue;
            var anyEntity= await _service.AnyAsync(x=>x.ID==id);

            if (anyEntity)
            {
                await next.Invoke();
                return;
            }

            context.Result = new NotFoundObjectResult(CustomResponseDto<NoContentDto>.Fail(404, $"{typeof(T).Name} is not found"));

        }
    }
}
