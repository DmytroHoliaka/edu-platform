﻿using EduPlatform.WPF.Commands.BaseCommands;
using EduPlatform.WPF.Stores;

namespace EduPlatform.WPF.Commands.GeneralCommands
{
    public class CloseFormCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;

        public CloseFormCommand(ModalNavigationStore modalNavigationStore)
        {
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object? parameter)
        {
            _modalNavigationStore.Close();
        }
    }
}