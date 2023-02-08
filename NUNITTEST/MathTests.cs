﻿using TestNinja.Fundamentals;
using Math = TestNinja.Fundamentals.Math;

namespace NUNITTEST;

[TestFixture]
public class MathTests
{
    private Math _math;
    
    // SetUp
    // TearDown
    [SetUp]
    public void SetUp()
    {
        _math = new Math();
    }
    
    [Test]
    [Ignore("Because i wanted to!")]
    public void Add_WhenCalled_ReturnTheSumOfArguments()
    {
        var result = _math.Add(1, 3);
        
        Assert.That(result, Is.EqualTo(4));
    }
    
    [Test]
    [TestCase(2,1,2)]
    [TestCase(1,2,2)]
    [TestCase(2,2,2)]
    public void Max_WhenCalled_ReturnTheGreaterArgument(int a, int b, int expectedResult)
    {
        var result = _math.Max(a, b);
        
        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [Test]
    public void GetOddNumbers_LimitIsGreaterThanZero_ReturnOddNumbersUpToLimit()
    {
        var result = _math.GetOddNumbers(5);
        
//        Assert.That(result, Is.Not.Empty);
//        Assert.That(result.Count(), Is.EqualTo(3));
//        Assert.That(result, Does.Contain(1));
//        Assert.That(result, Does.Contain(3));
        Assert.That(result, Is.EquivalentTo(new [] {1, 3, 5})); // Equal to[]
//        Assert.That(result, Is.Ordered); // Is Ordered
//        Assert.That(result, Is.Unique); //No duplicated numbers
    }
}