using blndrer;
using BlndrerGUI.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BlndrerGUI.ViewModels;

public partial class BlndControlViewModel: ViewModelBase
{
    [ObservableProperty] private BlendFile? _activeBlndFile;
    [ObservableProperty] private BlndFile _blndFile;

    public BlndControlViewModel(BlendFile file)
    {
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
                Skeleton = file.Pool.mSkeleton.path,
                ResourceSize = file.Pool.mResourceSize
            }
        };
    }
}