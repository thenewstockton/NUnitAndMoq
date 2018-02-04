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


```CS
[Test]
public void TestPaperBeatsRock()
{
    Assert.IsTrue(paper.Beats(rock) > 0);
    Assert.IsFalse(rock.Beats(paper) > 0);
}
```