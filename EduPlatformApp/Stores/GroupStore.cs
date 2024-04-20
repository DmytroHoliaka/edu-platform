using EduPlatform.Domain.Commands;
using EduPlatform.Domain.Models;
using EduPlatform.Domain.Queries;
using EduPlatform.WPF.Service;

namespace EduPlatform.WPF.Stores
{
    public class GroupStore(
        IGetAllGroupsQuery getAllGroupsQuery,
        ICreateGroupCommand createGroupCommand,
        IUpdateGroupCommand updateGroupCommand,
        IDeleteGroupCommand deleteGroupCommand)
    {
        public event Action<Group>? GroupAdded;
        public event Action<Group>? GroupUpdated;
        public event Action<Guid>? GroupDeleted;

        private readonly IGetAllGroupsQuery _getAllGroupsQuery = getAllGroupsQuery;


        public async Task Add(Group newGroup)
        {
            await createGroupCommand.ExecuteAsync(SerializationCopier.DeepCopy(newGroup)!);
            GroupAdded?.Invoke(newGroup);
        }

        public async Task Update(Group targetGroup)
        {
            await updateGroupCommand.ExecuteAsync(SerializationCopier.DeepCopy(targetGroup)!);
            GroupUpdated?.Invoke(targetGroup);
        }

        public async Task Delete(Guid groupId)
        {
            await deleteGroupCommand.ExecuteAsync(groupId);
            GroupDeleted?.Invoke(groupId);
        }
    }
}
