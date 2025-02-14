using System.Linq;

namespace OWOVRC.MuscleGenerator.Classes
{
    internal static class FileWriter
    {
        private static int SearchLine(string[] lines, string search)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Trim().Equals(search))
                {
                    return i;
                }
            }

            return -1;
        }

        public static void WriteDictionaryToFile(Dictionary<string, string> dictionary, string path)
        {
            string content = File.ReadAllText(path);
            string[] lines = content.Split(Environment.NewLine);

            int startLine = SearchLine(lines, "//<AUTOGEN: MuscleDictionary>");
            int endLine = SearchLine(lines, "//</AUTOGEN: MuscleDictionary>");
            int sectionLength = endLine - startLine;

            if (endLine <= startLine)
            {
                Console.WriteLine("FATAL: Could not find generator section in file.");
                return;
            }

            Console.WriteLine($"Found generator section: {startLine}-{endLine}");

            string[] codeStart = lines.Take(startLine + 1).ToArray();
            string[] codeEnd = lines
                .Skip(startLine + sectionLength)
                .Take(lines.Length - sectionLength)
                .ToArray();

            int indent = lines[startLine].IndexOf("//");
            string indentString = new(' ', indent);

            List<string> dictionaryContent = new();
            foreach (KeyValuePair<string, string> entry in dictionary)
            {
                dictionaryContent.Add($"{indentString}{{ \"{entry.Key}\", {entry.Value} }},");
            }

            string dictionaryContentString = String.Join(Environment.NewLine, dictionaryContent)
                .TrimEnd(',');

            string codeStartStr = String.Join(Environment.NewLine, codeStart);
            string codeEndStr = String.Join(Environment.NewLine, codeEnd);
            string newContent = $"{codeStartStr}{Environment.NewLine}{dictionaryContentString}{Environment.NewLine}{codeEndStr}";

            Console.WriteLine("Writing data to file...");
            File.WriteAllText(path, newContent);
        }
    }
}
