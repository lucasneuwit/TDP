namespace TDP.Models.Application;

/// <summary>
/// Exception thrown when a movie could not be found.
/// </summary>
/// <param name="message">The exception message.</param>
public class MovieNotFoundException(string message) : Exception(message);