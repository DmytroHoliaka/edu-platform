using EduPlatform.Domain.Commands;
using EduPlatform.Domain.Models;
using EduPlatform.Domain.Queries;
using EduPlatform.WPF.Service.Utilities;

namespace EduPlatform.WPF.Stores
{
    public class GroupStore(
        IGetAllGroupsQuery getAllGroupsQuery,
        ICreateGroupCommand createGroupCommand,
        IUpdateGroupCommand updateGroupCommand,
        IDeleteGroupCommand deleteGroupCommand)
    {
        public event Action? GroupsLoaded;
        public event Action<Group>? GroupAdded;
        public event Action<Group>? GroupUpdated;
        public event Action<Guid>? GroupDeleted;

        public IEnumerable<Group> Groups => _groups;
        private readonly List<Group> _groups = [];

        public async Task Load()
        {
            IEnumerable<Group> groupsFromDb = await getAllGroupsQuery.ExecuteAsync();
            IEnumerable<Group> clonedGroups = groupsFromDb.Select(SerializationCopier.DeepCopy)!;

            _groups.Clear();
            _groups.AddRange(clonedGroups);

            GroupsLoaded?.Invoke();
        }

        public async Task Add(Group newGroup)
        {
            await createGroupCommand.ExecuteAsync(SerializationCopier.DeepCopy(newGroup)!);

            _groups.Add(newGroup);

            GroupAdded?.Invoke(newGroup);
        }

        public async Task Update(Group targetGroup)
        {
            await updateGroupCommand.ExecuteAsync(SerializationCopier.DeepCopy(targetGroup)!);

            int index = _groups.FindIndex(g => g.GroupId == targetGroup.GroupId);

            if (index == -1)
            {
                _groups.Add(targetGroup);
            }
            else
            {
                _groups[index] = targetGroup;
            }

            GroupUpdated?.Invoke(targetGroup);
        }

        public async Task Delete(Guid groupId)
        {
            await deleteGroupCommand.ExecuteAsync(groupId);

            _groups.RemoveAll(g => g.GroupId == groupId);

            GroupDeleted?.Invoke(groupId);
        }
    }
}
