using EduPlatform.WPF.Models;

namespace EduPlatform.WPF.Stores
{
    public class TeacherStore
    {
        public event Action<Teacher>? TeacherAdded;
        public event Action<Teacher>? TeacherUpdated;
        public event Action<Guid>? TeacherDeleted;

        public async Task Add(Teacher newTeacher)
        {
            TeacherAdded?.Invoke(newTeacher);
        }

        public async Task Update(Teacher targetTeacher)
        {
            TeacherUpdated?.Invoke(targetTeacher);
        }

        public async Task Delete(Guid teacherId)
        {
            TeacherDeleted?.Invoke(teacherId);
        }
    }
}
