using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Windows.Devices.Gpio.Led
{
	/// <summary>
	/// Defines the LED the specific LED
	/// on the Raspberry Pi board that the
	/// sequence will apply to.
	/// </summary>
	public enum LedInstance
	{
		/// <summary>
		/// Specifies the Red Power LED.
		/// </summary>
		RedPower,
		/// <summary>
		/// Specifies the Green Status LED.
		/// </summary>
		GreenStatus
	}

	/// <summary>
	/// Specifies the On or Off state of the LED.
	/// </summary>
	public enum LedState
	{
		/// <summary>
		/// The LED is On.
		/// </summary>
		On,
		/// <summary>
		/// The LED is Off.
		/// </summary>
		Off
	}

	/// <summary>
	/// Provides an intra face to manage the state of the Raspberry Pi
	/// built-in LED's allowing complex sequences to be applied to either
	/// or both LED's.
	/// </summary>
	public class LedManager : Dictionary<string, LedSequence>, IDisposable
	{
		private const int RedPowerLedPinNumber = 35;
		private const int GreenActivityLedPinNumber = 47;

		/// <summary>
		/// Initializes a default instance of Windows.Devices.Gpio.Led.LedManager.
		/// </summary>
		public LedManager()
		{
			// ***
			// *** Get the default GPIO controller on the system
			// ***
			GpioController gpio = GpioController.GetDefault();

			// ***
			// *** Check that we have a controller
			// ***
			if (gpio != null)
			{
				// ***
				// ***
				// ***
				this.RedPower = gpio.OpenPin(RedPowerLedPinNumber);
				this.RedPower.SetDriveMode(GpioPinDriveMode.Output);

				// ***
				// *** 
				// ***
				this.GreenStatus = gpio.OpenPin(GreenActivityLedPinNumber);
				this.GreenStatus.SetDriveMode(GpioPinDriveMode.Output);
			}
		}

		/// <summary>
		/// Gets the GPIO pin for the Red Power LED for direct access.
		/// </summary>
		public GpioPin RedPower { get; protected set; }

		/// <summary>
		/// Gets the GPIO pin for the Green Status LED for direct access.
		/// </summary>
		public GpioPin GreenStatus { get; protected set; }

		/// <summary>
		/// Runs named sequence on the specified LED. The sequence must be preloaded
		/// or a SequenceDoesNotExistException will be thrown.
		/// </summary>
		/// <param name="name">The name of the sequence.</param>
		/// <param name="ledInstance">The LED to run the sequence on.</param>
		/// <param name="cancellationToken">A cancellation token to be used to stop the sequence.</param>
		/// <param name="repeat">Determines if the sequence continues to run until the cancellation
		/// token is canceled (true) or run once and quit (false). The default value is true.</param>
		/// <returns>Returns an awaitable task.</returns>
		public Task RunSequence(string name, LedInstance ledInstance, CancellationToken cancellationToken, bool repeat = true)
		{
			if (cancellationToken == null)
			{
				throw new ArgumentOutOfRangeException(nameof(cancellationToken), "The cancellationToken parameter cannot be null when repeat is true.");
			}

			return Task.Factory.StartNew(async () =>
			{
				try
				{
					if (this.ContainsKey(name))
					{
						// ***
						// *** Start with the LED's off
						// ***
						await this.SetState(ledInstance, LedState.Off);

						// ***
						// *** Loop until canceled
						// ***
						while (!cancellationToken.IsCancellationRequested && repeat)
						{
							// ***
							// *** Step through each state
							// ***
							foreach (var sequenceState in this[name])
							{
								// ***
								// *** Set the light status
								// ***
								await this.SetState(ledInstance, sequenceState.State);

								// ***
								// *** Delay the specified amount of time
								// ***
								await Task.Delay(sequenceState.Delay);
							}
						}
					}
					else
					{
						// ***
						// *** The key name was invalid
						// ***
						throw new SequenceDoesNotExistException(name);
					}
				}
				finally
				{
					// ***
					// *** Finish with the LED's off
					// ***
					await this.SetState(ledInstance, LedState.Off);
				}
			});
		}

		/// <summary>
		/// Sets the On or Off state of the specified LED.
		/// </summary>
		/// <param name="ledInstance">The LED to change the state on.</param>
		/// <param name="state">The On or Off state of the LED.</param>
		/// <returns>Returns an awaitable task.</returns>
		public Task SetState(LedInstance ledInstance, LedState state)
		{
			if (ledInstance == LedInstance.GreenStatus)
			{
				if (this.GreenStatus != null) this.GreenStatus.Write(state == LedState.On ? GpioPinValue.High : GpioPinValue.Low);
			}
			else
			{
				if (this.RedPower != null) this.RedPower.Write(state == LedState.On ? GpioPinValue.High : GpioPinValue.Low);
			}

			return Task.FromResult(0);
		}

		#region IDisposable
		/// <summary>
		/// Performs application-defined tasks associated with freeing, 
		/// releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			// ***
			// *** Dispose the GPIO pin for the red LED
			// ***
			if (this.RedPower != null)
			{
				this.RedPower.Dispose();
				this.RedPower = null;
			}

			// ***
			// *** Dispose the GPIO pin for the green LED
			// ***
			if (this.GreenStatus != null)
			{
				this.GreenStatus.Dispose();
				this.GreenStatus = null;
			}
		}
		#endregion
	}
}
