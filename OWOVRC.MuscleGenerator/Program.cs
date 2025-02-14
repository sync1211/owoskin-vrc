using OWOVRC.MuscleGenerator.Classes;

namespace OWOVRC.MuscleGenerator
{
    public static class Program
    {
        private static readonly string currentPath = Environment.ProcessPath ?? ".";
        private static readonly string codeFilePath = $"{currentPath}\\..\\..\\..\\..\\..\\OWOVRC\\Classes\\OWOSuit\\OWOMuscles.cs";

        public static void Main()
        {
            Dictionary<string, string> musclesDict = MuscleData.GetMusclesFromProperties();
            Console.WriteLine($"Writing dictionary to file: {codeFilePath}");
            FileWriter.WriteDictionaryToFile(musclesDict, codeFilePath);
        }
    }
}
