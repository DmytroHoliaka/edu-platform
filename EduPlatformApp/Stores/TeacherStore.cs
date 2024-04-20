using EduPlatform.Domain.Commands;
using EduPlatform.Domain.Models;
using EduPlatform.Domain.Queries;
using EduPlatform.WPF.Service;

namespace EduPlatform.WPF.Stores
{
    public class TeacherStore(
        IGetAllTeachersQuery getAllTeachersQuery,
        ICreateTeacherCommand createTeacherCommand,
        IUpdateTeacherCommand updateTeacherCommand,
        IDeleteTeacherCommand deleteTeacherCommand)
    {
        public event Action<Teacher>? TeacherAdded;
        public event Action<Teacher>? TeacherUpdated;
        public event Action<Guid>? TeacherDeleted;

        private readonly IGetAllTeachersQuery _getAllTeachersQuery = getAllTeachersQuery;

        public async Task Add(Teacher newTeacher)
        {
            await createTeacherCommand.ExecuteAsync(SerializationCopier.DeepCopy(newTeacher)!);
            TeacherAdded?.Invoke(newTeacher);
        }

        public async Task Update(Teacher targetTeacher)
        {
            await updateTeacherCommand.ExecuteAsync(SerializationCopier.DeepCopy(targetTeacher)!);
            TeacherUpdated?.Invoke(targetTeacher);
        }

        public async Task Delete(Guid teacherId)
        {
            await deleteTeacherCommand.ExecuteAsync(teacherId);
            TeacherDeleted?.Invoke(teacherId);
        }
    }
}
