using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Markup.Xaml;
using LivingBeingsApp.ViewModels;
using LivingBeingsApp.Views;

namespace LivingBeingsApp;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this); // ��������� XAML
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // ��������� ������������ ��������� ��� Avalonia � CommunityToolkit
            DisableAvaloniaDataAnnotationValidation();

            // ������������� ������� ���� ����������
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(), // ����������� DataContext ��� MainWindow
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        // �������� ������ �������� ��� ���������
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // ������� ������ ������
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }
}
