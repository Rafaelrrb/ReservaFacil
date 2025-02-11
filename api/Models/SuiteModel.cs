using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Motel.Model;
using SuiteType.Model; // Add this line to import the Motel class

namespace Suite.Model;

  public class SuiteModel
  {
    public SuiteModel() { }
    public SuiteModel(string numero, Guid idMotel, Guid idTypeSuite, string status)
    {
      Id = Guid.NewGuid();
      Numero = numero;
      IdMotel = idMotel;
      IdTypeSuite = idTypeSuite;
      Status = status;
    }
    public Guid Id { get; init; }

    [Required]
    [StringLength(10)]
    public string Numero { get; private set; }

    [Required]
    public Guid IdMotel { get; private set; }

    [Required]
    public Guid IdTypeSuite { get; private set; }

    [Required]
    [StringLength(20)]
    [RegularExpression("Disponível|Ocupada|Manutenção")]
    public string Status { get; private set; }


    [ForeignKey("IdMotel")]
    [JsonIgnore]
    public MotelModel Motel { get; set; }


    [ForeignKey("IdTypeSuite")]
    [JsonIgnore]
    public virtual SuiteTypeModel SuiteType { get; set; }
  }

  