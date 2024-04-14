﻿using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.WPF.Stores;
using EduPlatform.WPF.ViewModels.CoursesViewModels;
using EduPlatform.WPF.ViewModels.GroupsViewModels;
using EduPlatform.WPF.ViewModels.StudentsViewModel;
using EduPlatform.WPF.ViewModels.TeachersViewModel;

namespace EduPlatform.WPF.Commands.GroupCommands
{
    public class OpenUpdateGroupFormCommand : CommandBase
    {
        public GroupViewModel? UpdatingGroup { get; set; }

        private readonly GroupStore _groupStore;
        private readonly ViewStore _viewStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly CourseSequenceViewModel _courseSequenceVM;
        private readonly TeacherSequenceViewModel _teacherSequenceVM;
        private readonly StudentSequenceViewModel _studentSequenceVM;

        public OpenUpdateGroupFormCommand
        (
            GroupStore groupStore,
            ViewStore viewStore,
            ModalNavigationStore modalNavigationStore,
            CourseSequenceViewModel courseSequenceVM,
            TeacherSequenceViewModel teacherSequenceVM,
            StudentSequenceViewModel studentSequenceVM
        )
        {
            _groupStore = groupStore;
            _viewStore = viewStore;
            _modalNavigationStore = modalNavigationStore;
            _courseSequenceVM = courseSequenceVM;
            _teacherSequenceVM = teacherSequenceVM;
            _studentSequenceVM = studentSequenceVM;
        }

        public override bool CanExecute(object? parameter)
        {
            return UpdatingGroup is not null;
        }

        public override void Execute(object? parameter)
        {
            UpdateGroupViewModel updateGroupViewModel = new
            (
                UpdatingGroup!,
                _modalNavigationStore,
                _courseSequenceVM,
                _teacherSequenceVM,
                _studentSequenceVM,
                _groupStore
            );

            _modalNavigationStore.CurrentViewModel = updateGroupViewModel;
            _viewStore.UnfocuseGroup();
        }
    }
}
