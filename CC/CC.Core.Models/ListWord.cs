using Prism.Mvvm;
using PropertyChanged;

namespace CC.Core.Models
{
    [ImplementPropertyChanged]
    public class ListWord : BindableBase
    {
        public int ID { get; set; }

        public string Question { get; set; }

        [DoNotNotify]
        public string Answer
        {
            get { return _answer; }
            set { SetProperty(ref _answer, value.ToLower()); }
        }

        private string _answer;
    }
}
