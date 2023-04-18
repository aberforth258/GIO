using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using GIO.UI.Stores;
using GIO.UI.Views;
using GIO.UI.ViewModels;
using GIO.Models;

namespace GIO.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly NavigationStore _navigationStore;

        public App()
        {
            _navigationStore = new NavigationStore();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            using (GIOContext dbContext = new GIOContext())
            {
                if(!dbContext.Database.CanConnect() )
                {
                    MessageBox.Show("Cannot connect to the Database...");
                    Environment.Exit(0);
                }
            }

            _navigationStore.CurrentViewModel = new BookingListingViewModel(_navigationStore);
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
