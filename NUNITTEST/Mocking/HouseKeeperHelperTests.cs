using Moq;
using TestNinja.Mocking;

namespace NUNITTEST.Mocking;

[TestFixture]
public class HouseKeeperHelperTests
{
    private Mock<BookingHelper.IUnitOfWork> _unitOfWork;
    private Mock<IStatementGenerator> _statementGenerator;
    private Mock<IEmailSender> _emailSender;
    private Mock<HousekeeperHelper.IXtraMessageBox> _xtraMessageBox;
    private HousekeeperHelper.Housekeeper _housekeeper;
    private HousekeeperHelper _service;
    private readonly string _statementFilename = "filename";


    [SetUp]
    public void SetUp()
    {
        _unitOfWork = new Mock<BookingHelper.IUnitOfWork>();
        _statementGenerator = new Mock<IStatementGenerator>();
        _emailSender = new Mock<IEmailSender>();
        _xtraMessageBox = new Mock<HousekeeperHelper.IXtraMessageBox>();
        _housekeeper = new HousekeeperHelper.Housekeeper
        {
            Email = "a", FullName = "b", Oid = 1, StatementEmailBody = "C"
        };
        
        _unitOfWork.Setup(uW => uW.Query<HousekeeperHelper.Housekeeper>()).Returns(new List<HousekeeperHelper.Housekeeper> {_housekeeper}.AsQueryable);

        _service = new HousekeeperHelper(_unitOfWork.Object, _statementGenerator.Object, _emailSender.Object,_xtraMessageBox.Object);
    }
    
    [Test]
    public void SendStatementEmails_WhenCalled_GenerateStatements()
    {
        _service.SendStatementEmails(new DateTime(2017,1,1));
        
        _statementGenerator.Verify(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, new DateTime(2017,1,1)));
    }
    
    [Test]
    public void SendStatementEmails_HouseKeepersEmailIsNull_ShouldNotGenerateStatement()
    {
        _housekeeper.Email = null;
        
        _service.SendStatementEmails(new DateTime(2017,1,1));
        
        _statementGenerator.Verify(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, new DateTime(2017,1,1)), Times.Never);
    }
    
    [Test]
    public void SendStatementEmails_HouseKeepersEmailIsWhiteSpace_ShouldNotGenerateStatement()
    {
        _housekeeper.Email = "";
        
        _service.SendStatementEmails(new DateTime(2017,1,1));
        
        _statementGenerator.Verify(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, new DateTime(2017,1,1)), Times.Never);
    }
    
    [Test]
    public void SendStatementEmails_WhenCalled_EmailTheStatement()
    {
        _statementGenerator.Setup(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, new DateTime(2017,1,1))).Returns(_statementFilename);

        _service.SendStatementEmails(new DateTime(2017,1,1));
        
        _emailSender.Verify(es => es.EmailFile(_housekeeper.Email, _housekeeper.StatementEmailBody, _statementFilename, It.IsAny<string>()));
    }
    
    [Test]
    public void SendStatementEmails_EmailSendingFails_DisplayMessageBox()
    {
        _statementGenerator.Setup(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, new DateTime(2017,1,1))).Returns(_statementFilename);

        _emailSender.Setup(es => es.EmailFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Throws<Exception>();
        
        _service.SendStatementEmails(new DateTime(2017,1,1));
        
        _xtraMessageBox.Verify( xMB => xMB.Show(It.IsAny<string>(),It.IsAny<string>(),HousekeeperHelper.MessageBoxButtons.OK));
    }
}