using System;

namespace Windows.Devices.Gpio.Led
{
	/// <summary>
	/// Defines a single state fort an LED Sequence.
	/// </summary>
	public class LedSequenceState
	{
		/// <summary>
		/// Gets/sets the On or Off state of the LED.
		/// </summary>
		public LedState State { get; set; }

		/// <summary>
		/// Gets/sets the amount of time the LED is in
		/// the On or Off state.
		/// </summary>
		public TimeSpan Delay { get; set; }
	}	
}
