using System.Collections.ObjectModel;
using blndrer;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BlndrerGUI.ViewModels;

public partial class BlndControlViewModel: ViewModelBase
{
    [ObservableProperty] private BlendFile? _blndFile;

    public ObservableCollection<BlendData> BlendDataAry { get; set; } = new();
    public ObservableCollection<TrackResource> BlendTracks => new(BlndFile.Pool.mBlendTrackAry);
    public ObservableCollection<PathRecord> AnimNames => new(BlndFile.Pool.mAnimNames);
    
    public BlndControlViewModel(BlendFile? file = null)
    {
        if (file is null)
        {
            BlndFile = new BlendFile();
        }
        else
        {
            BlndFile = file;
            BlendDataAry = new ObservableCollection<BlendData>(BlndFile.Pool.mBlendDataAry);
        }
    }
}