namespace Windows.Devices.Gpio.Led
{
	/// <summary>
	/// The exception that is thrown when the requested sequence key has not been defined.
	/// </summary>
	public class SequenceDoesNotExistException : LedException
	{
		/// <summary>
		/// Initializes an instance of SequenceDoesNotExistException with the given
		/// sequence name.
		/// </summary>
		/// <param name="name"></param>
		public SequenceDoesNotExistException(string name)
			: base(string.Format("The LED Manager does not contain a sequenced named '{0}'.", name))
		{
		}
	}
}