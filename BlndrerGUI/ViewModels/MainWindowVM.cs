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

public partial class MainWindowVM : ViewModelBase
{
    [ObservableProperty] string _windowTitle = "BlndrerGUI";
    [ObservableProperty] BlndControlVM _blndControl;
    [ObservableProperty] private string _fileName = "BADWOLF";
    [ObservableProperty] private bool _createBlnd = true;
    [ObservableProperty] private bool _createJson = false;
    
    public MainWindowVM()
    {
        BlndControl = new BlndControlVM();
    }


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
            BlndControl = new BlndControlVM(blnd);
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
            
            var newBlnd = BlndControl.CreateBlnd();
            
            if (CreateBlnd)
            {
             BlndTools.WriteBLND(newBlnd, Path.Combine(folder.Path.AbsolutePath, $"{FileName}.blnd"));
            }

            if (CreateJson)
            {
               BlndTools.WriteJSON(newBlnd, Path.Combine(folder.Path.AbsolutePath, $"{FileName}_blnd.json"));
            }
        }
        catch (Exception e)
        {
            ErrorMessages?.Add(e.Message);
        }
    }
}