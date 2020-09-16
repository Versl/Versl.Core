using Versl.Enums;

namespace Versl.Services.Media
{
    public class MediaAsset
    {
        public MediaAsset(string uri, string title, string imageUri, MediaPlaybackType playback)
        {
            Uri = uri;
            Title = title;
            ImageUri = imageUri;
            Playback = playback;
        }

        public string Uri { get; protected set; }
        public string Title { get; protected set; }
        public string ImageUri { get; protected set; }
        public MediaPlaybackType Playback { get; protected set; }
    }
}
