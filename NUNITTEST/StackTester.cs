namespace NUNITTEST;
using TestNinja.Fundamentals;

[TestFixture]
public class StackTester
{
    private Stack<string> _stack;
    
    [SetUp]
    public void SetUp()
    {
        _stack = new Stack<string>();
    }

    [Test]
    public void Push_ArgumentIsNull_ReturnNullException()
    {
        Assert.That(() => _stack.Push(null), Throws.ArgumentNullException);
    }
    
    [Test]
    public void Push_ArgumentIsAnObject_ReturnAddictionOfObjectToStack()
    {
        _stack.Push("a");
        
        Assert.That(_stack.Count, Is.EqualTo(1));
    }

    [Test]
    public void Pop_StackIsEmpty_ReturnInvalidOperationException()
    {
        Assert.That(() => _stack.Pop() , Throws.InvalidOperationException);
    }
    
    [Test]
    public void Pop_RemoveObjFromStack_ReturnStackWithOneLessObj()
    {
        _stack.Push("a");
        _stack.Push("b");
        
        _stack.Pop(); 
        
        Assert.That(_stack.Count, Is.EqualTo(1));
    }
    
    [Test]
    public void Peek_StackIsEmpty_ReturnInvalidOperationException()
    {
        Assert.That(() => _stack.Peek() , Throws.InvalidOperationException);
    }
    
    [Test]
    public void Peek_WhenUsed_ReturnFirstObjOfStack()
    {
        _stack.Push("a");
        _stack.Push("b");
        
        Assert.That(() => _stack.Peek() , Is.EqualTo("b").IgnoreCase);
    }
}