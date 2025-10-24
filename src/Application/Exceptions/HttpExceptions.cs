namespace CMS_NetApi.Application.Exceptions;

public abstract class HttpException(int statusCode, string message) : Exception(message)
{
    public int StatusCode { get; } = statusCode;
}

#region 4xx Client errors
public class BadRequestException(string message = "Solicitud incorrecta") : HttpException(400, message)
{
}

public class UnauthorizedException(string message = "No autorizado") : HttpException(401, message)
{
}

public class ForbiddenException(string message = "Prohibido") : HttpException(403, message)
{
}

public class NotFoundException(string message = "Recurso no encontrado") : HttpException(404, message)
{
}

public class ConflictException(string message = "Conflicto con el estado actual") : HttpException(409, message)
{
}

public class UnsupportedMediaTypeException(string message = "Tipo de contenido no soportado") : HttpException(415, message)
{
}

public class UnprocessableEntityException(string message = "Entidad no procesable") : HttpException(422, message)
{
}

public class TooManyRequestsException(string message = "Demasiadas solicitudes") : HttpException(429, message)
{
}
#endregion

#region 5xx Server errors
public class InternalServerException(string message = "Error interno del servidor") : HttpException(500, message)
{
}

public class NotImplementedException(string message = "No implementado") : HttpException(501, message)
{
}

public class BadGatewayException(string message = "Puerta de enlace incorrecta") : HttpException(502, message)
{
}

public class ServiceUnavailableException(string message = "Servicio no disponible") : HttpException(503, message)
{
}

public class GatewayTimeoutException(string message = "Tiempo de espera agotado") : HttpException(504, message)
{
}
#endregion

#region 2xx Success (poco comunes como excepción)
public class CreatedException(string message = "Recurso creado") : HttpException(201, message)
{
}

public class AcceptedException(string message = "Aceptado para procesamiento") : HttpException(202, message)
{
}

public class NoContentException(string message = "Sin contenido") : HttpException(204, message)
{
}
#endregion