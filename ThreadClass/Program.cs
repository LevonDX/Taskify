namespace ThreadClass
{
	internal class Program
	{
		static long Sum()
		{
			long sum = 0;
			for (int i = 1; i <= 1E09; i++)
			{
				sum += i;
			}

			return sum;
		}

		static void Main(string[] args)
		{
			Taskify<long> taskify = new Taskify<long>(Sum);

			taskify.Start();

			taskify.ContinueWith(TaskFinished);

			Console.WriteLine("Summing...");

			while (true)
			{
				Console.WriteLine("Working");
				Thread.Sleep(500);
			}
		}

		static void TaskFinished(Taskify<long> taskify)
		{
			long result = taskify.Result;

			Console.WriteLine("Sum is {0}", result);
		}
	}
}