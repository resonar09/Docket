using Docket.Model;
using Docket.UI.Data;
using Docket.UI.Event;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Docket.UI.ViewModel
{
    public class NavigationItemViewModel : ViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;
        private string _displayMember;

        public NavigationItemViewModel(int id, string displayMember, IEventAggregator eventAggregator)
        {
            Id = id;
            DisplayMember = displayMember;
            _eventAggregator = eventAggregator;
            OpenClientDetailViewCommand = new DelegateCommand(OnOpenClientDetailView);

        }

        public int Id { get; }

        public string DisplayMember
        {
            get
            {
                return _displayMember;
            }
            set
            {
                _displayMember = value;
                OnPropertyChanged();
            }
        }
        public ICommand OpenClientDetailViewCommand { get; }

        private void OnOpenClientDetailView()
        {
            _eventAggregator.GetEvent<OpenClientDetailViewEvent>()
                .Publish(Id);
        }
    }
}
