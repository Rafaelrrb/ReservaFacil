using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Suite.Model;

namespace Motel.Model
{
  [Table("Motel")]
  public class MotelModel
  {
     public MotelModel() { }
    public MotelModel(string name, string address, string phone)
    {
      Id = Guid.NewGuid();
      Name = name;
      Address = address;
      Phone = phone;
    }
    
    public Guid Id { get; init;}

    [Required]
    [StringLength(100)]
    public string Name { get; private set; }

    [Required]
    public string Address { get; private set; }

    [Required]
    [StringLength(20)]
    public string Phone { get; private set; }

    public void ChangePhone(string phone) => Phone = Phone;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public virtual ICollection<SuiteModel> Suites { get; set; }

  }
}