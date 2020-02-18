using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Validators
{
    public class StringLengthValidator:ValidationRule
    {
        private int _minLength;

        public int MinLength
        {
            set { _minLength = value; }
            get { return _minLength; }
        }

        public StringLengthValidator()
        {
            _minLength = int.MaxValue;
        }

        public string Property { get; set; }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            

            
                if (((string)value).Length < MinLength)
                {
                    return new ValidationResult(false, Property + " is too short. Should be at least " + MinLength);
                }
            
                return new ValidationResult(true, null);
            
        }
    }
}
