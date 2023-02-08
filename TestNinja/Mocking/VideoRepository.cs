using TestNinja.Mocking;


namespace NUNITTEST.Mocking;

public interface IVideoRepository
{
    IEnumerable<VideoService.Video> GetUnprocessedVideos();
}

public class VideoRepository : IVideoRepository
{
    public IEnumerable<VideoService.Video> GetUnprocessedVideos()
    {
        using (var context = new VideoService.VideoContext())
        {
            var videos = (from video in context.Videos
                where !video.IsProcessed
                select video).ToList();
            
            return videos;
        }
    }
}