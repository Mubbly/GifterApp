using System;
using System.Linq;
using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Helpers
{
    public class DataInitializers
    {
        public static void MigrateDatabase(AppDbContext context)
        {
            context.Database.Migrate();
        }

        public static bool DeleteDatabase(AppDbContext context)
        {
            return context.Database.EnsureDeleted();
        }
        
        public static void SeedIdentity(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            var roles = new string[] {"user", "admin"};

            foreach (var roleName in roles)
            {
                var role = roleManager.FindByNameAsync(roleName).Result;
                if (role == null)
                {
                    role = new AppRole
                    {
                        Name = roleName
                    };

                    var result = roleManager.CreateAsync(role).Result;
                    if (!result.Succeeded) throw new ApplicationException("Role creation failed for: " + roleName);
                }
            }

            var users = new (string email, string username, string password, string firstName, string lastName, Guid Id)[]
            {
                ("test@test.com", "TestAccount", "Test123!", "Test", "Tester", new Guid("00000000-0000-0000-0000-000000000001"))
            };

            foreach (var userInfo in users)
            {
                var user = userManager.FindByNameAsync(userInfo.username).Result;
                if (user == null)
                {
                    user = new AppUser
                    {
                        Email = userInfo.email,
                        UserName = userInfo.username,
                        FirstName = userInfo.firstName,
                        LastName = userInfo.lastName
                    };
                    var result = userManager.CreateAsync(user, userInfo.password).Result;
                    if (!result.Succeeded)
                    {
                        throw new ApplicationException("User creation failed for: " + userInfo.username);
                    }
                }

                var roleResult = userManager.AddToRoleAsync(user, "admin").Result;
                roleResult = userManager.AddToRoleAsync(user, "user").Result;
            }
        }

        public static void SeedData(AppDbContext context, UserManager<AppUser> userManager)
        {
            var quizTypes = new[]
            {
                new QuizType
                {
                    Name = "Quiz",
                    Id = new Guid("00000000-0000-0000-0000-000000000001")
                },
                new QuizType
                {
                    Name = "Poll",
                    Id = new Guid("00000000-0000-0000-0000-000000000002")
                }
            };
            
            foreach (var quizType in quizTypes)
                if (!context.QuizTypes.Any(a => a.Id == quizType.Id))
                    context.QuizTypes.Add(quizType);
            context.SaveChanges();


            // EXAMPLE for when test user depends on some predefined data:
            // Test user related data
            var user = userManager.FindByEmailAsync("test@test.com").Result;
            if (user != null)
            {
                var quizzes = new[]
                {
                    new Quiz
                    {
                        Name = "Quiz",
                        Description = "Only one answer is correct for each question",
                        QuizTypeId =  new Guid("00000000-0000-0000-0000-000000000001"),
                        AppUserId = user!.Id,
                        Id = new Guid("00000000-0000-0000-0000-000000000001")
                    },
                    new Quiz
                    {
                        Name = "Poll",
                        Description = "There are no correct answers",
                        QuizTypeId = new Guid("00000000-0000-0000-0000-000000000002"),
                        AppUserId = user!.Id,
                        Id = new Guid("00000000-0000-0000-0000-000000000002")
                    }
                };
                
                foreach (var quiz in quizzes)
                    if (!context.Quizzes.Any(a => a.Id == quiz.Id))
                        context.Quizzes.Add(quiz);
                context.SaveChanges();
                
                var questions = new[]
                {
                    new Question
                    {
                        Name = "USA president",
                        QuizId =  new Guid("00000000-0000-0000-0000-000000000001"),
                        Id = new Guid("00000000-0000-0000-0000-000000000001")
                    },
                    new Question
                    {
                        Name = "Estonia president",
                        QuizId = new Guid("00000000-0000-0000-0000-000000000001"),
                        Id = new Guid("00000000-0000-0000-0000-000000000002")
                    },
                    new Question
                    {
                        Name = "Favorite flower",
                        QuizId = new Guid("00000000-0000-0000-0000-000000000002"),
                        Id = new Guid("00000000-0000-0000-0000-000000000003")
                    },
                    new Question
                    {
                        Name = "Favorite band",
                        QuizId = new Guid("00000000-0000-0000-0000-000000000002"),
                        Id = new Guid("00000000-0000-0000-0000-000000000004")
                    }
                };
                
                foreach (var question in questions)
                    if (!context.Questions.Any(a => a.Id == question.Id))
                        context.Questions.Add(question);
                context.SaveChanges();
                
                var answers = new[]
                {
                    new Answer
                    {
                        Name = "Donald Duck",
                        QuestionId =  new Guid("00000000-0000-0000-0000-000000000001"),
                        Id = new Guid("00000000-0000-0000-0000-000000000001")
                    },
                    new Answer
                    {
                        Name = "Donald Trump",
                        QuestionId = new Guid("00000000-0000-0000-0000-000000000001"),
                        Id = new Guid("00000000-0000-0000-0000-000000000002")
                    },
                    new Answer
                    {
                        Name = "Kristiina Ojuland",
                        QuestionId = new Guid("00000000-0000-0000-0000-000000000002"),
                        Id = new Guid("00000000-0000-0000-0000-000000000003")
                    },
                    new Answer
                    {
                        Name = "Kersti Kaljulaid",
                        QuestionId = new Guid("00000000-0000-0000-0000-000000000002"),
                        Id = new Guid("00000000-0000-0000-0000-000000000004")
                    },
                    new Answer
                    {
                        Name = "Tulip",
                        QuestionId = new Guid("00000000-0000-0000-0000-000000000003"),
                        Id = new Guid("00000000-0000-0000-0000-000000000005")
                    },
                    new Answer
                    {
                        Name = "Rose",
                        QuestionId = new Guid("00000000-0000-0000-0000-000000000003"),
                        Id = new Guid("00000000-0000-0000-0000-000000000006")
                    },
                    new Answer
                    {
                        Name = "Lord of the Lost",
                        QuestionId = new Guid("00000000-0000-0000-0000-000000000004"),
                        Id = new Guid("00000000-0000-0000-0000-000000000007")
                    },
                    new Answer
                    {
                        Name = "Justin Bieber",
                        QuestionId = new Guid("00000000-0000-0000-0000-000000000004"),
                        Id = new Guid("00000000-0000-0000-0000-000000000008")
                    },
                };
                
                foreach (var answer in answers)
                    if (!context.Answers.Any(a => a.Id == answer.Id))
                        context.Answers.Add(answer);
                context.SaveChanges();
                
                var quizResponses = new[]
                {
                    new QuizResponse
                    {
                        AppUserId = user!.Id,
                        AnswerId = new Guid("00000000-0000-0000-0000-000000000001"),
                        QuestionId = new Guid("00000000-0000-0000-0000-000000000001"),
                        QuizId = new Guid("00000000-0000-0000-0000-000000000001"),
                        Id = new Guid("00000000-0000-0000-0000-000000000001")
                    },
                    new QuizResponse
                    {
                        AppUserId = user!.Id,
                        AnswerId = new Guid("00000000-0000-0000-0000-000000000004"),
                        QuestionId = new Guid("00000000-0000-0000-0000-000000000002"),
                        QuizId = new Guid("00000000-0000-0000-0000-000000000001"),
                        Id = new Guid("00000000-0000-0000-0000-000000000002")
                    },
                    new QuizResponse
                    {
                        AppUserId = user!.Id,
                        AnswerId = new Guid("00000000-0000-0000-0000-000000000006"),
                        QuestionId = new Guid("00000000-0000-0000-0000-000000000003"),
                        QuizId = new Guid("00000000-0000-0000-0000-000000000002"),
                        Id = new Guid("00000000-0000-0000-0000-000000000003")
                    },
                    new QuizResponse
                    {
                        AppUserId = user!.Id,
                        AnswerId = new Guid("00000000-0000-0000-0000-000000000007"),
                        QuestionId = new Guid("00000000-0000-0000-0000-000000000004"),
                        QuizId = new Guid("00000000-0000-0000-0000-000000000002"),
                        Id = new Guid("00000000-0000-0000-0000-000000000004")
                    }
                };
                
                foreach (var quizResponse in quizResponses)
                    if (!context.QuizResponses.Any(a => a.Id == quizResponse.Id))
                        context.QuizResponses.Add(quizResponse);
                context.SaveChanges();
            }
        }
    }
}