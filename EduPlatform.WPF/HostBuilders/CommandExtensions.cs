using EduPlatform.Domain.Commands;
using EduPlatform.EntityFramework.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EduPlatform.WPF.HostBuilders;

public static class CommandExtensions
{
    public static IHostBuilder AddCommands(this IHostBuilder? hostBuilder)
    {
        ArgumentNullException.ThrowIfNull(hostBuilder, nameof(hostBuilder));

        hostBuilder.ConfigureServices(
            (context, services) =>
            {
                services.AddSingleton<ICreateCourseCommand, CreateCourseCommand>();
                services.AddSingleton<IUpdateCourseCommand, UpdateCourseCommand>();
                services.AddSingleton<IDeleteCourseCommand, DeleteCourseCommand>();


                services.AddSingleton<ICreateGroupCommand, CreateGroupCommand>();
                services.AddSingleton<IUpdateGroupCommand, UpdateGroupCommand>();
                services.AddSingleton<IDeleteGroupCommand, DeleteGroupCommand>();


                services.AddSingleton<ICreateStudentCommand, CreateStudentCommand>();
                services.AddSingleton<IUpdateStudentCommand, UpdateStudentCommand>();
                services.AddSingleton<IDeleteStudentCommand, DeleteStudentCommand>();


                services.AddSingleton<ICreateTeacherCommand, CreateTeacherCommand>();
                services.AddSingleton<IUpdateTeacherCommand, UpdateTeacherCommand>();
                services.AddSingleton<IDeleteTeacherCommand, DeleteTeacherCommand>();
            });
        
        return hostBuilder;
    }
}