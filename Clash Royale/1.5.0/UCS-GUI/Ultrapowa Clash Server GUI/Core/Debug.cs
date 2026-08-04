namespace CRS.Core
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;

    public static class Debug
    {
		[Conditional("DEBUG")]
		public static void Write(object message, [CallerLineNumber] int sourceLineNumber = 0)
        {
            var path = @"Logs\Write.txt";
            var stackTrace = new StackTrace(true);
            var stackFrame = stackTrace.GetFrame(1);
            var methodBase = stackFrame.GetMethod();
            var Line = sourceLineNumber;
            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            var FullMessage = $"{Line} {methodBase.DeclaringType?.Name} {methodBase.Name} {message}";
            Console.WriteLine(FullMessage);
            Console.ForegroundColor = oldColor;
            if (!File.Exists(path))
            {
                var createText = FullMessage + Environment.NewLine;
                File.WriteAllText(path, createText, Encoding.UTF8);
            }
            else
            {
                var appendText = FullMessage + Environment.NewLine;
                File.AppendAllText(path, appendText, Encoding.UTF8);
            }
        }
		public static string FlattenException(Exception exception)
		{
			var stringBuilder = new StringBuilder();

			while (exception != null)
			{
				stringBuilder.AppendLine(exception.Message);
				stringBuilder.AppendLine(exception.StackTrace);

				exception = exception.InnerException;
			}

			return stringBuilder.ToString();
		}
	}
}
