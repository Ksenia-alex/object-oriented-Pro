using System.Text;

namespace Lab1.CoursePlatform.Models.Persons;

public class Student : Person
{
    public string PersonalInfo { get; set; }
    
    public Student(string fullName, string personalInfo = "")
        : base(fullName)
        {
            PersonalInfo = personalInfo;
        }

    public override string GetFullInfo()
    {
        var sb = new StringBuilder();
        string baseInfo = base.GetFullInfo();
        
        sb.AppendLine(baseInfo);
        AppendInfoIfNotEmpty(sb, "Персональная информация",  PersonalInfo);
        
        return sb.ToString().Trim();
    }
}