using Suite.Model;

namespace Motel.Request;

public record MotelRequest(string Name, string Address, string Phone,ICollection<SuiteModel> Suites);
 

