using System;
using System.Linq;
using CourseManagement.Enums;
using CourseManagement.Interfaces;
using CourseManagement.Models;
using CourseManagement.Services;

namespace CourseManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            ICourseService courseService = new CourseService();

            do
            {
                Console.WriteLine("Etmek Isdediyniz Emeliyyatin Nomresini Daxil Edin:");
                Console.WriteLine("1. Qrup Elave Et");
                Console.WriteLine("2. Telebe Elave Et:");
                Console.WriteLine("3. Qruplarin Siyahisini Gor:");
                Console.WriteLine("4. Telebelerin Siyahisini Gor:");
                Console.WriteLine("5. Qrup Uzerinde Duzelis Et:");
                Console.WriteLine("6. Telebenin Melumatlarni Editle:");

                string answerStr = Console.ReadLine();
                int answerNum;

                while (!int.TryParse(answerStr, out answerNum) || answerNum < 1 || answerNum > 6)
                {
                    Console.WriteLine("Duzgun Secim Edin");
                    answerStr = Console.ReadLine();
                }

                switch (answerNum)
                {
                    case 1:
                        Console.Clear();
                        AddGroup(ref courseService);
                        break;
                    case 2:
                        AddStudnet(ref courseService);
                        break;
                    case 3:
                        ShowGroups(ref courseService);
                        break;
                    case 4:
                        ShowStudentsByGroupNo(ref courseService);
                        break;
                    case 5:
                        EditGroup(ref courseService);
                        break;
                    case 6:
                        EditStudent(ref courseService);
                        break;
                    default:
                        break;
                }
            } while (true);
        }

        static void AddGroup(ref ICourseService courseService)
        {
            Console.WriteLine("Groupub Novunu Sec Reqemi daxil Et");
            foreach (var item in Enum.GetValues(typeof(GroupType)))
            {
                Console.WriteLine((int)item + " " + item);
            }
            string grouptypestr = Console.ReadLine();
            int groupTypeNum;
            while (!int.TryParse(grouptypestr, out groupTypeNum) || groupTypeNum < 1 || groupTypeNum > 3)
            {
                //Console.BackgroundColor = ConsoleColor.Red;
                //Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Duzgun Group Novunun Nomresini Daxil Et");
                grouptypestr = Console.ReadLine();
            }

            Console.WriteLine("Qrupun Telebe Limitini Daxil Et");
            string limitstr = Console.ReadLine();
            byte limitNum;

            while (!byte.TryParse(limitstr, out limitNum) || limitNum < 12 || limitNum > 18)
            {
                Console.WriteLine("Duzgun Qrupun Telebe Limitini Daxil Et: Minimum 12 Maksimum 18 Ola Biler");
                limitstr = Console.ReadLine();
            }

            courseService.AddGroup((GroupType)groupTypeNum, limitNum);
            //Console.BackgroundColor = ConsoleColor.Red;
            //Console.ForegroundColor = ConsoleColor.White;
            //Console.WriteLine("Qrup Ugurla Elave Olundu");

            //Console.ResetColor();
        }

        static void AddStudnet(ref ICourseService courseService)
        {
            if (courseService.Groups.Length <= 0)
            {
                Console.WriteLine("Once Qrup Elave Et");
                return;
            }

            Console.WriteLine("Qrupu Secin: Nomresini Daxil Edin");
            foreach (Group group in courseService.Groups)
            {
                Console.WriteLine(group.No);
            }
            string groupNo = Console.ReadLine();

            while (!courseService.CheckgroupNo(groupNo))
            {
                Console.WriteLine("Duzgun Qrup Nomresini Daxil Et:");
                groupNo = Console.ReadLine();
            }

            Console.WriteLine("Telebenin Adini Daxil Et:");
            string name = Console.ReadLine();

            Console.WriteLine("Telebenin Soyadini Daxil Et:");
            string surname = Console.ReadLine();

            Console.WriteLine("Telebenin Yasini Daxil Et:");
            string ageStr = Console.ReadLine();
            byte ageNum;
            while (!byte.TryParse(ageStr, out ageNum) || ageNum < 15 || ageNum > 50)
            {
                Console.WriteLine("Duzgun Yas Daxil Et: Minimum 15 Maksimum 50");
                ageStr = Console.ReadLine();
            }

            Console.WriteLine("Telebenin Zemanet Novunu Sec");
            foreach (var item in Enum.GetValues(typeof(StudentType)))
            {
                Console.WriteLine($"{(int)item} {item}");
            }
            string studentTypeStr = Console.ReadLine();
            int studentTypeNum;

            while (!int.TryParse(studentTypeStr, out studentTypeNum) || studentTypeNum < 1 || studentTypeNum > 2)
            {
                Console.WriteLine("Duzgun Secim Edin:");
                studentTypeStr = Console.ReadLine();
            }

            courseService.AddStudent(name, surname, ageNum, groupNo, (StudentType)studentTypeNum);
        }

        static void ShowGroups(ref ICourseService courseService)
        {
            foreach (var item in courseService.Groups)
            {
                Console.WriteLine($"{item.No} {item.Limit} {item.Students.Length} {item.GroupType}");
            }
        }

        static void ShowStudentsByGroupNo(ref ICourseService courseService)
        {
            Console.WriteLine("Qrupu Secin: Nomresini Daxil Edin");
            foreach (Group group in courseService.Groups)
            {
                Console.WriteLine(group.No);
            }

            string groupNo = Console.ReadLine();

            while (!courseService.CheckgroupNo(groupNo))
            {
                Console.WriteLine("Duzgun Qrup Nomresini Daxil Et:");
                groupNo = Console.ReadLine();
            }

            Group selectedGroup = courseService.GetGroupByNo(groupNo);

            foreach (Student student in selectedGroup.Students)
            {
                Console.WriteLine($"{student.Id} {student.Name} {student.SurName} {student.Age} {student.GroupNo} {student.StudentType}");
            }
        }

        static void EditGroup(ref ICourseService courseService)
        {
            Console.WriteLine("Qrupu Secin: Nomresini Daxil Edin");
            foreach (Group group in courseService.Groups)
            {
                Console.WriteLine(group.No);
            }

            string groupNo = Console.ReadLine();

            while (!courseService.CheckgroupNo(groupNo))
            {
                Console.WriteLine("Duzgun Qrup Nomresini Daxil Et:");
                groupNo = Console.ReadLine();
            }

            Console.WriteLine("Groupub Novunu Sec Reqemi daxil Et");
            foreach (var item in Enum.GetValues(typeof(GroupType)))
            {
                Console.WriteLine((int)item + " " + item);
            }
            string grouptypestr = Console.ReadLine();
            int groupTypeNum;
            while (!int.TryParse(grouptypestr, out groupTypeNum) || groupTypeNum < 1 || groupTypeNum > 3)
            {
                //Console.BackgroundColor = ConsoleColor.Red;
                //Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Duzgun Group Novunun Nomresini Daxil Et");
                grouptypestr = Console.ReadLine();
            }

            Console.WriteLine("Qrupun Telebe Limitini Daxil Et");
            string limitstr = Console.ReadLine();
            byte limitNum;

            while (!byte.TryParse(limitstr, out limitNum) || limitNum < 12 || limitNum > 18)
            {
                Console.WriteLine("Duzgun Qrupun Telebe Limitini Daxil Et: Minimum 12 Maksimum 18 Ola Biler");
                limitstr = Console.ReadLine();
            }

            courseService.EditGroup(groupNo, (GroupType)groupTypeNum, limitNum);
        }

        static void EditStudent(ref ICourseService courseService)
        {
            Console.WriteLine("Qrupu Secin: Nomresini Daxil Edin");
            foreach (Group group in courseService.Groups)
            {
                Console.WriteLine(group.No);
            }

            string groupNo = Console.ReadLine();

            while (!courseService.CheckgroupNo(groupNo))
            {
                Console.WriteLine("Duzgun Qrup Nomresini Daxil Et:");
                groupNo = Console.ReadLine();
            }

            Group selectedGroup = courseService.GetGroupByNo(groupNo);

            Console.WriteLine("Duzelis Etmek Isdediyniz Telebeni Id-ni daxil Edin");
            foreach (Student student in selectedGroup.Students)
            {
                Console.WriteLine($"{student.Id} {student.Name} {student.SurName} {student.Age} {student.GroupNo} {student.StudentType}");
            }
            string idStr = Console.ReadLine();
            int idNum;

            while (!int.TryParse(idStr, out idNum) || !courseService.CheckStudnetById(idNum, groupNo))
            {
                Console.WriteLine("Duzgun Id Daxil Et");
                idStr = Console.ReadLine();
            }

            Console.WriteLine("Telebenin Adini Daxil Et:");
            string name = Console.ReadLine();

            Console.WriteLine("Telebenin Soyadini Daxil Et:");
            string surname = Console.ReadLine();

            Console.WriteLine("Telebenin Yasini Daxil Et:");
            string ageStr = Console.ReadLine();
            byte ageNum;
            while (!byte.TryParse(ageStr, out ageNum) || ageNum < 15 || ageNum > 50)
            {
                Console.WriteLine("Duzgun Yas Daxil Et: Minimum 15 Maksimum 50");
                ageStr = Console.ReadLine();
            }

            Console.WriteLine("Telebenin Zemanet Novunu Sec");
            foreach (var item in Enum.GetValues(typeof(StudentType)))
            {
                Console.WriteLine($"{(int)item} {item}");
            }
            string studentTypeStr = Console.ReadLine();
            int studentTypeNum;

            while (!int.TryParse(studentTypeStr, out studentTypeNum) || studentTypeNum < 1 || studentTypeNum > 2)
            {
                Console.WriteLine("Duzgun Secim Edin:");
                studentTypeStr = Console.ReadLine();
            }

            courseService.EditStudent(groupNo, idNum, name, surname, ageNum, (StudentType)studentTypeNum);
        }
    }
}
