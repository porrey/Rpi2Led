using System.Collections;
using System.Collections.Generic;

namespace Windows.Devices.Gpio.Led
{
	/// <summary>
	/// Defines a sequence of states for the LED including the
	/// On or Off state and the length of time the LED is in
	/// this given state.
	/// </summary>
	public class LedSequence : IReadOnlyList<LedSequenceState>
	{
		private readonly List<LedSequenceState> _sequence = new List<LedSequenceState>();

		/// <summary>
		/// Initializes a instance of Windows.Devices.Led.LedSequence with
		/// the given list of LedSequenceState objects.
		/// </summary>
		/// <param name="sequence">One or more instances of Windows.Devices.Led.LedSequenceState</param>
		public LedSequence(params LedSequenceState[] sequence)
		{
			_sequence.AddRange(sequence);
		}

		/// <summary>
		/// Initializes a instance of Windows.Devices.Led.LedSequence with
		/// the given list of LedSequenceState objects. 
		/// </summary>
		/// <param name="sequence">One or more instances of Windows.Devices.Led.LedSequenceState</param>
		public LedSequence(IEnumerable<LedSequenceState> sequence)
		{
			_sequence.AddRange(sequence);
		}

		/// <summary>
		/// Gets the element at the specified index in the read-only list.
		/// </summary>
		/// <param name="index">The zero-based index of the element to get.</param>
		/// <returns>The element at the specified index in the read-only list.</returns>
		public LedSequenceState this[int index] => _sequence[index];

		/// <summary>
		/// Gets the number of elements in the collection.
		/// </summary>
		public int Count => _sequence.Count;

		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>A System.Collections.Generic.IEnumerator`1 that can 
		/// be used to iterate through the collection.</returns>
		public IEnumerator<LedSequenceState> GetEnumerator() => _sequence.GetEnumerator();

		/// <summary>
		/// Returns an enumerator that iterates through a collection.
		/// </summary>
		/// <returns>An System.Collections.IEnumerator object that 
		/// can be used to iterate through the collection.</returns>
		IEnumerator IEnumerable.GetEnumerator() => _sequence.GetEnumerator();
	}
}
