using System.Text;

namespace OWOVRC.MuscleGenerator.Classes
{
    internal static class FunctionGenerator
    {
        public const string REPLACEMENT_STRING = "//<AUTOGEN: MuscleFunction>";
        private const string SWITCH_CASE_TEMPLATE = @"                ""{0}"" => {1},";

        public static string CreateSwitchStatement(Dictionary<string, string> muscles)
        {
            StringBuilder switchStatement = new();
            foreach (KeyValuePair<string, string> entry in muscles)
            {
                string muscleName = entry.Key;
                string muscleObj = entry.Value;
                switchStatement.AppendFormat(SWITCH_CASE_TEMPLATE, muscleName, muscleObj).AppendLine();
            }

            return ClassTemplate.ClassText.Replace(REPLACEMENT_STRING, switchStatement.ToString());
        }
    }
}
