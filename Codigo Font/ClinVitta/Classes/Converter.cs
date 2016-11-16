using System;
using System.Windows;
using System.Windows.Data;
using System.Threading;
using System.Windows.Media;

namespace ClinVitta.Classes
{
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Thread.CurrentThread.CurrentCulture = Classes.ClinVittaAmbiente.Culture;
            Thread.CurrentThread.CurrentUICulture = Classes.ClinVittaAmbiente.Culture;

            string parametro = (string)parameter;

            if (parametro == "dd/MM/yyyy")
            {
                if (value == null)
                    return "__/__/____";
            }
            else if (parametro == "MM/yyyy")
            {
                if (value == null || value.ToString() == "" || value.ToString() == "__/____")
                    return "__/____";
            }


            DateTime dt;

            if (parametro == "MM/yyyy")
            {
                if (value.ToString().Length == 6)
                    dt = DateTime.Parse("01/" + value.ToString().Insert(2, "/"));
                else
                    dt = DateTime.Parse("01/" + value.ToString());
            }
            else
            {
                dt = DateTime.Parse(value.ToString());
                if (dt == DateTime.MinValue)
                    return null;
            }

            return dt.ToString(parametro);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Thread.CurrentThread.CurrentCulture = Classes.ClinVittaAmbiente.Culture;
            Thread.CurrentThread.CurrentUICulture = Classes.ClinVittaAmbiente.Culture;

            string val = (string)value;
            string parametro = (string)parameter;

            DateTime outDate;

            if (parametro == "dd/MM/yyyy")
            {
                if (DateTime.TryParse(val, out outDate))
                    return outDate;
            }
            else if (parametro == "MM/yyyy")
            {
                if (DateTime.TryParse(("01/" + val), out outDate))
                    return outDate.ToString("MM/yyyy");
                else
                    return "";
            }

            return null;
        }
    }

    public class IntegerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string format = (parameter == null) ? "" : parameter.ToString();
            return (value == null) ? String.Empty : Int64.Parse(value.ToString()).ToString(format);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                if (value != null && value.ToString() != String.Empty)
                {
                    return Int64.Parse(value.ToString());
                }
            }
            catch { }

            return null;
        }
    }
    public class DecimalFormatterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string format = (parameter == null) ? "N2" : parameter.ToString();
            return (value == null) ? String.Empty : decimal.Parse(value.ToString()).ToString(format);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                if (value != null && value.ToString() != String.Empty)
                {
                    return decimal.Parse(value.ToString());
                }
            }
            catch { }

            return null;
        }
    }
    public class RadioButtonConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;
            return (value.ToString() == parameter.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value.Equals(true)) return parameter;
            return DependencyProperty.UnsetValue;
        }
    }
    public class CheckBoxConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((string)value) == "S" ? true : false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((bool)value) == true ? "S" : "N";
        }
    }
    public class UpperLowerCaseConveter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;

            string valor = (string)value, parametro = (string)parameter;

            if (parametro == "UpperCase")
                return valor.ToUpper();
            else
                return valor.ToLower();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string valor = (string)value, parametro = (string)parameter;
            if (parametro == "UpperCase")
                return valor.ToUpper();
            else
                return valor.ToLower();
        }
    }
}
