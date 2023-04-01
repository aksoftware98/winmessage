using Microsoft.UI.Xaml;
using WinRT; // required to support Window.As<ICompositionSupportsSystemBackdrop>()
using Microsoft.UI.Composition.SystemBackdrops;
using System.Runtime.InteropServices;
using Windows.System;
using Windows.UI;

namespace WinMessage.Windows
{

	public class MicaActivator
	{

		WindowsSystemDispatcherQueueHelper m_wsdqHelper; // See below for implementation.
		MicaController m_backdropController;
		SystemBackdropConfiguration m_configurationSource;
		Window window;

		public MicaActivator(Window window)
		{
			this.window = window;
			TrySetSystemBackdrop();
		}

		private void Window_Activated(object sender, WindowActivatedEventArgs args)
		{
			m_configurationSource.IsInputActive = args.WindowActivationState != WindowActivationState.Deactivated;
		}

		private void Window_Closed(object sender, WindowEventArgs args)
		{
			// Make sure any Mica/Acrylic controller is disposed
			// so it doesn't try to use this closed window.
			if (m_backdropController != null)
			{
				m_backdropController.Dispose();
				m_backdropController = null;
			}
			window.Activated -= Window_Activated;
			m_configurationSource = null;
		}

		private void Window_ThemeChanged(FrameworkElement sender, object args)
		{
			if (m_configurationSource != null)
			{
				SetConfigurationSourceTheme();
			}
		}

		private void SetConfigurationSourceTheme()
		{
			switch (((FrameworkElement)window.Content).ActualTheme)
			{
				case ElementTheme.Dark: m_configurationSource.Theme = Microsoft.UI.Composition.SystemBackdrops.SystemBackdropTheme.Dark; break;
				case ElementTheme.Light: m_configurationSource.Theme = Microsoft.UI.Composition.SystemBackdrops.SystemBackdropTheme.Light; break;
				case ElementTheme.Default: m_configurationSource.Theme = Microsoft.UI.Composition.SystemBackdrops.SystemBackdropTheme.Default; break;
			}
		}


		bool TrySetSystemBackdrop()
		{
			if (Microsoft.UI.Composition.SystemBackdrops.MicaController.IsSupported())
			{
				m_wsdqHelper = new WindowsSystemDispatcherQueueHelper();
				m_wsdqHelper.EnsureWindowsSystemDispatcherQueueController();

				m_configurationSource = new SystemBackdropConfiguration();
				window.Activated += Window_Activated;
				window.Closed += Window_Closed;
				((FrameworkElement)window.Content).ActualThemeChanged += Window_ThemeChanged;


				m_configurationSource.IsInputActive = true;
				SetConfigurationSourceTheme();

				m_backdropController = new Microsoft.UI.Composition.SystemBackdrops.MicaController();
				//m_backdropController.TintOpacity = 5f;
				//m_backdropController.TintColor = Color.FromArgb(22, 22, 22, 255);
				m_backdropController.FallbackColor = Color.FromArgb(53, 30, 40, 255);
				m_backdropController.TintOpacity = 0.075f;
				m_backdropController.LuminosityOpacity = 0.075f;
				// Enable the system backdrop.
				// Note: Be sure to have "using WinRT;" to support the Window.As<...>() call.
				m_backdropController.AddSystemBackdropTarget(window.As<Microsoft.UI.Composition.ICompositionSupportsSystemBackdrop>());
				m_backdropController.SetSystemBackdropConfiguration(m_configurationSource);
				return true;
			}

			return false;
		}
	}


	class WindowsSystemDispatcherQueueHelper
	{
		[StructLayout(LayoutKind.Sequential)]
		struct DispatcherQueueOptions
		{
			internal int dwSize;
			internal int threadType;
			internal int apartmentType;
		}

		[DllImport("CoreMessaging.dll")]
		private static extern int CreateDispatcherQueueController([In] DispatcherQueueOptions options, [In, Out, MarshalAs(UnmanagedType.IUnknown)] ref object dispatcherQueueController);

		object m_dispatcherQueueController = null;
		public void EnsureWindowsSystemDispatcherQueueController()
		{
			if (DispatcherQueue.GetForCurrentThread() != null)
			{
				// one already exists, so we'll just use it.
				return;
			}

			if (m_dispatcherQueueController == null)
			{
				DispatcherQueueOptions options;
				options.dwSize = Marshal.SizeOf(typeof(DispatcherQueueOptions));
				options.threadType = 2;    // DQTYPE_THREAD_CURRENT
				options.apartmentType = 2; // DQTAT_COM_STA

				CreateDispatcherQueueController(options, ref m_dispatcherQueueController);
			}
		}
	}

}