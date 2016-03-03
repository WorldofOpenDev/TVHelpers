﻿using MediaAppSample.Core;
using MediaAppSample.Core.ViewModels;
using MediaAppSample.UI.Controls;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

namespace MediaAppSample.UI.Views
{
    public abstract class ShellViewBase : ViewBase<ShellViewModel>
    {
    }
    
    public sealed partial class ShellView : ShellViewBase
    {
        #region Constructors

        public ShellView()
        {
            this.InitializeComponent();

            SuspensionManager.RegisterFrame(bodyFrame, "ShellBodyFrame");

            this.Loaded += (sender, e) =>
            {
                bodyFrame.Navigated += BodyFrame_Navigated;
                this.UpdateSelectedMenuItem();
            };

            this.Unloaded += (sender, e) => 
            {
                bodyFrame.Navigated -= BodyFrame_Navigated;
            };
        }

        #endregion

        #region Methods

        protected override void OnApplicationResuming()
        {
            // Set the child frame to the navigation service so that it can appropriately perform navigation of pages to the desired frame.
            this.Frame.SetSubFrame(bodyFrame);

            base.OnApplicationResuming();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Set the child frame to the navigation service so that it can appropriately perform navigation of pages to the desired frame.
            this.Frame.SetSubFrame(bodyFrame);

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            // Remove the bodyFrame as the childFrame
            this.Frame.SetSubFrame(null);
        }

        protected override Task OnLoadStateAsync(LoadStateEventArgs e)
        {
            if (e.NavigationEventArgs.NavigationMode == NavigationMode.New || this.ViewModel == null)
                this.SetViewModel(new ShellViewModel());

            return base.OnLoadStateAsync(e);
        }

        /// <summary>
        /// Update the selected item in the shell navigation menu based on watching the body frame navigated event.
        /// </summary>
        private void UpdateSelectedMenuItem()
        {
            var view = bodyFrame.Content;
            if (view is SettingsView)
            {
                lvMenuTop.SelectedItem = null;
                lvMenuBottom.SelectedItem = btnSettings;
            }
            else if (view is SearchView)
            {
                lvMenuTop.SelectedItem = btnSearch;
                lvMenuBottom.SelectedItem = null;
            }
            else if (view is MainView || view is ItemView)
            {
                lvMenuTop.SelectedItem = btnHome;
                lvMenuBottom.SelectedItem = null;
            }
        }

        #endregion

        #region Event Handlers

        private async void BodyFrame_Navigated(object sender, NavigationEventArgs e)
        {
            // Update the selected item when a page navigation occurs in the body frame
            await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                this.UpdateSelectedMenuItem();
            });
        }

        /// <summary>
        /// Executes the command associated to the navigation menu item clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvMenu_ItemClick(object sender, Windows.UI.Xaml.Controls.ItemClickEventArgs e)
        {
            var item = e.ClickedItem as SplitViewNavigationButton;
            item?.Command.Execute(item?.CommandParameter);
        }

        #endregion
    }
}
