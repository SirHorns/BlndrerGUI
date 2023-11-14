using System.Collections.ObjectModel;
using System.Linq;
using blndrer;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BlndrerGUI.ViewModels;

public partial class BlndControlViewModel: ViewModelBase
{
    [ObservableProperty] private BlendFile? _blndFile;
    [ObservableProperty] private ObservableCollection<PathRecord> _animNames = new ObservableCollection<PathRecord>();
    [ObservableProperty] private ObservableCollection<BlendData> _blendDataAry = new ObservableCollection<BlendData>();
    [ObservableProperty] private ObservableCollection<TrackResource> _blendTrackAry =new ObservableCollection<TrackResource>();
    
    public BlndControlViewModel(BlendFile? file = null)
    {
        if (file is null)
        {
            BlndFile = new BlendFile();
        }
        else
        {
            BlndFile = file;
            BlendDataAry = new ObservableCollection<BlendData>(file.Pool.mBlendDataAry);
            BlendTrackAry = new ObservableCollection<TrackResource>(file.Pool.mBlendTrackAry);
            AnimNames = new ObservableCollection<PathRecord>(file.Pool.mAnimNames);
            
        }
    }

    [ObservableProperty] private int _animationIndex;

    [RelayCommand]
    private void AddAnimationPath()
    {
        AnimationIndex--;
        if (AnimationIndex < 0)
        {
            AnimationIndex = 0;
        }
        AnimNames.Insert(AnimationIndex, new PathRecord(""));
    }

    [RelayCommand]
    private void RemoveAnimationPath()
    {
        if (AnimationIndex < 0 || AnimNames.Count<=0)
        {
         return;   
        }
        AnimNames.RemoveAt(AnimationIndex);
    }

    public BlendFile CreateBlnd()
    {
        BlndFile.Pool.mBlendDataAry = BlendDataAry.ToArray();
        BlndFile.Pool.mBlendTrackAry = BlendTrackAry.ToArray();
        BlndFile.Pool.mAnimNames = AnimNames.ToArray();
        return BlndFile;
    }
}