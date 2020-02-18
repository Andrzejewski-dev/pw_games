using GameInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GameCatalog
{
    public class GameViewModel : INotifyPropertyChanged,INotifyDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private IGame _game;
         
        public GameViewModel(IGame game)
        {
            _game = game;
            IsGameEdit = true;
        }

        [Required]
        [Key]
        [Range(0, 255)]
        public int Id
        {
            get { return _game.Id; }
            set 
            {
                IsGameEdit = true;
                _game.Id = value;
                RaisePropertyChanged("Id");
            }
        }

        [Required]
        [StringLength(100, MinimumLength=3, ErrorMessage = "To pole musi być co najmniej takie długie")]
        public string Name
        {
            get { return _game.Name; }
            set
            {
                IsGameEdit = true;
                _game.Name = value;
                RaisePropertyChanged("Name");
            }
        }

        [Required]
        [Range(1930,2020)]
        public int ProdYear
        {
            get { return _game.ProdYear; }
            set
            {
                IsGameEdit = true;
                _game.ProdYear = value;
                RaisePropertyChanged("ProdYear");
            }
        }

        [StringLength(1000)]
        public string Description
        {
            get { return _game.Description; }
            set
            {
                IsGameEdit = true;
                _game.Description = value;
                RaisePropertyChanged("Description");
            }
        }

     

        public void RaisePropertyChanged(string propertyName)
        {
            if ( PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
            if (propertyName != "HasErrors")
            {
                Validate();
            }
        }
    


        private readonly Dictionary<string, ICollection<string>> _validationErrors = new Dictionary<string, ICollection<string>>();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        private void RaiseErrorChanged( string property)
        {
            if( ErrorsChanged != null)
            {
                ErrorsChanged(this, new DataErrorsChangedEventArgs(property));
                RaisePropertyChanged("HasErrors");
            }
        }

        public System.Collections.IEnumerable GetErrors(string propertyName)
        {
            if ( string.IsNullOrEmpty(propertyName) || !_validationErrors.ContainsKey( propertyName ))
            {
                return null;
            }

            return _validationErrors[propertyName];

        }
        public bool HasErrors
        {
            
            get { return _validationErrors.Count > 0; }
        }

        private bool _isGameEdit;

        public bool IsGameEdit
        {
            set
            {
                _isGameEdit = value;
                RaisePropertyChanged("IsGameEdit");
            }
            get
            {
                return _isGameEdit;
            }
        }

        public void Validate()
        {
           
                var validationContext = new ValidationContext(this, null, null);
                var validationResults = new List<ValidationResult>();
                Validator.TryValidateObject(this, validationContext, validationResults, true);

                foreach (var kv in _validationErrors.ToList())
                {
                    if (validationResults.All(r => r.MemberNames.All(m => m != kv.Key)))
                    {
                        _validationErrors.Remove(kv.Key);
                        RaiseErrorChanged(kv.Key);
                    }
                }

                var q = from r in validationResults
                        from m in r.MemberNames
                        group r by m into g
                        select g;

                foreach (var prop in q)
                {
                    var messages = prop.Select(r => r.ErrorMessage).ToList();

                    if (_validationErrors.ContainsKey(prop.Key))
                    {
                        _validationErrors.Remove(prop.Key);
                    }
                    _validationErrors.Add(prop.Key, messages);
                    RaiseErrorChanged(prop.Key);
                }
           
        }
    }
}
