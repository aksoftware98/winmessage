using Microsoft.UI;
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
			InitializeMessages();
			ChatShadow.Receivers.Add(ProfileDetailsGrid);
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
				DisplayName = "Adrien Smith",
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

		public List<Message> Messages { get; set; } = new();

		private void InitializeMessages()
		{
			Messages.Add(new Message
			{
				IsSent = false,
				Id = Guid.NewGuid().ToString(),
				ProfilePictureUrl = null,
				Content = "Hi, how are you?",
				IsImage = false,
				Time = "11:40 PM"
			});
			Messages.Add(new Message
			{
				IsSent = false,
				Id = Guid.NewGuid().ToString(),
				ProfilePictureUrl = null,
				Content = "I'm soo curios to know how the work is going!!!!!",
				IsImage = false,
				Time = "11:40 PM"
			});
			
			Messages.Add(new Message
			{
				IsSent = true,
				Id = Guid.NewGuid().ToString(),
				ProfilePictureUrl = null,
				Content = "Heey Sarah!!",
				IsImage = false,
				Time = "11:40 PM"
			});
			Messages.Add(new Message
			{
				IsSent = false,
				Id = Guid.NewGuid().ToString(),
				ProfilePictureUrl = null,
				Content = "How is the design?",
				IsImage = false,
				Time = "11:40 PM"
			});
			Messages.Add(new Message
			{
				IsSent = true,
				Id = Guid.NewGuid().ToString(),
				ProfilePictureUrl = null,
				Content = "It's going crazy 😍🤣😍🤣",
				IsImage = false,
				Time = "11:40 PM"
			});
			Messages.Add(new Message
			{
				IsSent = true,
				Id = Guid.NewGuid().ToString(),
				ProfilePictureUrl = null,
				Content = "Designing Windows 11 stuff is amaaazing!!!!",
				IsImage = false,
				Time = "11:40 PM"
			});
			Messages.Add(new Message
			{
				IsSent = false,
				Id = Guid.NewGuid().ToString(),
				ProfilePictureUrl = null,
				Content = "Come on! send me some live screenshots!!",
				IsImage = false,
				Time = "11:41 PM"
			});
			Messages.Add(new Message
			{
				IsSent = true,
				Id = Guid.NewGuid().ToString(),
				ProfilePictureUrl = null,
				Content = "Sure thing!",
				IsImage = false,
				Time = "11:41 PM"
			});
			Messages.Add(new Message
			{
				IsSent = true,
				Id = Guid.NewGuid().ToString(),
				ProfilePictureUrl = @"ms-appx:///Assets/Images/ChatImage.png",
				Content = "Currently working on the new video feature",
				IsImage = true,
				Time = "11:42 PM"
			});
			Messages.Add(new Message
			{
				IsSent = false,
				Id = Guid.NewGuid().ToString(),
				ProfilePictureUrl = null,
				Content = "Lovely! 😍 😍 Planning to support audio and video call??",
				IsImage = false,
				Time = "11:43 PM"
			});
			Messages.Add(new Message
			{
				IsSent = true,
				Id = Guid.NewGuid().ToString(),
				ProfilePictureUrl = null,
				Content = "Currently working on the new video feature",
				IsImage = false,
				Time = "11:43 PM"
			});

		}
	}

	public class Message
	{
		public string Id { get; set; }

		public bool IsSent { get; set; }

		public bool IsImage { get; set; }

		public string Content { get; set; }

		public string Time { get; set; }

		public string ProfilePictureUrl { get; set; }
		
	}

	public class MessageTemplateSelector : DataTemplateSelector
	{

		public DataTemplate SentMessageDataTemplate { get; set; }

		public DataTemplate ReceivedMessageDataTemplate { get; set; }

		public DataTemplate SentImagesMessageDataTemplate { get; set; }

		public DataTemplate ReceivedImagesMessageDataTemplate { get; set; }

		protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
		{
			var message = item as Message;
			if (message.IsSent && !message.IsImage)
			{
				return SentMessageDataTemplate;
			}
			else if (message.IsSent && message.IsImage)
			{
				return SentImagesMessageDataTemplate;
			}
			else if (!message.IsSent && !message.IsImage)
			{
				return ReceivedMessageDataTemplate;
			}
			else
			{
				return ReceivedImagesMessageDataTemplate;
			}
		}
	}
}
