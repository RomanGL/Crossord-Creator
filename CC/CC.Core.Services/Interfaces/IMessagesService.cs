using System.Windows;

namespace CC.Core.Services.Interfaces
{
    public interface IMessagesService
    {
        void ShowMessage(string text);
        void ShowMessage(string text, string title);
        MessageBoxResult ShowMessage(string text, MessageBoxButton button);
        MessageBoxResult ShowMessage(string text, string title, MessageBoxButton button);
    }
}
