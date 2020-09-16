using System.Threading.Tasks;
using MediaManager.Player;

namespace Versl.Services.Media
{
    public interface IMediaService
    {
        MediaAsset CurrentItem { get; }
        MediaPlayerState State { get; }
        Task Play(MediaAsset item);
        Task PlayPause();
    }
}
