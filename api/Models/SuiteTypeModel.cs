using System;
using System.ComponentModel.DataAnnotations;
using Suite.Model;

namespace SuiteType.Model;

  public class SuiteTypeModel
  {
    public SuiteTypeModel(){}
    public SuiteTypeModel(string description, decimal price)
  {
    Id = Guid.NewGuid();
    Description = description;
    Price = price;
  }
    public Guid Id { get; init; }

    [Required]
    [MaxLength(100)]
    public string Description { get; private set; }

    [Required]
    [Range(0, 9999999999.99)]
    public decimal Price { get; private set; }

    public virtual ICollection<SuiteModel> Suites { get; set; }
  }
  