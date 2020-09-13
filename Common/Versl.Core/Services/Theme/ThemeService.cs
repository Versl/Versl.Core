using System;
using Versl.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Versl.Services.Theme
{
    public class ThemeService : IThemeService
    {
        private readonly ILayoutConfiguration _layoutConfiguration;        

        public ThemeService(ILayoutConfiguration layoutConfiguration)
        {
            _layoutConfiguration = layoutConfiguration;

            Application.Current.RequestedThemeChanged += Current_RequestedThemeChanged;
            Init();
        }

        public void Init()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                SetTheme(Application.Current.RequestedTheme);
            });
        }

        private void Current_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
        {
            SetTheme(e.RequestedTheme);
        }

        private void SetTheme(OSAppTheme requestedTheme)
        {
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(GetTheme(requestedTheme));
        }

        private ResourceDictionary GetTheme(OSAppTheme requestedTheme)
        {
            if (requestedTheme == OSAppTheme.Dark)
            {
                return GetDarkTheme();
            }

            return GetLightTheme();
        }

        private ResourceDictionary GetDarkTheme()
        {
            var themeDictionary = new ResourceDictionary();

            themeDictionary.Add("PrimaryColor", _layoutConfiguration.Colors.DarkPrimaryColor);
            themeDictionary.Add("OnPrimaryColor", _layoutConfiguration.Colors.DarkOnPrimaryColor);
            themeDictionary.Add("PrimaryVariantColor", _layoutConfiguration.Colors.DarkPrimaryVariantColor);
            themeDictionary.Add("OnPrimaryVariantColor", _layoutConfiguration.Colors.DarkOnPrimaryVariantColor);
            themeDictionary.Add("SecondaryColor", _layoutConfiguration.Colors.DarkSecondaryColor);
            themeDictionary.Add("OnSecondaryColor", _layoutConfiguration.Colors.DarkOnSecondaryColor);
            themeDictionary.Add("SecondaryVariantColor", _layoutConfiguration.Colors.DarkSecondaryVariantColor);
            themeDictionary.Add("OnSecondaryVariantColor", _layoutConfiguration.Colors.DarkOnSecondaryVariantColor);
            themeDictionary.Add("BackgroundColor", _layoutConfiguration.Colors.DarkBackgroundColor);
            themeDictionary.Add("OnBackgroundColor", _layoutConfiguration.Colors.DarkOnBackgroundColor);
            themeDictionary.Add("SurfaceColor", _layoutConfiguration.Colors.DarkSurfaceColor);
            themeDictionary.Add("OnSurfaceColor", _layoutConfiguration.Colors.DarkOnSurfaceColor);
            themeDictionary.Add("ErrorColor", _layoutConfiguration.Colors.DarkErrorColor);
            themeDictionary.Add("OnErrorColor", _layoutConfiguration.Colors.DarkOnErrorColor);
            themeDictionary.Add("VerslPurple", _layoutConfiguration.Colors.VerslPurple);
            themeDictionary.Add("VerslTeal", _layoutConfiguration.Colors.VerslTeal);

            return themeDictionary;
        }

        private ResourceDictionary GetLightTheme()
        {
            var themeDictionary = new ResourceDictionary();

            themeDictionary.Add("PrimaryColor", _layoutConfiguration.Colors.LightPrimaryColor);
            themeDictionary.Add("OnPrimaryColor", _layoutConfiguration.Colors.LightOnPrimaryColor);
            themeDictionary.Add("PrimaryVariantColor", _layoutConfiguration.Colors.LightPrimaryVariantColor);
            themeDictionary.Add("OnPrimaryVariantColor", _layoutConfiguration.Colors.LightOnPrimaryVariantColor);
            themeDictionary.Add("SecondaryColor", _layoutConfiguration.Colors.LightSecondaryColor);
            themeDictionary.Add("OnSecondaryColor", _layoutConfiguration.Colors.LightOnSecondaryColor);
            themeDictionary.Add("SecondaryVariantColor", _layoutConfiguration.Colors.LightSecondaryVariantColor);
            themeDictionary.Add("OnSecondaryVariantColor", _layoutConfiguration.Colors.LightOnSecondaryVariantColor);
            themeDictionary.Add("BackgroundColor", _layoutConfiguration.Colors.LightBackgroundColor);
            themeDictionary.Add("OnBackgroundColor", _layoutConfiguration.Colors.LightOnBackgroundColor);
            themeDictionary.Add("SurfaceColor", _layoutConfiguration.Colors.LightSurfaceColor);
            themeDictionary.Add("OnSurfaceColor", _layoutConfiguration.Colors.LightOnSurfaceColor);
            themeDictionary.Add("ErrorColor", _layoutConfiguration.Colors.LightErrorColor);
            themeDictionary.Add("OnErrorColor", _layoutConfiguration.Colors.LightOnErrorColor);
            themeDictionary.Add("VerslPurple", _layoutConfiguration.Colors.VerslPurple);
            themeDictionary.Add("VerslTeal", _layoutConfiguration.Colors.VerslTeal);

            return themeDictionary;
        }
    }
}
