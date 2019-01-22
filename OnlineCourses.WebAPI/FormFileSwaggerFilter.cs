using System.Collections.Generic;
using System.Linq;
using Courses.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Courses.WebAPI
{
    public class FileUploadOperation : IOperationFilter
    {
        private readonly IEnumerable<string> _actionsWithUpload = new []
        {
            NamingHelpers.GetOperationId<FilesController>(nameof(FilesController.Post))
        };

        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (_actionsWithUpload.Contains(operation.OperationId) )
            {
                operation.Parameters.Clear();
                operation.Parameters.Add(new NonBodyParameter
                {
                    Name = "file",
                    In = "formData",
                    Description = "Upload File",
                    Required = true,
                    Type = "file"
                });
                operation.Consumes.Add("multipart/form-data");
            }
        }
    }

    public static class NamingHelpers
    {
        public static string GetOperationId<T>(string actionName) where T : Controller => $"{GetControllerName<T>()}{actionName}";

        public static string GetControllerName<T>() where T : Controller => typeof(T).Name.Replace(nameof(Controller), string.Empty);
    }
}