﻿using Microsoft.UI;
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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinMessage.Windows.Pages
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class ChatPage : Page
	{
		public ChatPage()
		{
			this.InitializeComponent();
			InitializeConversations();
		}


		public List<Conversation> Conversations { get; set; } = new();

		private void InitializeConversations()
		{
			Conversations.Add(new Conversation
			{
				DisplayName = "Sarah Johnson",
				Id = Guid.NewGuid().ToString(),
				LastMessageTime = "11:42 PM",
				LatestMessage = "Sure thing, I will talk to you soon",
				ProfilePictureUrl = @"ms-appx:///Assets/Images/person-1.png"
			});
			Conversations.Add(new Conversation
			{
				DisplayName = "Nour Smith",
				Id = Guid.NewGuid().ToString(),
				LastMessageTime = "10:56 PM",
				LatestMessage = "Have good night!!",
				ProfilePictureUrl = @"ms-appx:///Assets/Images/person-2.png"
			});
			Conversations.Add(new Conversation
			{
				DisplayName = "John Smith",
				Id = Guid.NewGuid().ToString(),
				LastMessageTime = "Yesterday",
				LatestMessage = "Bye bye!",
				ProfilePictureUrl = @"ms-appx:///Assets/Images/person-3.png"
			});
			Conversations.Add(new Conversation
			{
				DisplayName = "John Smith",
				Id = Guid.NewGuid().ToString(),
				LastMessageTime = "Yesterday",
				LatestMessage = "🚀🚀🚀🚀",
				ProfilePictureUrl = @"ms-appx:///Assets/Images/person-4.png"
			});
		}
	}
}
