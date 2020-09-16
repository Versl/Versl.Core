using System;
using System.Threading.Tasks;
using MediaManager;
using MediaManager.Player;
using MvvmCross.Plugin.Messenger;

namespace Versl.Services.Media
{
    public class MediaService : IMediaService
    {
        IMvxMessenger _messenger;

        public MediaService(IMvxMessenger messenger)
        {
            _messenger = messenger;

            CrossMediaManager.Current.StateChanged += Current_StateChanged;
        }

        private void Current_StateChanged(object sender, MediaManager.Playback.StateChangedEventArgs e)
        {
            var msg = new MediaMessage(this, CurrentItem, CrossMediaManager.Current.State);
            _messenger.Publish(msg);
        }

        public MediaAsset CurrentItem { get; private set; }

        public MediaPlayerState State
        {
            get
            {
                return CrossMediaManager.Current.State;
            }
        }

        public Task Play(MediaAsset item)
        {
            CurrentItem = item;

            var msg = new MediaMessage(this, item, CrossMediaManager.Current.State);
            _messenger.Publish(msg);
         
            return CrossMediaManager.Current.Play(item.Uri);            
        }

        public Task PlayPause()
        {
            var msg = new MediaMessage(this, CurrentItem, CrossMediaManager.Current.State);
            _messenger.Publish(msg);

            return CrossMediaManager.Current.PlayPause();
        }
    }
}
