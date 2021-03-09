using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Task9_1.Data;

namespace Task9_1.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new Task9_1Context(serviceProvider.GetRequiredService<DbContextOptions<Task9_1Context>>()))
            {
                if (context.Student.Any())
                {
                    return;
                }
                else
                {
                    context.Student.AddRange(
                        new Student
                        {
                            GroupID = 1,
                            FirstName = "Dmitriy",
                            LastName = "Test"
                        },
                        new Student
                        {
                            GroupID = 1,
                            FirstName = "Vladimir",
                            LastName = "Putin"
                        },
                        new Student
                        {
                            GroupID = 2,
                            FirstName = "Serg",
                            LastName = "Brin"
                        },
                        new Student
                        {
                            GroupID = 2,
                            FirstName = "Mark",
                            LastName = "Zckrbrg"
                        },
                        new Student
                        {
                            GroupID = 3,
                            FirstName = "Billy",
                            LastName = "Gates"
                        },
                        new Student
                        {
                            GroupID = 3,
                            FirstName = "Lary",
                            LastName = "Page"
                        },
                        new Student
                        {
                            GroupID = 4,
                            FirstName = "Richard",
                            LastName = " Hendrix"
                        },
                        new Student
                        {
                            GroupID = 4,
                            FirstName = "Jared",
                            LastName = "Dan"
                        }
                        );
                }

                if (context.Group.Any())
                {
                    return;
                }
                else 
                {
                    context.Group.AddRange(
                        new Group
                        {
                            CourseID = 1,
                            Name = "SR - 01"
                        },
                        new Group
                        {
                            CourseID = 1,
                            Name = "SR - 02"
                        },
                        new Group
                        {
                            CourseID = 2,
                            Name = "AR - 01"
                        },
                        new Group
                        {
                            CourseID = 2,
                            Name = "AR - 02"
                        }
                        );
                }

                if (context.Course.Any())
                    return;
                else
                {
                    context.Course.AddRange(
                        new Course
                        {
                            Name = "Test Faculty",
                            Description = " It's just a test"
                        },
                        new Course
                        {
                            Name = "Software Development",
                            Description = "Opisaniye"
                        }
                        );
                }
                context.SaveChanges();
            }
            
        }
    }
}
