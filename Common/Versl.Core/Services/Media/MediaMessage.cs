using MediaManager.Player;
using MvvmCross.Plugin.Messenger;

namespace Versl.Services.Media
{
    public class MediaMessage : MvxMessage
    {
        public MediaMessage(object sender, MediaAsset  item, MediaPlayerState state) : base(sender)
        {
            Media = item;
            State = state;
        }

        public MediaAsset  Media {get; protected set;}
        public MediaPlayerState State { get; protected set; }
    }
}
