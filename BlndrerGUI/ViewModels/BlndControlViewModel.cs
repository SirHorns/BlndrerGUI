using System.Collections.ObjectModel;
using blndrer;
using BlndrerGUI.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using PathRecord = BlndrerGUI.Models.PathRecord;

namespace BlndrerGUI.ViewModels;

public partial class BlndControlViewModel: ViewModelBase
{
    [ObservableProperty] private BlendFile? _activeBlndFile;
    [ObservableProperty] private BlndFile _blndFile;

    public ObservableCollection<BlendTrack> BlendTracks => new(BlndFile.Pool.BlendTrackAry);
    public ObservableCollection<PathRecord> AnimNames => new(BlndFile.Pool.AnimNames);

    public BlndControlViewModel(BlendFile? file = null)
    {
        if (file is null)
        {
            BlndFile = new BlndFile();
        }
        else
        {
            ActiveBlndFile = file;
            BlndFile = new BlndFile(file);
        }
    }
}