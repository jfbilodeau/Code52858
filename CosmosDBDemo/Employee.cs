using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

class Employee {
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [JsonPropertyName("dept")]
    [Column("dept")]
    public string Department { get; set; }
    public double? Salary { get; set; }
}

