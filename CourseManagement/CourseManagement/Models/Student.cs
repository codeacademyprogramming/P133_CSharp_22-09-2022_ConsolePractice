using System;
using System.Collections.Generic;
using System.Text;
using CourseManagement.Enums;

namespace CourseManagement.Models
{
    class Student
    {
        private static int _id;
        public readonly int Id;
        public string Name;
        public string SurName;
        public byte Age;
        public string GroupNo;
        public StudentType StudentType;

        static Student()
        {
            _id = 0;
        }

        public Student(string name, string surName, byte age, string groupNo, StudentType studentType)
        {
            Name = name;
            SurName = surName;
            Age = age;
            GroupNo = groupNo.ToUpper();
            StudentType = studentType;
            _id++;
            Id = _id;
        }

        public override string ToString()
        {
            return $"Name: {Name}\nSurName: {SurName}";
        }
    }
}
