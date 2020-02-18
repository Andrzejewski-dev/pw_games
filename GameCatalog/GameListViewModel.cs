using GameInterfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using GameCatalog.Properties;

namespace GameCatalog
{
    public class GameListViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<GameViewModel> _games;

        private ListCollectionView view;

        private ICommand addGameCommand;
        
        private ICommand deleteGameCommand;

        private RelayCommand filterDataCommand;

        private GameViewModel selectedGame;

        private GameViewModel editedGame;

        private RelayCommand addGameToListCommand;

        private ICommand groupByYearCommand;

        private ICommand clearGroupingCommand;

        private string _filterName;

        private bool _isEdit;

        private Type catalogType;

        private Type gameType;

        public GameListViewModel()
        {
            initDatabase();
            _games = new ObservableCollection<GameViewModel>();
            view = (ListCollectionView)CollectionViewSource.GetDefaultView(_games);
            GetAllGames();
            //addGameCommand = new AddGameCommand(this);
            addGameCommand = new RelayCommand(param => this.AddNewGame() );
            deleteGameCommand = new RelayCommand(param => this.DeleteGame() );
            filterDataCommand = new RelayCommand(param => this.FilterData() );
            addGameToListCommand = new RelayCommand(param => this.AddGameToList());
            groupByYearCommand = new RelayCommand(param => this.GroupByYear());
            clearGroupingCommand = new RelayCommand(param => this.ClearGrouping());
            _filterName = "";
        }

        public void initDatabase()
        {
            Settings settings = new Settings();
            Assembly a = Assembly.UnsafeLoadFrom(settings.gameDLL);

            foreach (var t in a.GetTypes())
            {
                if (typeof(ICatalog).IsAssignableFrom(t))                
                    catalogType = t;
                if (typeof(IGame).IsAssignableFrom(t))
                    gameType = t;
                
            }
        }

        public ObservableCollection<GameViewModel> Games
        {
            get { return _games; }
            set 
            { 
                _games = value;
                RaisePropertyChanged("Games");
            }

        }

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void GetAllGames()
        {
            MethodInfo methodInfo = catalogType.GetMethod("GetGames");
            List<IGame> games = (List<IGame>)methodInfo.Invoke(null, null);

            foreach ( var c in games)
            {
                _games.Add(new GameViewModel(c));
            }
        }

        public GameViewModel AddNewGame()
        {
            ConstructorInfo constructorInfo = gameType.GetConstructor(new Type[] { });
            IGame game = (IGame)constructorInfo.Invoke(new object[] { });
            GameViewModel cvm = new GameViewModel(game);

            EditedGame = cvm;
            SelectedGame = null;
            return cvm;
        }

        public void DeleteGame()
        {
            _games.Remove(EditedGame);
            EditedGame = null;
        }

        public ICommand AddGameCommand
        {
            get { return addGameCommand; }
            set { addGameCommand = value; }
        }

        public ICommand DeleteGameCommand
        {
            get { return deleteGameCommand; }
            set { deleteGameCommand = value; }
        }

        public ICommand AddGameToListCommand
        {
            get { return addGameToListCommand; }
            /*set { 
                
            }*/
        }

        public GameViewModel SelectedGame
        {
            set 
            { 
                selectedGame = value;
                RaisePropertyChanged("SelectedGame");
                if ( EditedGame == null || !EditedGame.HasErrors)
                {
                    EditedGame = selectedGame;
                }
                
            }
            get
            {
                return selectedGame;
            }
        }

        public GameViewModel EditedGame
        {
            set
            {
                editedGame = value;
                RaisePropertyChanged("EditedGame");
            }

            get
            {
                return editedGame;
            }
        }

        public string FilterName 
        {
            set { _filterName = value; }
            get { return _filterName; }
        }

        public RelayCommand FilterDataCommand
        {
            get { return filterDataCommand; }
        }

        private void FilterData()
        {
            if (FilterName.Length > 0)
                view.Filter = (c) => ((GameViewModel)c).Name.Contains(FilterName);
            else
                view.Filter = null;
        }

        public bool IsEdit
        {
            set
            {
                _isEdit = value;
                RaisePropertyChanged("IsEdit");
            }
            get
            {
                return _isEdit;
            }
        }

        private void AddGameToList()
        {
            if (EditedGame != null && !EditedGame.HasErrors)
            {
                _games.Add(EditedGame);
                EditedGame = null;
            }
        }

        private void GroupByYear()
        {
            view.SortDescriptions.Add(new SortDescription("ProdYear", ListSortDirection.Ascending));
            view.GroupDescriptions.Add(new PropertyGroupDescription("ProdYear"));
        }

        public ICommand GroupByYearCommand
        {
            get { return groupByYearCommand; }
        }

        private void ClearGrouping()
        {
            view.SortDescriptions.Clear();
            view.GroupDescriptions.Clear();
        }

        public ICommand ClearGrouppingCommand
        {
            get { return clearGroupingCommand; }
        }
    }
}
