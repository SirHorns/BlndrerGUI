using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using blndrer;
using BlndrerGUI.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace BlndrerGUI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] string _windowTitle = "BlndrerGUI";
    [ObservableProperty] BlndControlViewModel _blndControl;
    [ObservableProperty] private string _fileName = "BADWOLF";
    
    [RelayCommand]
    private async Task OpenBlndFile(CancellationToken token)
    {
        WindowTitle = "BlndrerGUI";
        ErrorMessages?.Clear();
        try
        {
            var filesService = App.Current?.Services?.GetService<FilesService>();
            if (filesService is null)
            {
                throw new NullReferenceException("Missing File Service instance.");
            }

            var file = await filesService.OpenFileAsync();
            if (file is null)
            {
                return;
            }

            WindowTitle = $"BlndrerGUI: {file.Path.AbsolutePath}";
            FileName = file.Name.Split(".")[0];
            var blnd = BlndTools.ReadBLND(file.Path.AbsolutePath);
            BlndControl = new BlndControlViewModel(blnd);
        }
        catch (Exception e)
        {
            ErrorMessages?.Add(e.Message);
        }
    }

    [RelayCommand]
    private async Task SaveBlndFile()
    {
        ErrorMessages?.Clear();
        try
        {
            var filesService = App.Current?.Services?.GetService<FilesService>();
            if (filesService is null)
            {
                throw new NullReferenceException("Missing File Service instance.");
            }

            var folder = await filesService.SelectFolderAsync();
            if (folder is null || BlndControl.BlndFile is null)
            {
                return;
            }
            
            var blndPath = Path.Combine(folder.Path.AbsolutePath, "NewBlnd.blnd");
            var jsonPath = Path.Combine(folder.Path.AbsolutePath, "NewBlnd.blnd.json");
            BlndTools.WriteBLND(BlndControl.BlndFile, blndPath);
            BlndTools.WriteJSON(BlndControl.BlndFile, jsonPath);
        }
        catch (Exception e)
        {
            ErrorMessages?.Add(e.Message);
        }
    }
}