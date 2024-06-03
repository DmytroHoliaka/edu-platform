using EduPlatform.Domain.Models;

namespace EduPlatform.WPF.Tests.UnitTesting.xUnit.Utilities;

public static class ModelGenerator
{    
    public static Group GetPlugGroup(string groupName)
    {
        Group group = new()
        {
            Name = groupName
        };

        return group;
    }

    public static Group GetPopulatedGroup(string courseName, string groupName)
    {
        Group group = new()
        {
            Name = groupName,
            Course = new Course()
            {
                Name = courseName
            },
            Students =
            [
                new Student()
                {
                    FirstName = "John",
                    LastName = "Doe",
                },
                new Student()
                {
                    FirstName = "Alex",
                    LastName = "Jordan",
                },
            ]
        };

        return group;
    }


}