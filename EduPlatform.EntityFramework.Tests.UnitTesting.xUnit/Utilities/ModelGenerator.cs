using EduPlatform.Domain.Models;

namespace EduPlatform.EntityFramework.Tests.UnitTesting.xUnit.Utilities;

public static class ModelGenerator
{
    private static List<Course> Courses =>
    [
        new Course()
        {
            CourseId = Guid.NewGuid(),
            Name = "Introduction to Computer Science",
            Description = "An introductory course on computer science principles.",
            Groups = new List<Group>()
        },

        new Course()
        {
            CourseId = Guid.NewGuid(),
            Name = "Advanced Mathematics",
            Description = "A course on advanced mathematical concepts and techniques.",
            Groups = new List<Group>()
        },

        new Course()
        {
            CourseId = Guid.NewGuid(),
            Name = "Physics for Engineers",
            Description = "A comprehensive course on physics with applications in engineering.",
            Groups = new List<Group>()
        },

        new Course()
        {
            CourseId = Guid.NewGuid(),
            Name = "Literature and Composition",
            Description = "An exploration of literary works and composition techniques.",
            Groups = new List<Group>()
        }
    ];

    private static List<Group> Groups =>
    [
        new Group()
        {
            GroupId = Guid.NewGuid(),
            Name = "Group A",
            CourseId = null,
            Course = null,
            Teachers = new List<Teacher>(),
            Students = new List<Student>()
        },

        new Group()
        {
            GroupId = Guid.NewGuid(),
            Name = "Group B",
            CourseId = null,
            Course = null,
            Teachers = new List<Teacher>(),
            Students = new List<Student>()
        },

        new Group()
        {
            GroupId = Guid.NewGuid(),
            Name = "Group C",
            CourseId = null,
            Course = null,
            Teachers = new List<Teacher>(),
            Students = new List<Student>()
        },

        new Group()
        {
            GroupId = Guid.NewGuid(),
            Name = "Group D",
            CourseId = null,
            Course = null,
            Teachers = new List<Teacher>(),
            Students = new List<Student>()
        }
    ];

    private static List<Student> Students =>
    [
        new Student()
        {
            StudentId = Guid.NewGuid(),
            FirstName = "Alice",
            LastName = "Johnson",
            GroupId = null,
            Group = null,
        },

        new Student()
        {
            StudentId = Guid.NewGuid(),
            FirstName = "Bob",
            LastName = "Smith",
            GroupId = null,
            Group = null,
        },

        new Student()
        {
            StudentId = Guid.NewGuid(),
            FirstName = "Charlie",
            LastName = "Brown",
            GroupId = null,
            Group = null,
        },

        new Student()
        {
            StudentId = Guid.NewGuid(),
            FirstName = "David",
            LastName = "Williams",
            GroupId = null,
            Group = null,
        }
    ];

    private static List<Teacher> Teachers =>
    [
        new Teacher()
        {
            TeacherId = Guid.NewGuid(),
            FirstName = "John",
            LastName = "Doe",
            Groups = new List<Group>()
        },

        new Teacher()
        {
            TeacherId = Guid.NewGuid(),
            FirstName = "Alex",
            LastName = "Jordan",
            Groups = new List<Group>()
        },

        new Teacher()
        {
            TeacherId = Guid.NewGuid(),
            FirstName = "Tom",
            LastName = "Adams",
            Groups = new List<Group>()
        },

        new Teacher()
        {
            TeacherId = Guid.NewGuid(),
            FirstName = "Benedict",
            LastName = "Flanders",
            Groups = new List<Group>()
        }
    ];

    public static List<Course> GetPopulatedCourses()
    {
        return [];
    }

    public static List<Group> GetPopulatedGroups()
    {
        List<Group> groups = Groups;

        groups[0].Course = Courses[0];
        groups[0].Students = [Students[0]];
        groups[0].Teachers = [Teachers[0], Teachers[1]];

        groups[1].Course = Courses[1];
        groups[1].Students = [Students[1]];
        groups[1].Teachers = [Teachers[2], Teachers[3]];

        groups[2].Course = Courses[2];
        groups[2].Students = [Students[2]];
        groups[2].Teachers = [Teachers[1]];

        groups[3].Course = Courses[3];
        groups[3].Students = [Students[3]];
        groups[3].Teachers = [Teachers[0], Teachers[1], Teachers[2], Teachers[3]];

        return groups;
    }

    public static List<Student> GetPopulatedStudents()
    {
        List<Student> unfilledStudents = Students;

        unfilledStudents[0].Group = Groups[0];
        unfilledStudents[1].Group = Groups[1];
        unfilledStudents[2].Group = Groups[2];
        unfilledStudents[3].Group = Groups[3];

        return unfilledStudents;
    }

    public static List<Teacher> GetPopulatedTeachers()
    {
        List<Teacher> unfilledTeachers = Teachers;

        unfilledTeachers[0].Groups = [Groups[0], Groups[1]];
        unfilledTeachers[1].Groups = [Groups[0], Groups[3]];
        unfilledTeachers[2].Groups = [Groups[0]];
        unfilledTeachers[3].Groups = [Groups[0], Groups[1], Groups[2], Groups[3]];

        return unfilledTeachers;
    }

    public static Course GetPopulatedCourse()
    {
        Course unfilledCourse = Courses[0];
        unfilledCourse.Groups = [Groups[0], Groups[1], Groups[2], Groups[3]];

        return unfilledCourse;
    }

    public static Group GetPopulatedGroup()
    {
        Group unfilledGroup = Groups[0];
        
        unfilledGroup.Course = Courses[0];
        unfilledGroup.Teachers = [Teachers[0], Teachers[1], Teachers[2], Teachers[3]];
        unfilledGroup.Students = [Students[0], Students[1], Students[2], Students[3]];

        return unfilledGroup;
    }

    public static Student GetPopulatedStudent()
    {
        Student unfilledStudent = Students[0];
        unfilledStudent.Group = Groups[0];

        return unfilledStudent;
    }

    public static Teacher GetPopulatedTeacher()
    {
        Teacher unfilledTeacher = Teachers[0];
        unfilledTeacher.Groups = [Groups[0], Groups[1], Groups[2], Groups[3]];

        return unfilledTeacher;
    }

    public static Course GetUnfilledCourse()
    {
        Course unfilledCourse = Courses[0];
        return unfilledCourse;
    }

    public static Group GetUnfilledGroup()
    {
        Group unfilledGroup = Groups[0];
        return unfilledGroup;
    }

    public static Student GetUnfilledStudent()
    {
        Student unfilledStudent = Students[0];
        return unfilledStudent;
    }

    public static Teacher GetUnfilledTeacher()
    {
        Teacher unfilledTeacher = Teachers[0];
        return unfilledTeacher;
    }
}