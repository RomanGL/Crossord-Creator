using CC.Core.Models;
using CC.Core.Services.Interfaces;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC.Core.Services.Context
{
    /// <summary>
    /// Представляет контекст редактора списка терминов.
    /// </summary>
    public sealed class ListEditorContext : EditorContext
    {
        public ListEditorContext(IMessagesService messagesService, IBlurService blurService)
        {
            _messagesService = messagesService;
            _blurService = blurService;

            Words = new ObservableCollection<ListWord>();
        }

        public ObservableCollection<ListWord> Words { get; set; }

        public override void New()
        {
            if (IsDirty && !Save(false))
                return;

            Words.Clear();

            FileName = "Безыменный список";
            FilePath = null;
        }

        public override void Open()
        {
            throw new NotImplementedException();
        }

        public override void Open(string filePath)
        {
            throw new NotImplementedException();
        }

        public override bool Save(bool asNew)
        {
            asNew = String.IsNullOrEmpty(FilePath);

            if (asNew)
            {
                _blurService.Blur();

                var sfd = new SaveFileDialog();
                sfd.Title = "Сохранить список определений";
                sfd.Filter = DialogsFilter;

                if (sfd.ShowDialog() == true)
                {
                    return true;
                }

                _blurService.UnBlur();
            }

            return false;
        }

        private readonly IMessagesService _messagesService;
        private readonly IBlurService _blurService;
        private const string DialogsFilter = "Список терминов Crossword Creator (*.cwtx)|*.cwtx|Список терминов Crossword Creator 1.x (*.cwtf)|*.cwtf";
    }
}
