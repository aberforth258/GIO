using GIO.UI.Stores;
using GIO.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIO.UI.Commands
{
    class NavigateCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly ViewModelBase viewModel;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="navigationStore">Navigation Store object</param>
        /// <param name="viewModel">View to be navigated to</param>
        public NavigateCommand(NavigationStore navigationStore, ViewModelBase viewModel)
        {
            _navigationStore = navigationStore;
            this.viewModel = viewModel;
        }
        /// <summary>
        /// Navigates to 
        /// </summary>
        /// <param name="parameter"></param>
        public override void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel = viewModel; 
        }
    }
}
