using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.CommonResult
{
    public class Error
    {
      
        public string Code { get; set; }
        public string Description { get; set; }
        public ErrorType Type { get; set; }

        private Error(string code, string description, ErrorType type) // Private constructor to enforce the use of factory methods
        {
            Code = code;
            Description = description;
            Type = type;

        }

        // Factory method to create an error with a specific type

        public static Error Failure(string code="General.Failure", string description = "General.Failure has occure")
        {
            return new Error(code, description,ErrorType.Failure);
        }
        public static Error InvalidCredintails(string code= "General.InvalidCredentials", string description= "General.InvalidCredentials")
        {
            return new Error(code, description, ErrorType.InvalidCredentials);
        }
        public static Error NotFound(string code = "General.NotFound", string description = "General.Failure has occure")
        {
            return new Error(code, description, ErrorType.NotFound);
        }

        public static Error Validation(string code = "General.Validation", string description = "General.Validation has occure")
        {
            return new Error(code, description, ErrorType.Validation);
        }

        public static Error Unauthorized(string code = "General.Unauthorized", string description = "General.Unauthorized has occure")
        {
            return new Error(code, description, ErrorType.Unauthorized);
        }

        public static Error Forbidden(string code = "General.Forbidden", string description = "General.Forbidden has occure")
        {
            return new Error(code, description, ErrorType.Forbidden);
        }


    }
}
