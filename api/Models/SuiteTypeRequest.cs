using Suite.Model;

namespace SuiteType.Request;
public record SuiteTypeRequest(string description, decimal price, ICollection<SuiteModel> Suites);
    
