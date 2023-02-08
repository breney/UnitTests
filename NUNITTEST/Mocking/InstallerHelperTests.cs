using System.Net;
using Moq;
using TestNinja.Mocking;

namespace NUNITTEST.Mocking;

[TestFixture]
public class InstallerHelperTests
{
    private Mock<IFileDownloader> _fileDwonloader;
    private InstallerHelper _installerHelper;
    
    [SetUp]
    public void SetUp()
    {
        _fileDwonloader = new Mock<IFileDownloader>();
        _installerHelper = new InstallerHelper(_fileDwonloader.Object);
    }

    [Test]
    public void DwonloadInstaller_DwonloadFails_ReturnFalse()
    {
        _fileDwonloader.Setup(fd => fd.DwonloadFile(It.IsAny<string>(),It.IsAny<string>())).Throws<WebException>();
        Assert.That(() => _installerHelper.DwonloadInstaller("customer", "installer"), Is.False);
    }
    
    [Test]
    public void DwonloadInstaller_DwonloadCompletes_ReturnTrue()
    {
        Assert.That(() => _installerHelper.DwonloadInstaller("customer", "installer"), Is.True);
    }
}