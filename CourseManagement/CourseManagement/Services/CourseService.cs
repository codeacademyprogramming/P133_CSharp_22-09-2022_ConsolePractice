using System;
using System.Collections.Generic;
using System.Text;
using CourseManagement.Models;
using CourseManagement.Enums;
using CourseManagement.Interfaces;

namespace CourseManagement.Services
{
    class CourseService : ICourseService
    {
        private Group[] _groups;
        public CourseService()
        {
            _groups = new Group[0];
        }
        public Group[] Groups  => _groups;

        public void AddGroup(GroupType groupType, byte limit)
        {
            Group group = new Group(groupType, limit);

            Array.Resize(ref _groups, _groups.Length + 1);
            _groups[_groups.Length - 1] = group;
        }

        public void AddStudent(string name, string surName, byte age, string groupNo, StudentType studentType)
        {
            foreach (Group group in _groups)
            {
                if (group.No == groupNo.ToUpper())
                {
                    if (group.Limit > group.Students.Length)
                    {
                        Student student = new Student(name, surName, age, groupNo, studentType);

                        Array.Resize(ref group.Students,group.Students.Length+1);
                        group.Students[group.Students.Length - 1] = student;
                    }
                    else
                    {
                        Console.WriteLine($"{group.No} -  Da Yer Yoxdur");
                    }
                }
            }
        }

        public bool CheckgroupNo(string groupNo)
        {
            foreach (Group group in _groups)
            {
                if (group.No == groupNo.ToUpper())
                {
                    return true;
                }
            }

            return false;
        }

        public bool CheckStudnetById(int id, string groupNo)
        {
            foreach (Group group in _groups)
            {
                foreach (Student student in group.Students)
                {
                    if (student.Id == id && student.GroupNo == groupNo.ToUpper())
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void EditGroup(string no, GroupType groupType, byte limit)
        {
            foreach (Group group in _groups)
            {
                if (group.No == no.ToUpper())
                {
                    if (group.Students.Length <= limit)
                    {
                        group.Limit = limit;
                    }
                    else
                    {
                        Console.WriteLine("Yeni Limit Uygun Deyil");
                        return;
                    }

                    group.No = group.No.Replace(group.No[0], groupType.ToString()[0]);
                    group.GroupType = groupType;

                    foreach (Student student in group.Students)
                    {
                        student.GroupNo = group.No;
                    }

                    return;
                }
            }
        }

        public void EditStudent(string groupNo, int id, string name, string surname, byte age, StudentType studentType)
        {
            foreach (Group group in _groups)
            {
                if (group.No == groupNo.ToUpper())
                {
                    foreach (Student student in group.Students)
                    {
                        if (student.Id == id)
                        {
                            student.Name = name;
                            student.SurName = surname;
                            student.Age = age;
                            student.StudentType = studentType;

                            return;
                        }
                    }
                }
            }
        }

        public Group GetGroupByNo(string no)
        {
            foreach (Group group in _groups)
            {
                if (group.No == no.ToUpper())
                {
                    return group;
                }
            }

            return null;
        }
    }
}
