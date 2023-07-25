using Microsoft.AspNetCore.Diagnostics;
using NLayer.Core.DTOs;
using NLayer.Service.Exceptions;
using System.Text.Json;

namespace NLayer.API.Middlewares
{
    //Extension metotlar static olmalıdır. 
    //Bir hata olduğunda kendi CustomResponse modelimizi dönebilmek için middleware yazdık. 
    public static class UseCustomExceptionHandler
    { 
        public static void UserCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>(); //Hatayı alırız 
                    var statusCode = exceptionFeature.Error switch 
                    {
                        ClientSideException => 400, //Hata kullanıcıdan kaynaklı ise 400 dön , değil ise 500  dön olarak atama yaptık
                        _ => 500
                    };

                    context.Response.StatusCode = statusCode; //gelen statusCode ile response olusturduk.

                    var response = CustomResponseDto<NoContentDto>.Fail(statusCode,exceptionFeature.Error.Message); //Hatayı döneceğimiz modeli belirledik

                    await context.Response.WriteAsync(JsonSerializer.Serialize(response)); //Json Formatına çevirdik.
                });
            });
        }


    }
}
