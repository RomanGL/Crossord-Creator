using CC.Core.Services.Interfaces;
using FirstFloor.ModernUI.Windows.Controls;
using System.Windows;

namespace CCApp.Services
{
    public sealed class MessagesService : IMessagesService
    {
        public MessagesService(IBlurService blurService)
        {
            _blurService = blurService;
        }

        public void ShowMessage(string text)
        {
            ShowMessage(text, DefaultMessageDialogTitle, MessageBoxButton.OK);
        }

        public MessageBoxResult ShowMessage(string text, MessageBoxButton button)
        {
            return ShowMessage(text, DefaultMessageDialogTitle, button);
        }

        public void ShowMessage(string text, string title)
        {
            ShowMessage(text, title, MessageBoxButton.OK);
        }

        public MessageBoxResult ShowMessage(string text, string title, MessageBoxButton button)
        {
            _blurService.Blur();
            var result = ModernDialog.ShowMessage(text, title, button);
            _blurService.UnBlur();
            return result;
        }

        private readonly IBlurService _blurService;
        private const string DefaultMessageDialogTitle = "Crossword Creator";
    }
}
