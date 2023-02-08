using Moq;
using TestNinja.Mocking;

namespace NUNITTEST.Mocking;
[TestFixture]
public class VideoServiceTests
{
    private Mock<IFileReader> _fileReader;
    private VideoService _videoService;
    private Mock<IVideoRepository> _videoRepository;
    
    [SetUp]
    public void SetUp()
    {
        _fileReader = new Mock<IFileReader>();
        _videoRepository = new Mock<IVideoRepository>();
        _videoService = new VideoService(_fileReader.Object, _videoRepository.Object);
    }
    
    [Test]
    public void ReadVideoTitle_EmptyFile_ReturnError()
    {
        _fileReader.Setup(fr => fr.Read("video.txt")).Returns("");
        Assert.That(() => _videoService.ReadVideoTitle(), Does.Contain("error").IgnoreCase);
    }

    [Test]
    public void GetUnprocessedVideosAsCsv_ArgumentIsEmpty_ReturnEmptyString()
    {
        _videoRepository.Setup(vr => vr.GetUnprocessedVideos()).Returns(new List<VideoService.Video>());
        Assert.That(() => _videoService.GetUnprocessedVideosAsCsv(), Is.EqualTo(""));
    }
    
    [Test]
    public void GetUnprocessedVideosAsCsv_ArgumentContainsValues_ReturnAllIds()
    {
        var videos = new List<VideoService.Video> { new VideoService.Video() { Id = 1, Title = "Teste", IsProcessed = false}, new VideoService.Video(){Id = 2, Title = "Teste", IsProcessed = false}};
        _videoRepository.Setup(vr => vr.GetUnprocessedVideos()).Returns(videos);
        Assert.That(() => _videoService.GetUnprocessedVideosAsCsv(), Is.EqualTo("1,2"));
    }
}