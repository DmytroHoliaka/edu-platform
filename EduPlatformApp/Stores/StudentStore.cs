using EduPlatform.WPF.Models;

namespace EduPlatform.WPF.Stores
{
    public class StudentStore
    {
        public event Action<Student>? StudentAdded;
        public event Action<Guid, Student>? StudentUpdated;
        public event Action<Guid>? StudentDeleted;

        public async Task Add(Student newStudent)
        {
            StudentAdded?.Invoke(newStudent);
        }

        public async Task Update(Guid sourceId, Student targetStudent)
        {
            StudentUpdated?.Invoke(sourceId, targetStudent);
        }

        public async Task Delete(Guid studentId)
        {
            StudentDeleted?.Invoke(studentId);
        }
    }
}
