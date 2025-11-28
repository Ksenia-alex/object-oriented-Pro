using System.Text;

namespace Lab1.CoursePlatform.Models.Persons;

public abstract class Person
{
    public string Id { get; } = Guid.NewGuid().ToString();
    public string FullName { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime? BirthDate { get; set; }
    
    protected Person(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
        {
           throw new ArgumentException("Full name cannot be null or white space.", nameof(fullName)); 
        }
        FullName = fullName;
    }

    public virtual string GetFullInfo()
    {
        var sb = new StringBuilder();
        
        sb.AppendLine($"ФИО: {FullName}");
        AppendInfoIfNotEmpty(sb, "Почта", Email);
        AppendInfoIfNotEmpty(sb, "Номер телефона",  PhoneNumber);
        if (BirthDate.HasValue)
        {
            sb.AppendLine($"День рождения: {BirthDate.Value:dd.MM.yyyyy}");
        }

        return sb.ToString().Trim();
    }
    
    protected void AppendInfoIfNotEmpty(StringBuilder sb, string label, string info)
    {
        if (!string.IsNullOrWhiteSpace(info))
        {
            sb.AppendLine($"{label}: {info}\n");
        }
    }
}