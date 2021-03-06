using Application.DTO;
using Microsoft.AspNetCore.Http;

namespace Application.Extensions
{
    public static class HttpContextExtension
    {
        /// <summary>
        /// Extension to get the username from the HttpContext and set this value in command. This username must have been previously set by the authentication middleware
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetUserName(this HttpContext context, CommandBase command)
        {
            string userName = context.Items.ContainsKey("UserName") ? (string)context.Items["UserName"] : string.Empty;
            command.SetUsername(userName);

            return userName; 
        }

        /// <summary>
        /// Extension to get the username from the HttpContext. This username must have been previously set by the authentication middleware
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetUserName(this HttpContext context)
        {
            return context.Items.ContainsKey("UserName") ? (string)context.Items["UserName"] : string.Empty;
        }
    }
}
