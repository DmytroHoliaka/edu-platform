using EduPlatform.Domain.Models;

namespace EduPlatform.WPF.Stores
{
    public class GroupStore
    {
        public event Action<Group>? GroupAdded;
        public event Action<Group>? GroupUpdated;
        public event Action<Guid>? GroupDeleted;

        public async Task Add(Group newGroup)
        {
            GroupAdded?.Invoke(newGroup);
        }

        public async Task Update(Group targetGroup)
        {
            GroupUpdated?.Invoke(targetGroup);
        }

        public async Task Delete(Guid groupId)
        {
            GroupDeleted?.Invoke(groupId);
        }
    }
}
