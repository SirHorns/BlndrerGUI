using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;

namespace BlndrerGUI.Services;

public class FilesService
{
    private readonly Window _target;

    public FilesService(Window target)
    {
        _target = target;
    }

    public async Task<IStorageFile?> OpenFileAsync()
    {
        var filePickerOptions = new FilePickerOpenOptions()
        {
            Title = "Select Blnd File",
            AllowMultiple = false,
            FileTypeFilter = new FilePickerFileType[]
            {
                new("blnd")
                {
                    Patterns = new []{"*.blnd"}
                }
            }
        };
        
        var files = await _target.StorageProvider.OpenFilePickerAsync(filePickerOptions);

        return files.Count >= 1 ? files[0] : null;
    }

    public async Task<IStorageFile?> SaveFileAsync()
    {
        return await _target.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions()
        {
            Title = "Save Blnd File"
        });
    }

    public async Task<IStorageFolder?> SelectFolderAsync()
    {
        var folderPickerOptions = new FolderPickerOpenOptions()
        {
            Title = "Select Folder",
            AllowMultiple = false
        };
        
        var folders =  await _target.StorageProvider.OpenFolderPickerAsync(folderPickerOptions);

        return folders.Count >= 1 ? folders[0] : null;
    }
}