using System;

namespace Windows.Devices.Gpio.Led
{
	/// <summary>
	/// Predefined intervals used for the Delay property
	/// for an instance of Windows.Devices.Led.LedSequenceState.
	/// </summary>
	public static class Interval
	{
		/// <summary>
		/// Defines a 100 ms interval.
		/// </summary>
		public static TimeSpan Shortest { get; } = TimeSpan.FromMilliseconds(100);

		/// <summary>
		/// Defines a 250 ms interval.
		/// </summary>
		public static TimeSpan Shorter { get; } = TimeSpan.FromMilliseconds(250);

		/// <summary>
		/// Defines a 500 ms interval.
		/// </summary>
		public static TimeSpan Short { get; } = TimeSpan.FromMilliseconds(500);

		/// <summary>
		/// Defines a 750 ms interval.
		/// </summary>
		public static TimeSpan Medium { get; } = TimeSpan.FromMilliseconds(750);

		/// <summary>
		/// Defines a 1000 ms interval.
		/// </summary>
		public static TimeSpan Long { get; } = TimeSpan.FromMilliseconds(1000);

		/// <summary>
		/// Defines a 1250 ms interval.
		/// </summary>
		public static TimeSpan Longer { get; } = TimeSpan.FromMilliseconds(1250);

		/// <summary>
		/// Defines a 1500 ms interval.
		/// </summary>
		public static TimeSpan Longest { get; } = TimeSpan.FromMilliseconds(1500);
	}
}
