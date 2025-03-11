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
        AvaloniaXamlLoader.Load(this); // Загружаем XAML
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Отключаем дублирование валидаций для Avalonia и CommunityToolkit
            DisableAvaloniaDataAnnotationValidation();

            // Устанавливаем главное окно приложения
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(), // Привязываем DataContext для MainWindow
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        // Получаем список плагинов для валидации
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // Удаляем каждый плагин
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }
}
