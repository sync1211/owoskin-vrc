namespace OWOVRC.Classes.Commandline
{
    public class Spinner
    {
        private readonly char[] sequence = [ '\\', '|', '/', '-' ];
        private int counter = 0;
        public string Text;

        public Spinner(string text)
        {
            Text = text;
        }

        public void WriteToConsole()
        {
            Console.Write("\r");

            Console.Write($"{sequence[counter]} {Text}");

            counter = (counter + 1) % sequence.Length;
        }
    }
}