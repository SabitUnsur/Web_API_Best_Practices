using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Model;
using NLayer.Core.Services;

namespace NLayer.Web
{
    public class NotFoundFilter<T> : IAsyncActionFilter where T : BaseEntity
    {
        private readonly IService<T> _service;

        public NotFoundFilter(IService<T> service)
        {
            _service = service;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var idValue = context.ActionArguments.Values.FirstOrDefault(); 

            if (idValue == null)
            {
                await next.Invoke();
                return;
            }

            var id = (int)idValue;
            var anyEntity = await _service.AnyAsync(x => x.ID == id);

            if (anyEntity)
            {
                await next.Invoke();
                return;
            }

            var ErrorViewModel = new ErrorViewModel();
            ErrorViewModel.Errors.Add($"{typeof(T)} not found");

            context.Result = new RedirectToActionResult("Error", "Home", ErrorViewModel);
        }
    }
}
