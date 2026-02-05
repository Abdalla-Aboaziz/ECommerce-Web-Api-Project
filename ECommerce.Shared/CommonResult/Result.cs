using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.CommonResult
{
    public class Result
    {
        // IsSuccess: Indicates whether the operation was successful or not.
        // IsFailure: Indicates whether the operation failed or not.
        // Errors: A list of error messages that occurred during the operation.[Code,Description,ErrorType]

        private readonly List<Error> _errors = [];
        public bool IsSuccess=>_errors.Count == 0;
        public bool IsFailure => !IsSuccess;

        public IReadOnlyList<Error> Errors => _errors; // Expose errors as a read-only list to prevent external modification.

        // Ok - Successful result without any errors.
        protected Result() 
        {
            
        }
        // Fail - Failed result with one Error.
        protected Result(Error error)
        {
            _errors.Add(error);

        }
        // Fail - Failed result with multiple Errors.
        protected Result(List<Error> errors)
        {
            _errors.AddRange(errors);
        }
        public static Result Ok()
        {
            return new Result();
        }
        public static Result Fail(Error error)
        {
            return new Result(error);
        }
        public static Result Fail(List<Error> errors)
        {
            return new Result(errors);
        }

    }

    public class Result<Tvalue> : Result
    {
        private readonly Tvalue _value;

        public Tvalue Value => IsSuccess ? _value : throw new InvalidOperationException("Cannot access the value of a failed result.");

        // ok - Successful result with a value.
        private Result(Tvalue value) : base()
        {
            _value = value;
        }
        // Fail - Failed result with one Error.
        private Result(Error error) : base(error)
        {
            _value = default!;
        }
        // Fail - Failed result with multiple Errors.
        private Result(List<Error> errors) : base(errors)
        {
            _value = default!;
        }

        public static Result<Tvalue> Ok(Tvalue value)=> new Result<Tvalue>(value);
        public static new Result<Tvalue> Fail(Error error) => new Result<Tvalue>(error);
        public static new Result<Tvalue> Fail(List<Error> errors) => new Result<Tvalue>(errors);

        // Additinal Overloads for implicit conversions to simplify the creation of Result<Tvalue> instances.

        // Implicit conversion from Tvalue to Result<Tvalue> for successful results.
        public static implicit operator Result<Tvalue>(Tvalue value) => Ok(value);
        // Implicit conversion from Error to Result<Tvalue> for failed results.
        public static implicit operator Result<Tvalue>(Error error) => Fail(error);
        // Implicit conversion from List<Error> to Result<Tvalue> for failed results.
        public static implicit operator Result<Tvalue>(List<Error> errors) => Fail(errors);


    }
}
