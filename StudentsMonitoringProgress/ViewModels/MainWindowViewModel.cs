using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Reactive;
using ReactiveUI;
using StudentsMonitoringProgress.Models;

namespace StudentsMonitoringProgress.ViewModels
{
    public class MainWindowViewModel : ReactiveObject, INotifyPropertyChanged
    {
        public new event PropertyChangedEventHandler? PropertyChanged;
        private ObservableCollection<Student> _students = null!;
        private int _index;
        private string _newStudentName = null!;
        private int _selectedVisualProgGrade;
        private int _selectedMathAnalysisGrade;
        private int _selectedElectrotechnicGrade;
        private int _selectedComputerMathGrade;
        private List<int> _gradeChoices = new List<int> { 0, 1, 2 };
        public ObservableCollection<Student> Students
        {
            get { return _students; }
            set
            {
                _students = value;
                OnPropertyChanged("Students");
            }
        }
        public MainWindowViewModel()
        {
            Students = new ObservableCollection<Student>();
            AddStudentCommand = ReactiveCommand.Create(AddStudent);
            DeleteStudentCommand = ReactiveCommand.Create(DeleteStudent);
            Random rnd = new Random();
            string[] names = { "Денис Хухарев", "Илья Пожидаев", "Илья Стриков", "Павел Траханов", "Семён Лазарев", "Павел Махонин" };
            List<string> availableNames = new List<string>(names);
            
            for (int i = 0; i < names.Length; i++)
            {
                int index = rnd.Next(availableNames.Count);
                string name = availableNames[index]; 
                availableNames.Remove(name); 
                Student student = new Student();
                student.Name = name;
                student.VisualProg = rnd.Next(0, 2);
                student.MathAnalysis = rnd.Next(1, 3);
                student.Electrotechnic = rnd.Next(2, 3);
                student.ComputerMath = rnd.Next(0, 2);
                student.AverageMark = Math.Round((double)(student.VisualProg +student.MathAnalysis + student.Electrotechnic +student.ComputerMath) / 4, 2);
                Students.Add(student);
            }

            double sumVisualProg = 0, sumMathAnalysis = 0, sumElectrotechnic = 0, sumComputerMath = 0;
            foreach (var student in Students)
            {
                sumVisualProg += student.VisualProg;
                sumMathAnalysis += student.MathAnalysis;
                sumElectrotechnic += student.Electrotechnic;
                sumComputerMath += student.ComputerMath;
            }

            int count = Students.Count;
            double averageVisualProg = Math.Round(sumVisualProg / count, 2);
            double averageMathAnalysis = Math.Round(sumMathAnalysis / count, 2);
            double averageElectrotechnic = Math.Round(sumElectrotechnic / count, 2);
            double averageComputerMath = Math.Round(sumComputerMath / count, 2);         
            ScVisualProg = averageVisualProg.ToString(CultureInfo.CurrentCulture);
            ScMathAnalysis = averageMathAnalysis.ToString(CultureInfo.CurrentCulture);
            ScElectrotechnic = averageElectrotechnic.ToString(CultureInfo.CurrentCulture);
            ScComputerMath = averageComputerMath.ToString(CultureInfo.CurrentCulture);   
            ScAverageMark = Math.Round((averageVisualProg + averageMathAnalysis + averageElectrotechnic + averageComputerMath) / 3, 2).ToString(CultureInfo.CurrentCulture);
        }

        public ReactiveCommand<Unit, Unit> AddStudentCommand { get; }
        public ReactiveCommand<Unit, Unit> DeleteStudentCommand { get; }
        private void AddStudent()
        {
            Student newStudent = new Student()
            {
                Name = NewStudentName,
                VisualProg = SelectedVisualProgGrade,
                MathAnalysis = SelectedMathAnalysisGrade,
                Electrotechnic = SelectedElectrotechnicGrade,
                ComputerMath = SelectedComputerMathGrade,
            };
            Students.Add(newStudent);
            OnPropertyChanged(nameof(Students));
        }
        private void DeleteStudent()
        {
            Student selectedStudent = Students[Index];
            Students.Remove(selectedStudent);
            OnPropertyChanged(nameof(Students));
        }
        public string ScAverageMark { get; set; }
        public string ScComputerMath { get; set; }
        public string ScElectrotechnic { get; set; }
        public string ScMathAnalysis { get; set; }
        public string ScVisualProg { get; set; }
        public int Index
        {
            get { return _index; }
            set
            {
                _index = value;
                OnPropertyChanged("Index");
            }
        }
        public string NewStudentName
        {
            get { return _newStudentName; }
            set { this.RaiseAndSetIfChanged(ref _newStudentName, value); }
        }
        public int SelectedVisualProgGrade
        {
            get { return _selectedVisualProgGrade; }
            set { this.RaiseAndSetIfChanged(ref _selectedVisualProgGrade, value); }
        }
        public int SelectedMathAnalysisGrade
        {
            get { return _selectedMathAnalysisGrade; }
            set { this.RaiseAndSetIfChanged(ref _selectedMathAnalysisGrade, value); }
        }
        public int SelectedElectrotechnicGrade
        {
            get { return _selectedElectrotechnicGrade; }
            set { this.RaiseAndSetIfChanged(ref _selectedElectrotechnicGrade, value); }
        }
        public int SelectedComputerMathGrade
        {
            get { return _selectedComputerMathGrade; }
            set { this.RaiseAndSetIfChanged(ref _selectedComputerMathGrade, value); }
        }
        public List<int> GradeChoices
        {
            get => _gradeChoices;
            set => this.RaiseAndSetIfChanged(ref _gradeChoices, value);
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }

}