using blndrer;
using BlndrerGUI.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BlndrerGUI.ViewModels;

public partial class BlndControlViewModel: ViewModelBase
{
    [ObservableProperty] private BlendFile? _activeBlndFile;
    [ObservableProperty] private BlndFile _blndFile;

    public BlndControlViewModel(BlendFile? file = null)
    {
        if (file is null)
        {
            BlndFile = new BlndFile();
        }
        else
        {
            ActiveBlndFile = file;
            
            var bTracks = new BlendTrack[file.Pool.mBlendTrackAry.Length];
            for (int i = 0; i < file.Pool.mBlendTrackAry.Length; i++)
            {
                var track = file.Pool.mBlendTrackAry[i];
                bTracks[i] = new BlendTrack()
                {
                    BlendWeight = track.mBlendWeight,
                    BlendMode = track.mBlendMode,
                    Index = track.mIndex,
                    Name = track.mName,
                    ResourceSize = track.mResourceSize
                };
            }
            
            BlndFile = new BlndFile
            {
                Header = new Header
                {
                    EngineType = file.Header.mEngineType,
                    BinaryBlockType = file.Header.mBinaryBlockType,
                    BinaryBlockVersion = file.Header.mBinaryBlockVersion
                },
                Pool = new Pool()
                {
                    FormatToken = file.Pool.mFormatToken,
                    Version = file.Pool.mVersion,
                    UseCascadeBlend = file.Pool.mUseCascadeBlend,
                    CascadeBlendValue = file.Pool.mCascadeBlendValue,
                    BlendTrackAry = bTracks,
                    Skeleton = file.Pool.mSkeleton.path,
                    ResourceSize = file.Pool.mResourceSize
                }
            };
        }
        
        
    }
}