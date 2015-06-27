using System;

namespace Windows.Devices.Gpio.Led
{
	/// <summary>
	/// Represents errors that occur during application execution on
	/// objects in the Windows.Devices.Led namespace.
	/// </summary>
	public abstract class LedException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the Windows.Devices.Led.LedException class.
		/// </summary>
		public LedException() : base()
		{
		}

		/// <summary>
		/// Initializes a new instance of the System.Exception class with 
		/// a specified error message.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public LedException(string message) : base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the System.Exception class with a 
		/// specified error message and a reference to the inner exception 
		/// that is the cause of this exception.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="innerException">The exception that is the cause of the 
		/// current exception, or a null reference (Nothing in Visual Basic) 
		/// if no inner exception is specified.</param>
		public LedException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}