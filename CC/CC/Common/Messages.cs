using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using FirstFloor.ModernUI.Windows.Controls;

namespace Crossword_Application_Modern
{
    /// <summary>
    /// Статический класс, предназначенный для вывода диалоговых окон.
    /// </summary>
    public static class Messages
    {
        /// <summary>
        /// Отображает окно, сообщающее о недопустимости пустого элемента в коллекции.
        /// </summary>
        public static void EmptyElement()
        {
            string caption = "Пустой элемент";
            string message = "Невозможно добавить пустой элемент в коллекцию.\nЗаполните пустые поля и повторите попытку.";

            BlurHandler.Blur(true);
            ModernDialog.ShowMessage(message, caption, System.Windows.MessageBoxButton.OK);
            BlurHandler.Blur(false);
        }

        /// <summary>
        /// Отображает окно, предлагающее сохранить изменения в файле.
        /// </summary>
        /// <param name="fileName">Имя файла.</param>
        /// <returns>Результат работы окна.</returns>
        public static MessageBoxResult SaveTheChanges(string fileName)
        {
            string caption = "Имеются несохраненные изменения";
            string message = String.Format("Сохранить изменения в файле \"{0}\"?", fileName);

            BlurHandler.Blur(true);
            MessageBoxResult result = ModernDialog.ShowMessage(message, caption,
                System.Windows.MessageBoxButton.YesNoCancel);
            BlurHandler.Blur(false);
            return result;
        }

        /// <summary>
        /// Отображает окно, сообщающее о том, что перенесенный на поле элемент не поддерживает.
        /// </summary>
        public static void NotSupportedDroppedElementInCanvas()
        {
            string caption = "Элемент не поддерживается";
            string message = "Переместить в это поле можно только элементы из списка \"СЛОВА\"";

            BlurHandler.Blur(true);
            ModernDialog.ShowMessage(message, caption, System.Windows.MessageBoxButton.OK);
            BlurHandler.Blur(false);
        }

        /// <summary>
        /// Отображает окно, сообщающее об ошибке открытия файла.
        /// </summary>
        /// <param name="ex">Текст ошибки.</param>
        public static void OpenFileError(string ex)
        {
            string caption = "Ошибка при открытии";
            string message = String.Format("{0}\n\n{1}",
                "К сожалению, при открытии файла произошла ошибка.",
                ex);

            BlurHandler.Blur(true);
            ModernDialog.ShowMessage(message, caption, System.Windows.MessageBoxButton.OK);
            BlurHandler.Blur(false);
        }

        /// <summary>
        /// Отображает окно, сообщающее об ошибке сохранения файла.
        /// </summary>
        /// <param name="ex">Текст ошибки.</param>
        public static void SaveFileError(string ex)
        {
            string caption = "Ошибка при сохранении";
            string message = String.Format("{0}\n\n{1}",
                "К сожалению, выполнить сохранение файла не удалось.",
                ex);

            BlurHandler.Blur(true);
            ModernDialog.ShowMessage(message, caption, System.Windows.MessageBoxButton.OK);
            BlurHandler.Blur(false);
        }

        /// <summary>
        /// Отображает окно, спрашивающее о действии, которое требуется совершить с загруженным списком терминов.
        /// </summary>
        /// <returns>Результат работы окна.</returns>
        public static MessageBoxResult SelectOperationWithLoadedListFile()
        {
            string caption = "Список определений";
            string message = String.Format("{0}",
                "Создать на основе загруженного списка новый кроссворд? Для добавления слов в текущий список выберите \"No\".");

            BlurHandler.Blur(true);
            MessageBoxResult result = ModernDialog.ShowMessage(message, caption,
                System.Windows.MessageBoxButton.YesNoCancel);
            BlurHandler.Blur(false);
            return result;
        }

        /// <summary>
        /// Отображает окно, спрашивающее о действии, которое требуется выполнить с загруженным файлом сетки кроссворда.
        /// </summary>
        /// <returns>Результат работы окна.</returns>
        public static MessageBoxResult SelectOperationWithLoadedGridFile()
        {
            string caption = "Кроссворд";
            string message = String.Format("{0}",
                "Вы хотите заполнить данный кроссворд? Для изменения кроссворда выберите \"No\".");

            BlurHandler.Blur(true);
            MessageBoxResult result = ModernDialog.ShowMessage(message, caption,
                System.Windows.MessageBoxButton.YesNoCancel);
            BlurHandler.Blur(false);
            return result;
        }

        /// <summary>
        /// Отображает окно, которое сообщает о доступности новой версии и приложения и предлагает ее загрузить.
        /// </summary>
        /// <returns>Результат работы окна.</returns>
        public static MessageBoxResult NewVersionAvaible()
        {
            string caption = "Доступно обновление";
            string message = String.Format("{0}\n{1}",
                "Стала доступна новая версия Crossword Creator. Рекомендуется установить обновление.",
                "Вы хотите сделать это сейчас?");

            BlurHandler.Blur(true);
            MessageBoxResult result = ModernDialog.ShowMessage(message, caption,
                System.Windows.MessageBoxButton.YesNo);
            BlurHandler.Blur(false);
            return result;
        }

        /// <summary>
        /// Отображает окно, сообщающее об успешном завершении загрузки обновления.
        /// </summary>
        public static void DownloadUpdateCompleteSuccessfully()
        {
            string caption = "Загрузка завершена";
            string message = String.Format("{0}\n{1}",
                "Загрузка обновления успешно завершена.",
                "После нажатия кнопки \"ОК\" будет запущена программа установки, а приложение Crossword Creator будет закрыто.");

            BlurHandler.Blur(true);
            ModernDialog.ShowMessage(message, caption,
                System.Windows.MessageBoxButton.OK);
            BlurHandler.Blur(false);
        }

        /// <summary>
        /// Отображает окно, сообщающее, что выполнить проверку обновлений не удалось.
        /// </summary>
        /// <param name="ex">Текст ошибки.</param>
        public static void CheckUpdatesFailure(string ex)
        {
            string caption = "Не удалось выполнить проверку обновлений";
            string message = String.Format("{0}\n\n{1}",
                "При проверке обновлений произошла ошибка. Проверьте подключение к интернету и повторите попытку.",
                ex);

            BlurHandler.Blur(true);
            ModernDialog.ShowMessage(message, caption,
                System.Windows.MessageBoxButton.OK);
            BlurHandler.Blur(false);
        }

        /// <summary>
        /// Отображает окно, сообщающее об отсутствии обновлений.
        /// </summary>
        public static void NoUpdatesAvailable()
        {
            string caption = "Обновления отсутствуют";
            string message = "На данный момент вы имеете самую свежую версию Crossword Creator.";

            BlurHandler.Blur(true);
            ModernDialog.ShowMessage(message, caption,
                System.Windows.MessageBoxButton.OK);
            BlurHandler.Blur(false);
        }

        /// <summary>
        /// Отображает окно, сообщающее об ошибке при загрузке обновления.
        /// </summary>
        /// <param name="ex">Текст ошибки.</param>
        public static void DownloadUpdateCompleteFailure(string ex)
        {
            string caption = "Не удалось выполнить загрузку";
            string message = String.Format("{0}\n{1}\n\n{2}",
                "При загрузке обновления произошла ошибка. Проверьте подключение к интернету и повторите попытку.",
                "Техническая информация:",
                ex);

            BlurHandler.Blur(true);
            ModernDialog.ShowMessage(message, caption,
                System.Windows.MessageBoxButton.OK);
            BlurHandler.Blur(false);
        }

        /// <summary>
        /// Отображает окно, которое сообщает, что кроссворд заполнен неверно, и предлагает показать ошибки.
        /// </summary>
        /// <returns>Результат работы окна.</returns>
        public static MessageBoxResult CrosswordFilledIsNotCorrectly()
        {
            string caption = "Кроссворд заполнен неверно";
            string message = "При заполнении кроссворда были допущены ошибки. Вы хотите увидеть их?";

            BlurHandler.Blur(true);
            MessageBoxResult result = ModernDialog.ShowMessage(message, caption,
                System.Windows.MessageBoxButton.YesNo);
            BlurHandler.Blur(false);
            return result;
        }

        /// <summary>
        /// Отображает окно, сообщающее о том, что кроссворд заполнен верно.
        /// </summary>
        public static void CrosswordFilledIsCorrectly()
        {
            string caption = "Кроссворд заполнен верно";
            string message = "Замечательно! Вы заполнили крассворд абсолютно верно!";

            BlurHandler.Blur(true);
            ModernDialog.ShowMessage(message, caption, System.Windows.MessageBoxButton.OK);
            BlurHandler.Blur(false);
        }

        /// <summary>
        /// Отображает  окно с сообщением об отсутствии пользовательского фона приложения.
        /// </summary>
        public static void UserBackgroundFileNotFound()
        {
            string caption = "Отсутствует файл фона";
            string message = String.Format("{0} {1}\n\n{2}",
                "Была установлена тема оформления с пользовательским фоновым изображением \"C:\\1.jpg\", однако оно не было найдено.",
                "Сейчас установлена стандартная тема оформления.",
                "Если вы хотите снова использовать свое фоновое изображение, скопируйте его в корень диска \"C:\\\", указав ему имя \"1\", и выберите соответствующую тему в настройках Crossword Creator. Обратите внимание, что изображение должно быть в формате \"jpg\".");

            BlurHandler.Blur(true);
            ModernDialog.ShowMessage(message, caption, MessageBoxButton.OK);
            BlurHandler.Blur(false);
        }

        /// <summary>
        /// Отображает окно сообщения.
        /// </summary>
        /// <param name="message">Текст, который требуется отобразить в окне.</param>
        /// <param name="caption">Заголовок окна.</param>
        /// <param name="buttons">Кнопки, отображаемые в окне сообщения.</param>
        public static MessageBoxResult ShowMessage(string message, string caption, MessageBoxButton buttons)
        {
            BlurHandler.Blur(true);
            MessageBoxResult result = ModernDialog.ShowMessage(message, caption, buttons);
            BlurHandler.Blur(false);
            return result;
        }

        /// <summary>
        /// Отображает окно сообщения.
        /// </summary>
        /// <param name="message">Текст, который требуется отобразить в окне.</param>
        /// <param name="caption">Заголовок окна.</param>
        public static void ShowMessage(string message, string caption)
        {
            BlurHandler.Blur(true);
            ModernDialog.ShowMessage(message, caption, MessageBoxButton.OK);
            BlurHandler.Blur(false);
        }

        /// <summary>
        /// Отображает окно сообщения.
        /// </summary>
        /// <param name="message">Текст, который требуется отобразить в окне.</param>
        public static void ShowMessage(string message)
        {
            BlurHandler.Blur(true);
            ModernDialog.ShowMessage(message, "", MessageBoxButton.OK);
            BlurHandler.Blur(false);
        }
    }
}
