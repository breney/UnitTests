using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NUNITTEST.Mocking;

namespace TestNinja.Mocking;

public class VideoService
{
    private IFileReader _fileReader;

    private IVideoRepository _videoRepository;
    public VideoService(IFileReader fileReader = null, IVideoRepository videoRepository = null)
    {
        _fileReader = fileReader ?? new FileReader();
        _videoRepository = videoRepository ?? new VideoRepository();
    }
    
    public string ReadVideoTitle()
    {
        var str = _fileReader.Read("video.txt"); 
        var video = JsonConvert.DeserializeObject<Video>(str);
        
        return video == null ? "Error parsing the video." : video.Title;
    }

    public string GetUnprocessedVideosAsCsv()
    {
        var videosIds = _videoRepository.GetUnprocessedVideos().Select(v => v.Id).ToList();

        return string.Join(",", videosIds); 
    }

    public class Video
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsProcessed { get; set; }
    }

    public class VideoContext : DbContext
    {
        public DbSet<Video> Videos { get; set; }
    }
}