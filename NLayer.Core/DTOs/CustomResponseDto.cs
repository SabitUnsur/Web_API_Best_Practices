using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs
{
    public class CustomResponseDto<T>
    {
        //Factory Method Design Pattern
        public T Data { get; set; }

        [JsonIgnore]
        public int StatusCode { get; set; } //Bu şekilde clienta status code dönmez.
        public List<String> Errors { get; set; }

        public static CustomResponseDto<T> Success(int statusCode, T Data)
        {
            return new CustomResponseDto<T>() { Data = Data, StatusCode = statusCode};
        }
        public static CustomResponseDto<T> Success(int statusCode)
        {
            return new CustomResponseDto<T>() { StatusCode = statusCode };
        }

        public static CustomResponseDto<T> Fail(int statusCode , List<String> errors)
        {
            return new CustomResponseDto<T>() { StatusCode = statusCode , Errors = errors };
        }

        public static CustomResponseDto<T> Fail(int statusCode, string error)
        {
            return new CustomResponseDto<T>() { StatusCode = statusCode, Errors = new List<string> { error } };
        }

    }
}
