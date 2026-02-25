using System.Net;
using System.Text.Json;
using FluentValidation;
using MooMoo.Application.Common.Constants;
using MooMoo.Application.Common.DTOs;
using MooMoo.Application.Exceptions;

namespace MooMoo.Api.Middleware;

public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

    public GlobalExceptionHandlerMiddleware(
        RequestDelegate next,
        ILogger<GlobalExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        HttpStatusCode statusCode;
        object response;

        switch (exception)
        {
            case ConflictException conflictEx:
                statusCode = HttpStatusCode.Conflict;
                response = new ApiErrorResponse
                {
                    Error = new ErrorResponse
                    {
                        Code = conflictEx.ErrorCode,
                        Field = conflictEx.Field
                    }
                };
                break;

            case NotFoundException notFoundEx:
                statusCode = HttpStatusCode.NotFound;
                response = new ApiErrorResponse
                {
                    Error = new ErrorResponse
                    {
                        Code = notFoundEx.ErrorCode,
                        Field = notFoundEx.Field
                    }
                };
                break;

            case BadRequestException badRequestEx:
                statusCode = HttpStatusCode.BadRequest;
                response = new ApiErrorResponse
                {
                    Error = new ErrorResponse
                    {
                        Code = MessageCodes.ERROR_VALIDATION_ERROR
                    }
                };
                break;

            case UnauthorizedException:
                statusCode = HttpStatusCode.Unauthorized;
                response = new ApiErrorResponse
                {
                    Error = new ErrorResponse
                    {
                        Code = MessageCodes.ERROR_UNAUTHORIZED
                    }
                };
                break;

            case ForbiddenException:
                statusCode = HttpStatusCode.Forbidden;
                response = new ApiErrorResponse
                {
                    Error = new ErrorResponse
                    {
                        Code = MessageCodes.ERROR_FORBIDDEN
                    }
                };
                break;

            case ValidationException validationEx:
                statusCode = HttpStatusCode.BadRequest;
                response = new ApiErrorResponse
                {
                    Error = new ErrorResponse
                    {
                        Code = MessageCodes.ERROR_VALIDATION_ERROR
                    },
                    Errors = validationEx.Errors.Select(e => new ErrorResponse
                    {
                        Code = GetValidationErrorCode(e.ErrorCode),
                        Field = ToCamelCase(e.PropertyName),
                        Metadata = new Dictionary<string, object>
                        {
                            { "attemptedValue", e.AttemptedValue ?? "" }
                        }
                    }).ToList()
                };
                break;

            case UnauthorizedAccessException:
                statusCode = HttpStatusCode.Unauthorized;
                response = new ApiErrorResponse
                {
                    Error = new ErrorResponse
                    {
                        Code = MessageCodes.ERROR_UNAUTHORIZED
                    }
                };
                break;

            case KeyNotFoundException:
                statusCode = HttpStatusCode.NotFound;
                response = new ApiErrorResponse
                {
                    Error = new ErrorResponse
                    {
                        Code = MessageCodes.ERROR_RESOURCE_NOT_FOUND
                    }
                };
                break;

            default:
                statusCode = HttpStatusCode.InternalServerError;
                response = new ApiErrorResponse
                {
                    Error = new ErrorResponse
                    {
                        Code = MessageCodes.ERROR_INTERNAL_SERVER_ERROR
                    }
                };
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        return context.Response.WriteAsync(json);
    }

    private static string GetValidationErrorCode(string fluentValidationCode)
    {
        return fluentValidationCode switch
        {
            "EmailValidator" => MessageCodes.ERROR_INVALID_EMAIL_FORMAT,
            "NotEmptyValidator" => MessageCodes.ERROR_REQUIRED_FIELD_MISSING,
            "MinimumLengthValidator" => MessageCodes.ERROR_PASSWORD_TOO_WEAK,
            _ => MessageCodes.ERROR_VALIDATION_ERROR
        };
    }

    private static string ToCamelCase(string str)
    {
        if (string.IsNullOrEmpty(str) || char.IsLower(str[0]))
            return str;
        return char.ToLower(str[0]) + str.Substring(1);
    }
}
