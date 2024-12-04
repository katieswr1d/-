using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Email
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Автоинкремент
    public int Id { get; set; }

    public string Value { get; set; }

    public Email(string value)
    {
        Value = value;
    }

    public override string ToString()
    {
        return Value;
    }
}