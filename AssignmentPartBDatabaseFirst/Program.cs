using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AssignmentPartBDatabaseFirst
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AssignmentPartBEntities2 db = new AssignmentPartBEntities2();

            DbSet<Student> AllStudents = db.Students; //Edo den exei fortothei tipota stin mnimi!!!!!
            DbSet<Trainer> AllTrainers = db.Trainers;
            DbSet<Course> AllCourses = db.Courses;
            DbSet<Assignment> AllAssignments = db.Assignments;

            // print all students
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("All Students: ");
            Console.ResetColor();

            foreach (var student in AllStudents)
            {
                Console.Write(student.StudentID + " ");
                Console.Write(student.FirstName + " ");
                Console.WriteLine(student.LastName);
            }
            Console.WriteLine();

            // print all trainers
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("All Trainers:");
            Console.ResetColor();
            foreach (var trainer in AllTrainers)
            {
                Console.Write(trainer.TrainerID + " ");
                Console.Write(trainer.FirstName + " ");
                Console.WriteLine(trainer.LastName);
            }
            Console.WriteLine();

            // print all courses
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("All Courses:");
            Console.ResetColor();

            foreach (var course in AllCourses)
            {
                Console.Write(course.CourseID + " ");
                Console.Write(course.Title + " ");
                Console.Write(course.TheStream + " ");
                Console.WriteLine(course.TheType);
            }
            Console.WriteLine();

            // print all assignments
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("All Assignments:");
            Console.ResetColor();

            foreach (var assignment in AllAssignments)
            {
                Console.Write(assignment.AssignmentID + " ");
                Console.Write(assignment.Title + " ");
                Console.WriteLine(assignment.TheDescription);
            }
            Console.WriteLine();

            // print all students per course
            var studentsPerCourse = db.Courses.Include(c => c.Students).ToList();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("All Students Per Course:");
            Console.ResetColor();
            foreach (var course in studentsPerCourse)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"{course.Title}:");
                Console.ResetColor();
                foreach (var student in course.Students)
                {
                    Console.WriteLine($"{student.LastName}");
                }
            }

            // print all students per course
            var trainersPerCourse = db.Courses.Include(c => c.Trainers).ToList();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("All Trainers Per Course:");
            Console.ResetColor();
            foreach (var course in trainersPerCourse)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"{course.Title}:");
                Console.ResetColor();
                foreach (var trainer in course.Trainers)
                {
                    Console.WriteLine($"{trainer.LastName}");
                }
            }

            // print all assignments per course
            var assignmentsPerCourse = db.Courses.Include(c => c.Assignments).ToList();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("All Assignments Per Course:");
            Console.ResetColor();
            foreach (var course in assignmentsPerCourse)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"{course.Title}:");
                Console.ResetColor();
                foreach (var assignment in course.Assignments)
                {
                    Console.WriteLine($"{assignment.Title}");
                }
            }

            // print all assignments per course per student
            var students = db.Students
                .Include(s => s.Courses.Select(c => c.Assignments)) 
                .ToList();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("All Assignments Per Course Per Student:");
            Console.ResetColor();

            foreach (var student in students)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{student.LastName}");
                Console.ResetColor();

                foreach (var course in student.Courses)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"{course.Title}");
                    Console.ResetColor();

                    foreach (var assignment in course.Assignments)
                    {
                        Console.WriteLine($"{assignment.Title}");
                    }

                    Console.WriteLine(); 
                }

                Console.WriteLine(); 
            }

            // print students that belong to more than one course

            var students2 = db.Students.Where(s=>s.Courses.Count() > 1).ToList();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Students that belong to more than one course:");
            Console.ResetColor();
            foreach (var student in students2)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"{ student.LastName}");
                Console.ResetColor();
            }

            //insert trainer from the keyboard
            Trainer tr = new Trainer();
            Console.WriteLine("Enter Trainer's Firstname:");
            tr.FirstName = Console.ReadLine();
            Console.WriteLine("Enter Trainer's Lastname:");
            tr.LastName = Console.ReadLine();
            Console.WriteLine("Enter Trainer's Subject:");
            tr.Subject = Console.ReadLine();
            Console.WriteLine("Enter Trainer's ID:");
            tr.TrainerID = Convert.ToInt32(Console.ReadLine());
            db.Trainers.Add(tr);
            db.SaveChanges();
        }
    }
}
