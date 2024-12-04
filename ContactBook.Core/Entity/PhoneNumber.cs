using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;

namespace ContactBook.Core.Entity;

public class PhoneNumber
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Автоинкремент
    public int Id { get; set; }
    public string Value { get; set; }
    public int ContactId { get; set; }
    public Contact Contact { get; set; }
    public PhoneNumber(string Value)
    {
        this.Value = Value;
    }
    public override string ToString() {
        return Value;
    }
}