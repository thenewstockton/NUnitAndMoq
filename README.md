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

### Exercise

Use NUnit and the TDD cycle to write a test for RockPaperScissors.

Write three classes which are **Rock**, **Paper**, and **Scissors**. Each of them has a method called **Beats**. It returns:

* 1  If it wins
* -1 If it loses
* 0  If it's a tie
* -2 If it does not exist.


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
Assert.Warn("This is a warn");
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

### Exercises

1. In your previous exercise, you made the **Beats** method return -2 if it does not exist. Add warnings to each test case. Write a test case which deliberately invokes the warning. <br>
After adding warnings, add multiple assertions for each assertion, deliberately make a test fail to see if there are two error messages.


2. Create a **Player** class and **PlayerTest**
The player class has the method **ThrowsGesture(string gestureName)**
```csharp
public Gesture ThrowGesture(string gestureName)
```

This method returns the corresponding object. The mapping is:
```
"Rock" -> new Rock()
"Paper" -> new Paper()
"Scissors" -> new Scissors()
```
3. It throws an exception if the input is not valid. Make it throw an exception with a message and do an Assertion.


6. Data-Driven Testing
```csharp
private BankAccount ba;

[SetUp]
public void SetUp()
{
    ba = new BankAccount(100);
}

[Test]
[TestCase(50, true, 50)]
[TestCase(100, true, 0)]
[TestCase(1000, false, 100)]
public void TestMultipleWithdrawalScenarios(int amountToWithdraw, bool shouldSuccedd, int expectedBalance)
{
    var result = ba.Withdraw(amountToWithdraw);
    //Warn.If(!result, "Failed for some reason");
    Assert.Multiple(() =>
    {
        Assert.That(result, Is.EqualTo(shouldSuccedd));
        Assert.That(expectedBalance, Is.EqualTo(ba.Balance));
    });
}
```

However, if you pass a non-primitive types, you most likely will get the error message:  ``an attribute type must be a constant type``

To see more: [MSDN](https://docs.microsoft.com/en-us/dotnet/visual-basic/misc/bc30045)

For example, **decimal** is a type which does not work. If we change our _TestMultipleWithdrawalScenarios_ **int32** parameter types into **decimal** types, then we will encounter the same error.

To overcome this issue:
```csharp
private static object[] MyCaseSource =
{
    new object[] { 50.0m,true,50m },
    new object[] { 100m, true, 0m},
    new object[] { 1000m, false, 100m }
};

[Test]
[TestCaseSource("MyCaseSource")]
public void TestMultipleWithdrawalScenariosCaseSource(decimal amountToWithdraw, bool shouldSuccedd, decimal expectedBalance)
{
    var result = ba.Withdraw((int)amountToWithdraw);
    //Warn.If(!result, "Failed for some reason");
    Assert.Multiple(() =>
    {
        Assert.That(result, Is.EqualTo(shouldSuccedd));
        Assert.That((int)expectedBalance, Is.EqualTo(ba.Balance));
    });
}
```

### Exercise
1. In your **Player** class, give it an _int32_ property **Cash** and a method **Withdraw** like so:

```csharp
public class Player
{
    public int Cash { get; set; }
    public void Withdraw(int amount);
}
```

Use the _Data-Driven Testing_ mechanism to test the **Withdraw** method

2. In your first exercise, you wrote many test cases for **Gesture** which indicates who beats who. Use _Data-Driven Testing_ to re-implement those test cases.

## Section 3 Test Doubles
Most likely you have heard the terms **Fake**, **Stubs**, and **Mock** more or less.

Two classes interact which makes it hard to test. We need a _test double_ for each object. 
* Fake: Null Object Pattern
* Stub: Like a fake, but returns the answer you want
* Mock: A fake object where you can set expectations

Reference Reading: [Mocks are not stubs](https://martinfowler.com/articles/mocksArentStubs.html)