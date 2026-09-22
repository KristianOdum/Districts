namespace Districts.Application.Exceptions;

public class BusinessRuleViolationException(string message) : Exception(message);