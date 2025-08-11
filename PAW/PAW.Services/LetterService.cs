using PAW.FluentAPI;

namespace PAW.Services
{
    public class LetterService
    {
        public void GenerateLetter(string title, string subject, string footer, string corpus)
        {
            System.Diagnostics.Debug.WriteLine(
                new FluentLetter(corpus ?? "Lorem ipsum lalala")
                .AddTitle(title)
                .AddSubject(subject)
                .AddFooter(footer)
                .ToString());
        }
    }
}
