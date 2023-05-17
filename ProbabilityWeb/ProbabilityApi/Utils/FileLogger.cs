namespace ProbabilityApi.Utils;

public class FileLogger
{
	// Settings like that would usually come from appsettings in IOptions
	// That's a bit of an overkill for the current task
	private const string logFileName = "log.txt";

	// This functionality could be a service or a file repository depending on the needs of the project.
	// For the current task this is sufficient
	public static void Log(string logMsg)
	{
		using StreamWriter w = File.AppendText(logFileName);
		w.Write("\r\nCalculation Entry - ");
		w.WriteLine($"Timestamp (UTC): {DateTime.UtcNow}");
		w.WriteLine(logMsg);
		w.WriteLine("----------------------------");
	}
}
