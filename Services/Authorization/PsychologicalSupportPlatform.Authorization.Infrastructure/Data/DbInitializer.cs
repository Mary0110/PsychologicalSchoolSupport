using PsychologicalSupportPlatform.Authorization.Domain.Entities;

namespace PsychologicalSupportPlatform.Authorization.Infrastructure.Data;

public static class DbInitializer
{
    public static async Task SeedData(DataContext context)
    {
        if (!context.Users.Any())
        {
            var adminUser = new User()
            {
                Email = "mashenka0110@gmail.com",
                Password = "rJaJ4ickJwheNbnT4+i+2IyzQ0gotDuG/AWWytTG4nA=",
                Name = "Maria",
                Surname = "Raniuk",
                Patronymic = "Dmitrievna",
                Role = Role.Admin
            };

            var Natalia = new User()
            {
                Email = "nata@gmail.com",
                Password = "SMRJR4jgSEAQxcJ0Ojkwq5TxNXx4UoPTUo28Hb4+jF8=",
                Name = "Natalia",
                Surname = "Raniuk",
                Patronymic = "Nikolaevna",
                Role = Role.Manager,
            };

            var Psychologist = new User()
            {
                Email = "polina@gmail.com",
                Password = "MEmjge84+knLBnRR2GrnPD12pZ8niQSijLStq4PJrTY=",
                Name = "Polina",
                Surname = "Prokazova",
                Patronymic = "Petrovna",
                Role = Role.Psychologist,
            };
            context.Users.AddRange(new User[] { adminUser, Natalia, Psychologist });
            await context.SaveChangesAsync();
        }
        
        var firstA = new Form()
        {
            Letter = 'A',
            Parallel = 1
        };
        
        if (!context.Forms.Any())
        {
            context.Forms.AddRange(new Form[] { firstA });
            await context.SaveChangesAsync();
        }

        if (!context.Students.Any())
        {
            var stud1 = new Student()
            {
                Email = "stud1@gmail.com",
                Password = "MEmjge84+knLBnRR2GrnPD12pZ8niQSijLStq4PJrTY=",
                Name = "Max",
                Surname = "Maximov",
                Patronymic = "Maximovich",
                DateOfBirth = (DateTime.Now).Date,
                Form = firstA,
                Role = Role.Student
            };

            context.Students.AddRange(new Student[] { stud1 });
            await context.SaveChangesAsync();
        }
    }
}
