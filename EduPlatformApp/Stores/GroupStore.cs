using EduPlatform.WPF.Models;

namespace EduPlatform.WPF.Stores
{
    public class GroupStore
    {
        public event Action<Group>? GroupAdded;
        public event Action<Guid, Group>? GroupUpdated;

        public async Task Add(Group newGroup)
        {
            GroupAdded?.Invoke(newGroup);
        }

        public async Task Update(Guid sourceId, Group targetGroup)
        {
            GroupUpdated?.Invoke(sourceId, targetGroup);
        }
    }
}
