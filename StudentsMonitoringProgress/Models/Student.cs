namespace StudentsMonitoringProgress.Models
{
    public class Student
    {
        public string Name { get; set; } = null!;
        public int VisualProg { get; set; }
        public int MathAnalysis { get; set; }
        public int Electrotechnic { get; set; }
        public int ComputerMath { get; set; }
        public double AverageMark { get; set; }
    }
}