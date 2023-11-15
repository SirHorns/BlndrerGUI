using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BlndrerGUI.ViewModels;

public partial class ClipResourceVM: ViewModelBase
{
    [ObservableProperty] private ClipDataVM _clipData;

    protected ClipResourceVM()
    {
        ClipData = new ClipDataVM();
    }
}