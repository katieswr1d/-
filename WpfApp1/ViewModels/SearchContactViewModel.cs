using System.ComponentModel;
using System.Windows.Input;
using WpfApp1.Comands;

namespace WpfApp1.ViewModels
{
    public class SearchContactViewModel : INotifyPropertyChanged
    {
        private readonly MainViewModel _mainViewModel;
        private string _searchQuery;

        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                _searchQuery = value;
                OnPropertyChanged(nameof(SearchQuery));
            }
        }

        public ICommand SearchCommand { get; }
        public ICommand CancelCommand { get; }

        public SearchContactViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            SearchCommand = new RelayCommand(ExecuteSearch);
            CancelCommand = new RelayCommand(ExecuteCancel);
        }

        private void ExecuteSearch()
        {
            // Используем метод SearchContacts из MainViewModel
            _mainViewModel.SearchContacts(SearchQuery);
        }

        private void ExecuteCancel()
        {
            // Логика отмены, если необходима
            SearchQuery = string.Empty; // Очистить поле поиска
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}