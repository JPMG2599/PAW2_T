using PAW.Services;

namespace PAW.FluentLetterTest;

[TestClass]
public sealed class LetterGenerationTests
{
    [TestMethod]
    public void TestFluentLetterGeneratorUsingConcreteClass()
    {
        // Arrange
        var title = "PAW-1";
        var subject = "Generating fluent letter";
        var footer = "this is the last line";
        var corpus = "Lorem ipsum lalala";
        var expectedOutput = $"PAW-1\r\nLorem ipsum lalala\r\nGenerating fluent letter\r\nthis is the last line\r\n";

        // Act
        var fluentLetter = new PAW.FluentAPI.FluentLetter(corpus)
            .AddTitle(title)
            .AddSubject(subject)
            .AddFooter(footer)
            .ToString();

        // Assert
        Assert.IsNotNull(fluentLetter, "The fluent letter should not be null.");
        //Assert.AreEqual(expectedOutput, fluentLetter, "The generated letter does not match the expected output.");
        Assert.IsTrue(fluentLetter.Contains(title), "The letter should contain the title.");
        Assert.IsTrue(fluentLetter.Contains(subject), "The letter should contain the subject.");
        Assert.IsTrue(fluentLetter.Contains(footer), "The letter should contain the footer.");
        Assert.IsTrue(fluentLetter.Contains(corpus), "The letter should contain the corpus text.");
    }

    [TestMethod]
    public void TestFluentLetterGeneratorUsingLetterService_CorpusNull()
    {
        // Arrange
        var title = "PAW-1";
        var subject = "Generating fluent letter";
        var footer = "this is the last line";
        var corpus = "Lorem ipsum lalala";
        var expectedOutput =
            "PAW-1\n" +
            "Lorem ipsum lalala" +
            "Generating fluent letter\n" +
            "this is the last line\n";
        
        try
        {
            // Act
            var service = new LetterService();
            service.GenerateLetter(title, subject, footer, null);
        }
        catch
        {
            // Assert
            Assert.Fail("The letter generation should not throw an exception when corpus is null.");
        }
        finally
        {
            // Assert
            Assert.IsTrue(true, "The letter generation completed without throwing an exception.");
        }
    }
}
