using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GameCatalog
{
    public class AddGameCommand:ICommand
    {
        private GameListViewModel _gameListVM;
        public AddGameCommand(GameListViewModel gameListVM)
        {
            _gameListVM = gameListVM;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _gameListVM.AddNewGame();
        }
    }
}
