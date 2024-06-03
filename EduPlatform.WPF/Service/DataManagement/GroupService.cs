using System.Windows.Forms;
using EduPlatform.Domain.Models;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.GroupsViewModels;

namespace EduPlatform.WPF.Service.DataManagement;

public static class GroupService
{
    public static async Task<object> RemoveStudentsFromGroup(GroupViewModel groupVM, GroupStore groupStore)
    {
        Group targetGroup = new()
        {
            GroupId = groupVM.GroupId,
            Name = groupVM.GroupName,
            CourseId = groupVM.CourseId,
            Course = groupVM.Group.Course,
            Teachers = groupVM.Group.Teachers,
            Students = []
        };

        try
        {
            await groupStore.Update(targetGroup);
        }
        catch (Exception)
        {
            MessageBox.Show("Cannot remove all students from group before import.");
            return new object();
        }

        return targetGroup;
    }
}