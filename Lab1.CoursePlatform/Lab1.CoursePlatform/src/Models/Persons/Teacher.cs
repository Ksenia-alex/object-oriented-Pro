using System.Text;

namespace Lab1.CoursePlatform.Models.Persons;

public class Teacher : Person
{ 
    public string Specialization { get; set; }
    public string Bio  { get; set; } = string.Empty;

    public Teacher(string fullName, string specialization)
        : base(fullName)
    {
        Specialization = specialization;
    }

    public override string GetFullInfo()
    {
        var sb = new StringBuilder();
        string baseInfo = base.GetFullInfo();
        
        sb.AppendLine(baseInfo);
        AppendInfoIfNotEmpty(sb, "Специальность", Specialization);
        AppendInfoIfNotEmpty(sb, "Биография", Bio);
        return sb.ToString().Trim();
    }
}