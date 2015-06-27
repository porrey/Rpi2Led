using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using Windows.Devices.Gpio.Led;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Porrey.RPi2.Lights
{
	public sealed partial class MainPage : Page, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged = null;

		/// <summary>
		/// Defines a Cancellation Token Source used to
		/// stop the sequence on the red LED
		/// </summary>
		private CancellationTokenSource _redCancellationTokenSource = null;

		/// <summary>
		/// Defines a Cancellation Token Source used to
		/// stop the sequence on the green LED
		/// </summary>
		private CancellationTokenSource _greenCancellationTokenSource = null;

		/// <summary>
		/// Constant used as the key for the
		/// Warning LED Sequence
		/// </summary>
		private const string WarningSequence = "Warning";

		/// <summary>
		/// Constant used as the key for the
		/// Error LED sequence
		/// </summary>
		private const string ErrorSequence = "Error";

		public MainPage()
		{
			this.InitializeComponent();
		}

		/// <summary>
		/// Gets the LED manager
		/// </summary>
		private LedManager LedManager { get; } = new LedManager();

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);

			// ***
			// *** Define and add a sequence where the LED flashes 3 times and then pauses.
			// *** This sequence is named "Warning".
			// ***
			this.LedManager.Add(WarningSequence, new LedSequence(new LedSequenceState() { Delay = Interval.Shorter, State = LedState.On },
																 new LedSequenceState() { Delay = Interval.Shorter, State = LedState.Off },
																 new LedSequenceState() { Delay = Interval.Shorter, State = LedState.On },
																 new LedSequenceState() { Delay = Interval.Shorter, State = LedState.Off },
																 new LedSequenceState() { Delay = Interval.Shorter, State = LedState.On },
																 new LedSequenceState() { Delay = Interval.Longer, State = LedState.Off }));


			// ***
			// *** Define and add a sequence where the LED flashes on and off slowly. This
			// *** sequence is named "Error".
			// ***
			this.LedManager.Add(ErrorSequence, new LedSequence(new LedSequenceState() { Delay = Interval.Long, State = LedState.On },
															   new LedSequenceState() { Delay = Interval.Long, State = LedState.Off }));


			// ***
			// *** Turn both LED's off to start
			// ***
			this.LedManager.SetState(LedInstance.RedPower, LedState.Off);
			this.LedManager.SetState(LedInstance.GreenStatus, LedState.Off);
		}

		private void GreenStartButton_Click(object sender, RoutedEventArgs e)
		{
			// ***
			// *** Create a new Cancellation Token Source and start the green LED
			// ***
			this.GreenSequenceIsRunning = true;
			_greenCancellationTokenSource = new CancellationTokenSource();
			LedManager.RunSequence(WarningSequence, LedInstance.GreenStatus, _greenCancellationTokenSource.Token);
		}

		private void GreenStopButton_Click(object sender, RoutedEventArgs e)
		{
			// ***
			// *** Stop the green LED by calling Cancel() on 
			// *** the Cancellation Token Source
			// ***
			if (_greenCancellationTokenSource != null)
			{
				_greenCancellationTokenSource.Cancel();
				this.GreenSequenceIsRunning = false;
			}
		}

		private void RedStartButton_Click(object sender, RoutedEventArgs e)
		{
			// ***
			// *** Create a new Cancellation Token Source and start the red LED
			// ***
			this.RedSequenceIsRunning = true;
			_redCancellationTokenSource = new CancellationTokenSource();
			LedManager.RunSequence(ErrorSequence, LedInstance.RedPower, _redCancellationTokenSource.Token);	
		}

		private void RedStopButton_Click(object sender, RoutedEventArgs e)
		{
			// ***
			// *** Stop the red LED by calling Cancel() on 
			// *** the Cancellation Token Source
			// ***
			if (_redCancellationTokenSource != null)
			{
				_redCancellationTokenSource.Cancel();
				this.RedSequenceIsRunning = false;
			}
		}

		private bool _redSequenceIsRunning = false;
		public bool RedSequenceIsRunning
		{
			get
			{
				return _redSequenceIsRunning;
			}
			set
			{
				_redSequenceIsRunning = value;
				this.OnPropertyChanged();
				this.OnPropertyChanged(nameof(RedSequenceIsNotRunning));
			}
		}

		public bool RedSequenceIsNotRunning => !_redSequenceIsRunning;

		private bool _greenSequenceIsRunning = false;
		public bool GreenSequenceIsRunning
		{
			get
			{
				return _greenSequenceIsRunning;
			}
			set
			{
				_greenSequenceIsRunning = value;
				this.OnPropertyChanged();
				this.OnPropertyChanged(nameof(GreenSequenceIsNotRunning));
			}
		}

		public bool GreenSequenceIsNotRunning => !_greenSequenceIsRunning;

		private void OnPropertyChanged([CallerMemberName]string propertyName = null)
		{
			if (this.PropertyChanged != null && propertyName != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
