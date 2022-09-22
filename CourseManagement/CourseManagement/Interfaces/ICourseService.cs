using System;
using System.Collections.Generic;
using System.Text;
using CourseManagement.Models;
using CourseManagement.Enums;

namespace CourseManagement.Interfaces
{
    interface ICourseService
    {
        Group[] Groups {get;}
        void AddGroup(GroupType groupType, byte limit);
        void AddStudent(string name, string surName, byte age, string groupNo, StudentType studentType);
        bool CheckgroupNo(string groupNo);
        Group GetGroupByNo(string no);
        void EditGroup(string no,GroupType groupType, byte limit);
        void EditStudent(string groupNo, int id, string name, string surname, byte age, StudentType studentType);
        bool CheckStudnetById(int id, string groupNo);
    }
}
