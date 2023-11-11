using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BlndrerGUI.ViewModels;

public partial class ViewModelBase : ObservableObject
{
    [ObservableProperty] 
    private ObservableCollection<string>? _errorMessages;
    
    protected ViewModelBase()
    {
        ErrorMessages = new ObservableCollection<string>();
    }
}