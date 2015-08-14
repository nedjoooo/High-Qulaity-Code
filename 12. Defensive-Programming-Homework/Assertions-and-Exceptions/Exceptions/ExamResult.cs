using System;

public class ExamResult
{
    public ExamResult(int grade, int minGrade, int maxGrade, string comments)
    {
        if (grade < 0)
        {
            throw new ArgumentOutOfRangeException("Grade", "Grade can not be negative!");
        }

        if (minGrade < 0)
        {
            throw new ArgumentOutOfRangeException("MinGrade", "MinGrade can not be negative!");
        }

        if (maxGrade <= minGrade)
        {
            throw new InvalidOperationException("MaxGrade can not be less or equal MinGrade");
        }

        if (string.IsNullOrEmpty(comments))
        {
            throw new ArgumentNullException("Comments", "Comments can not be empty!");
        }

        this.Grade = grade;
        this.MinGrade = minGrade;
        this.MaxGrade = maxGrade;
        this.Comments = comments;
    }

    public int Grade { get; private set; }

    public int MinGrade { get; private set; }

    public int MaxGrade { get; private set; }

    public string Comments { get; private set; }
}
