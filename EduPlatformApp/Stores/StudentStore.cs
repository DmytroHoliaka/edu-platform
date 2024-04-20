using EduPlatform.Domain.Commands;
using EduPlatform.Domain.Models;
using EduPlatform.Domain.Queries;
using EduPlatform.WPF.Service;

namespace EduPlatform.WPF.Stores
{
    public class StudentStore(
        IGetAllStudentsQuery getAllStudentsQuery,
        ICreateStudentCommand createStudentCommand,
        IUpdateStudentCommand updateStudentCommand,
        IDeleteStudentCommand deleteStudentCommand)
    {
        public event Action<Student>? StudentAdded;
        public event Action<Student>? StudentUpdated;
        public event Action<Guid>? StudentDeleted;

        private readonly IGetAllStudentsQuery _getAllStudentsQuery = getAllStudentsQuery;


        public async Task Add(Student newStudent)
        {
            await createStudentCommand.ExecuteAsync(SerializationCopier.DeepCopy(newStudent)!);
            StudentAdded?.Invoke(newStudent);
        }

        public async Task Update(Student targetStudent)
        {
            await updateStudentCommand.ExecuteAsync(SerializationCopier.DeepCopy(targetStudent)!);
            StudentUpdated?.Invoke(targetStudent);
        }

        public async Task Delete(Guid studentId)
        {
            await deleteStudentCommand.ExecuteAsync(studentId);
            StudentDeleted?.Invoke(studentId);
        }
    }
}
