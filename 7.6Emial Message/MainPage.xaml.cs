using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace _7._6Emial_Message
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }


        //打开发送邮件程序
        private async void ComposeEmail(string emailAddress, string messageBody, StorageFile attachmentFile)
        {
            var emailMessage = new Windows.ApplicationModel.Email.EmailMessage();
            emailMessage.Body = messageBody;

            if (attachmentFile != null)
            {
                var stream = Windows.Storage.Streams.RandomAccessStreamReference.CreateFromFile(attachmentFile);

                var attachment = new Windows.ApplicationModel.Email.EmailAttachment(
                    attachmentFile.Name,
                    stream);

                emailMessage.Attachments.Add(attachment);
            }

            if (emailAddress != null)
            {
                var emailRecipient = new Windows.ApplicationModel.Email.EmailRecipient(emailAddress);
                emailMessage.To.Add(emailRecipient);
            }

            await Windows.ApplicationModel.Email.EmailManager.ShowComposeNewEmailAsync(emailMessage);

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var folder = KnownFolders.PicturesLibrary;
            StorageFile sampleFile1 = await folder.GetFileAsync("meinv01.PNG");
            ComposeEmail("mazg1987@outlook.com", "哈哈", sampleFile1);
        }


        //发短信
        private async void ComposeSms(string phoneNumber,string messageBody, StorageFile attachmentFile, string mimeType)
        {
            var chatMessage = new Windows.ApplicationModel.Chat.ChatMessage();
            chatMessage.Body = messageBody;

            if (attachmentFile != null)
            {
                var stream = Windows.Storage.Streams.RandomAccessStreamReference.CreateFromFile(attachmentFile);

                var attachment = new Windows.ApplicationModel.Chat.ChatMessageAttachment(mimeType,stream);

                chatMessage.Attachments.Add(attachment);
            }

            if (phoneNumber != null)
            {
                chatMessage.Recipients.Add(phoneNumber);
            }
            await Windows.ApplicationModel.Chat.ChatMessageManager.ShowComposeSmsMessageAsync(chatMessage);
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var folder = KnownFolders.PicturesLibrary;
            StorageFile sampleFile = await folder.GetFileAsync("meinv01.PNG");
            ComposeSms("+18888888888", "你好", sampleFile, "image/png");
        }
    }
}
