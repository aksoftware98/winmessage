using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using WinMessage.Windows.Pages;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinMessage.Windows
{
	/// <summary>
	/// An empty window that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainWindow : Window
	{
		public MainWindow()
		{
			this.InitializeComponent();
			ExtendsContentIntoTitleBar = true;
		
			new MicaActivator(this);
			MainFrame.SourcePageType = typeof(ChatPage);
			GetAppWindowAndPresenter();
			_presenter.SetBorderAndTitleBar(false, false);
		}

		public void GetAppWindowAndPresenter()
		{
			var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
			WindowId myWndId = Win32Interop.GetWindowIdFromWindow(hWnd);
			_apw = AppWindow.GetFromWindowId(myWndId);
			_presenter = _apw.Presenter as OverlappedPresenter;
		}

		private AppWindow _apw;
		private OverlappedPresenter _presenter;

	}

	public class Conversation
	{
		public string Id { get; set; }

		public string ProfilePicture { get; set; }

		public string DisplayName { get; set; }
		
		public string LatestMessage { get; set; }

		public string LastMessageTime { get; set; }
		
		
	}
}
