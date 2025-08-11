using System.Text;

namespace PAW.FluentAPI
{
    public class FluentLetter
    {
        private readonly StringBuilder _letter = new();

        public FluentLetter(string letter)
        {
            _letter.AppendLine($"{letter}" ?? "Corpus is Empty");
        }

        public FluentLetter AddTitle(string title)
        {
            _letter.Insert(0, $"{title}\n" ?? "Title: Fluent API Example\n");
            return this;
        }

        public FluentLetter AddSubject(string subject)
        {
            _letter.AppendLine($"{subject}" ?? "Subject: Demonstrating Fluent API Pattern");
            return this;
        }

        public FluentLetter AddFooter(string footer)
        {
            _letter.AppendLine($"{footer}" ?? "Subject: Demonstrating Fluent API Pattern");
            return this;
        }

        public override string ToString()
        {
            return _letter.ToString();
        }
    }
}
