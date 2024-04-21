using EduPlatform.Domain.Commands;
using EduPlatform.Domain.Models;
using EduPlatform.Domain.Queries;
Stores: Add event GroupsLoaded and sequence Groups in GroupStoreusing EduPlatform.EntityFramework.Queries;
using EduPlatform.WPF.Service;

namespace EduPlatform.WPF.Stores
{
    public class TeacherStore(
        IGetAllTeachersQuery getAllTeachersQuery,
        ICreateTeacherCommand createTeacherCommand,
        IUpdateTeacherCommand updateTeacherCommand,
        IDeleteTeacherCommand deleteTeacherCommand)
    {
        public event Action? TeachersLoaded;
        public event Action<Teacher>? TeacherAdded;
        public event Action<Teacher>? TeacherUpdated;
        public event Action<Guid>? TeacherDeleted;

        public IEnumerable<Teacher> Teachers => _teachers;
        private readonly List<Teacher> _teachers = [];

        public async Task Load()
        {
            IEnumerable<Teacher> teachersFromDb = await getAllTeachersQuery.ExecuteAsync();

            _teachers.Clear();
            _teachers.AddRange(teachersFromDb);

            TeachersLoaded?.Invoke();
        }

        public async Task Add(Teacher newTeacher)
        {
            await createTeacherCommand.ExecuteAsync(SerializationCopier.DeepCopy(newTeacher)!);

            _teachers.Add(newTeacher);

            TeacherAdded?.Invoke(newTeacher);
        }

        public async Task Update(Teacher targetTeacher)
        {
            await updateTeacherCommand.ExecuteAsync(SerializationCopier.DeepCopy(targetTeacher)!);

            int index = _teachers.FindIndex(t => t.TeacherId == targetTeacher.TeacherId);

            if (index == -1)
            {
                _teachers.Add(targetTeacher);
            }
            else
            {
                _teachers[index] = targetTeacher;
            }

            TeacherUpdated?.Invoke(targetTeacher);
        }

        public async Task Delete(Guid teacherId)
        {
            await deleteTeacherCommand.ExecuteAsync(teacherId);

            _teachers.RemoveAll(t => t.TeacherId == teacherId);

            TeacherDeleted?.Invoke(teacherId);
        }
    }
}
