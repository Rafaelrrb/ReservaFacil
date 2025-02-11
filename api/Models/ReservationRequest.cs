namespace Reservation.Request;

public record ReservationRequest(Guid IdUser, Guid IdSuite, Guid IdMotel, DateTime DataEntrada, DateTime DataSaida, string Status);
