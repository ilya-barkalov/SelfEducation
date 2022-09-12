using FluentValidation.Results;

namespace Application.Common.Exceptions;

public class ValidationException : Exception
{
    private const string BusinessRuleKey = "BusinessRule";

    public IDictionary<string, string[]> Errors { get; }

    public ValidationException()
        : base("One or more validation failures have occurred.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(string error)
        : this()
    {
        Errors.Add(BusinessRuleKey, new[] { error });
    }

    public ValidationException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }
}
