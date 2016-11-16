using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace ClinVitta.Classes
{
    public class ControlValidationHelper
    {
        private string _message;

        public ControlValidationHelper(string message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("Erro.");
            }

            _message = message;
            ThrowValidationError = true;
        }

        public bool ThrowValidationError
        {
            get;
            set;
        }

        public object ValidationError
        {
            get { return null; }
            set
            {
                if (ThrowValidationError)
                {
                    throw new ValidationException(_message);
                }
            }
        }
    }
}
