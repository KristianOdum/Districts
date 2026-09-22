namespace Districts.Api.Infrastructure;

public class BusinessRuleViolationException(string message) : Exception(message);