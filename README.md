# NUnit and Moq

A _unit_ is the smallest testable chunk of code.


## Section 1 Introduction

TDD: 
1.  Write a test
2.  Run the test to see it fail
3.  Write the code
4.  Run tests again -> Pass
5.  Refactor 

--> Red-Green-Refactor

### NUnit Test Runner

[NUnit Website](http://nunit.org/download/)  
[NUnit Github](https://github.com/nunit)  
[NUnit GUI](https://github.com/NUnitSoftware/nunit-gui/releases)  
[NUnit Console](https://github.com/nunit/nunit-console/releases/tag/3.8)

#### Exercise

Use NUnit and the TDD cycle to write a test for RockPaperScissors.

Write three classes which are **Rock**, **Paper**, and **Scissors**. Each of them has a method called **Beats**. It returns:

* 1  If it wins
* -1 If it loses
* 0  If it's a tie


```csharp
[Test]
public void TestPaperBeatsRock()
{
    Assert.IsTrue(paper.Beats(rock) > 0);
    Assert.IsFalse(rock.Beats(paper) > 0);
}
```

## Section 2 Assertions

1. Basic Assertions
```csharp
Assert.That(2+2, Is.EqualTo(5));
Assert.Fail("Fail");
Assert.Inconclusive("Inconclusive");
```
2. Warnings
```csharp
Assert.Warn("This is not good");
Warn.If(2 + 2 != 5);
Warn.If(2 + 2, Is.Not.EqualTo(5));
Warn.If(() => 2 + 2, Is.Not.EqualTo(5).After(2000));
Warn.Unless(2 + 2 != 5);
Warn.Unless(2 + 2, Is.EqualTo(5));
Warn.Unless(() => 2 + 2, Is.EqualTo(5).After(2000));
```
3. Arrange Act Assert
* AAA
```csharp
 [TestFixture]
public class BankAccountTests
{
    private BankAccount ba;
    [SetUp]
    public void SetUp()
    {
        ba = new BankAccount(100);
    }

    [Test]
    public void BankAccountShouldIncreaseOnPositiveDeposit()
    {
        ba.Deposit(100);
        Assert.That(ba.Balance, Is.EqualTo(200));
    }
}
```
4. Multiple Assertions
```csharp
[Test]
public void MultipleAsserts()
{
    ba.Withdraw(100);

    Assert.Multiple(() =>
    {
        Assert.That(ba.Balance, Is.EqualTo(0));
        Assert.That(ba.Balance, Is.LessThan(1));
    });
}
```
5. Exceptions
```csharp
[Test]
public void BankAccountShouldThrowOnNonPositiveAmount()
{
    var ex = Assert.Throws<ArgumentException>(() =>
    {
        ba.Deposit(-1);
    });

    StringAssert.StartsWith("Deposit amount must be positive", ex.Message);
}
```