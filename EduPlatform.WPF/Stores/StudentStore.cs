using EduPlatform.Domain.Commands;
using EduPlatform.Domain.Models;
using EduPlatform.Domain.Queries;
using EduPlatform.WPF.Service.Utilities;

namespace EduPlatform.WPF.Stores
{
    public class StudentStore(
        IGetAllStudentsQuery getAllStudentsQuery,
        ICreateStudentCommand createStudentCommand,
        IUpdateStudentCommand updateStudentCommand,
        IDeleteStudentCommand deleteStudentCommand)
    {
        public event Action? StudentsLoaded;
        public event Action<Student>? StudentAdded;
        public event Action<Student>? StudentUpdated;
        public event Action<Guid>? StudentDeleted;

        public IEnumerable<Student> Students => _students;
        private readonly List<Student> _students = [];

        public async Task Load()
        {
            IEnumerable<Student> studentsFromDb = await getAllStudentsQuery.ExecuteAsync();
            IEnumerable<Student> clonedStudents = studentsFromDb.Select(SerializationCopier.DeepCopy)!;

            _students.Clear();
            _students.AddRange(clonedStudents);

            StudentsLoaded?.Invoke();
        }

        public async Task Add(Student newStudent)
        {
            await createStudentCommand.ExecuteAsync(SerializationCopier.DeepCopy(newStudent)!);

            _students.Add(newStudent);

            StudentAdded?.Invoke(newStudent);
        }

        public async Task Update(Student targetStudent)
        {
            await updateStudentCommand.ExecuteAsync(SerializationCopier.DeepCopy(targetStudent)!);
            
            int index = _students.FindIndex(s => s.StudentId == targetStudent.StudentId);
            
            if (index == -1)
            {
                _students.Add(targetStudent);
            }
            else
            {
                _students[index] = targetStudent;
            }

            StudentUpdated?.Invoke(targetStudent);
        }

        public async Task Delete(Guid studentId)
        {
            await deleteStudentCommand.ExecuteAsync(studentId);

            _students.RemoveAll(s => s.StudentId == studentId);

            StudentDeleted?.Invoke(studentId);
        }
    }
}
