using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstFloor.ModernUI.Windows.Navigation;
using CC.Core.Services.Interfaces;
using Microsoft.Practices.Unity;
using Prism.Commands;
using PropertyChanged;
using FirstFloor.ModernUI.Windows.Controls;
using System.Collections.ObjectModel;
using CC.Core.Models;
using CC.Core.Services.Context;

namespace CC.Core.ViewModels
{
    [ImplementPropertyChanged]
    public class ListEditorViewModel : ViewModelBase
    {
        public ListEditorViewModel(INavigationParametersResolver navigationParametersResolver, IMessagesService messagesService,
            IBlurService blurService)
        {
            _navigationParametersResolver = navigationParametersResolver;
            _messagesService = messagesService;
            _blurService = blurService;
                       
            _context = new ListEditorContext(_messagesService, _blurService);

            SaveCommand = new DelegateCommand<string>(OnSaveCommand);
            OpenCommand = new DelegateCommand(OnOpenCommand);
            NewCommand = new DelegateCommand(OnNewCommand);
            AddPairCommand = new DelegateCommand(OnAddPairCommand);
        }

        #region Properties

        [DoNotNotify]
        public DelegateCommand<string> SaveCommand { get; private set; }

        [DoNotNotify]
        public DelegateCommand OpenCommand { get; private set; }

        [DoNotNotify]
        public DelegateCommand NewCommand { get; private set; }

        [DoNotNotify]
        public DelegateCommand AddPairCommand { get; private set; }

        [DoNotNotify]
        public ObservableCollection<ListWord> Words { get { return _context.Words; } }

        public string CurrentQuestion { get; set; }

        public string CurrentAnswer { get; set; }

        public ListWord CurrentWord { get; set; }

        #endregion

        public override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        #region Commands Handlers

        private async void OnSaveCommand(string isSaveAs)
        {
            bool asNew = false;
            bool.TryParse(isSaveAs, out asNew);

            await Task.Run(() => _context.Save(asNew));
        }

        private void OnOpenCommand()
        {
            _context.Open();
        }

        private void OnNewCommand()
        {
            _context.New();
        }
        
        private void OnAddPairCommand()
        {
            if (String.IsNullOrWhiteSpace(CurrentAnswer))
            {
                _messagesService.ShowMessage("Невозможно добавить пустой элемент в коллекцию.\nЗаполните поле ответа и повторите попытку.",
                    "Пустой элемент");
                return;
            }

            var word = new ListWord
            {
                Answer = CurrentAnswer,
                Question = CurrentQuestion
            };
            InsertPair(word);

            _context.IsDirty = true;
        }

        #endregion

        private void OpenFile(string filePath)
        {
            _context.Open(filePath);
        }

        private async void InsertPair(ListWord word)
        {
            Words.Insert(0, word);
            await UpdateIndexes();
        }

        private async Task UpdateIndexes()
        {
            await Task.Run(() =>
            {
                for (int i = 0; i < Words.Count; i++)
                {
                    Words[i].ID = i + 1;
                }
            });
        }

        private readonly INavigationParametersResolver _navigationParametersResolver;
        private readonly IMessagesService _messagesService;
        private readonly IBlurService _blurService;
        private readonly ListEditorContext _context;
    }
}
