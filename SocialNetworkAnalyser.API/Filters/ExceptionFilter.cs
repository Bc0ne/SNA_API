namespace SocialNetworkAnalyser.API.Filters
{
    using System;
    using System.Net;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using SocialNetworkAnalyser.Web.Models;

    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            ColorizeException(" Exception:");

            var result = new ObjectResult(ResponseResult.Failed())
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };

            context.Result = result;
        }

        private void ColorizeException(string title)
        {
            var defaultBackColor = Console.BackgroundColor;
            var defaultForeColor = Console.ForegroundColor;

            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine(title);
            Console.BackgroundColor = defaultBackColor;
            Console.ForegroundColor = defaultForeColor;
        }
    }
}