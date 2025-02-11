using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Motel.Model;
using Suite.Model;
using User.Model;

namespace Reservation.Model;

  public class ReservationModel
  {
    public ReservationModel(){}
    public ReservationModel(Guid idUser, Guid idSuite, Guid idMotel, DateTime dataEntrada, DateTime dataSaida, string status)
    {
      Id = Guid.NewGuid();
      IdUser = idUser;
      IdSuite = idSuite;
      IdMotel = idMotel;
      DataEntrada = dataEntrada;
      DataSaida = dataSaida;
      Status = status;
    }
    public Guid Id { get; init; }

    [Required]
    [ForeignKey("User")]
    public Guid IdUser { get; set; }

    [Required]
    [ForeignKey("Suite")]
    public Guid IdSuite { get; set; }

    [Required]
    [ForeignKey("Motel")]
    public Guid IdMotel { get; set; }

    [Required]
    public DateTime DataEntrada { get; private set; }

    [Required]
    public DateTime DataSaida { get; private set; }

    [Required]
    [MaxLength(20)]
    public string Status { get; private set; }

    // Navigation properties
    public virtual UserModel User { get; set; }
    public virtual SuiteModel Suite { get; set; }
    
    public virtual MotelModel Motel { get; set; }
  }

  