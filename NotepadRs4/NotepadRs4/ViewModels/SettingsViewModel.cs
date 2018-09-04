﻿using System;
using System.Windows.Input;

using NotepadRs4.Helpers;
using NotepadRs4.Services;

using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NotepadRs4.ViewModels
{
    // TODO WTS: Add other settings as necessary. For help see https://github.com/Microsoft/WindowsTemplateStudio/blob/master/docs/pages/settings.md
    public class SettingsViewModel : Observable
    {
        private ElementTheme _elementTheme = ThemeSelectorService.Theme;
        public ElementTheme ElementTheme
        {
            get { return _elementTheme; }

            set { Set(ref _elementTheme, value); }
        }

        private string _versionDescription;
        public string VersionDescription
        {
            get { return _versionDescription; }

            set { Set(ref _versionDescription, value); }
        }

        private ICommand _switchThemeCommand;
        public ICommand SwitchThemeCommand
        {
            get
            {
                if (_switchThemeCommand == null)
                {
                    _switchThemeCommand = new RelayCommand<ElementTheme>(
                        async (param) =>
                        {
                            ElementTheme = param;
                            await ThemeSelectorService.SetThemeAsync(param);
                        });
                }

                return _switchThemeCommand;
            }
        }

        private ICommand _navigateBackCommand;
        public ICommand NavigateBackCommand
        {
            get
            {
                if (_navigateBackCommand == null)
                {
                    _navigateBackCommand = new RelayCommand(
                        async () =>
                        {
                            NavigateBack();
                        });
                }
                return _navigateBackCommand;
            }
        }

        public SettingsViewModel()
        {
        }

        public void Initialize()
        {
            VersionDescription = GetVersionDescription();
        }

        // Methods
        private string GetVersionDescription()
        {
            var appName = "AppDisplayName".GetLocalized();
            var package = Package.Current;
            var packageId = package.Id;
            var version = packageId.Version;

            return $"{appName} - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
        }

        private void NavigateBack()
        {
            if (NavigationService.CanGoBack == true)
            {
                NavigationService.GoBack();
            }
        }

        /*
        private void CloseDialog(ContentDialog dialog)
        {
            if (dialog != null)
            {
                dialog.Hide();
            }
        } */
    }
}
