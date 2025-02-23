using OWOVRC.MuscleGenerator.Classes;

namespace OWOVRC.MuscleGenerator
{
    public static class Program
    {
        private static readonly string currentPath = Environment.ProcessPath ?? ".";
        private static readonly string codeFilePath = $"{currentPath}\\..\\..\\..\\..\\..\\OWOVRC\\Classes\\OWOSuit\\OWOMuscles.cs";
        private static readonly string functionFilePath = $"{currentPath}\\..\\..\\..\\..\\..\\OWOVRC\\Classes\\OWOSuit\\OWOMuscles.Autogen.cs";

        public static void Main()
        {
            Dictionary<string, string> musclesDict = MuscleData.GetMusclesFromProperties();

            string statementBody = FunctionGenerator.CreateSwitchStatement(musclesDict);
            Console.WriteLine($"Writing function to file: {codeFilePath}");
            Console.WriteLine("Writing data to file...");
            File.WriteAllText(functionFilePath, statementBody);
        }
    }
}
