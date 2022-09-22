using System;
using System.Collections.Generic;
using System.Text;
using CourseManagement.Enums;

namespace CourseManagement.Models
{
    class Group
    {
        private static int _count;
        public byte Limit;
        public string No;
        public GroupType GroupType;
        public Student[] Students;

        static Group()
        {
            _count = 100;
        }

        public Group(GroupType groupType, byte limit)
        {
            Students = new Student[0];
            GroupType = groupType;
            Limit = limit;
            _count++;
            No = $"{groupType.ToString()[0]}{_count}";
        }
    }
}
