using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ContactBook.Core.Entity;

public class PhoneNumber
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Автоинкремент
    public int Id { get; set; } //добавила айди

    public string Value { get; set; }

    public PhoneNumber(string Value)
    {
        this.Value = Value;
    }

    public override string ToString()
    {
        return Value;
    }
}