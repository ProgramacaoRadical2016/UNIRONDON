using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Globalization;
using System.Windows.Data;
using System.Linq;
using System.Collections.Generic;
using System.Windows;


namespace ClinVitta.Classes
{
    public static class Funcoes
    {
        public static void TrataTextBox(ref TextBox txt, TipoFiltro pTipoFiltro, TextChangedEventHandler pTextChangedEvent, bool pUpdateBinding)
        {
            txt.TextChanged -= pTextChangedEvent;
            int posicao = txt.SelectionStart, tamanho = txt.Text.Length;
            txt.Text = Funcoes.TrataCampoNumerico(txt.Text, pTipoFiltro);

            if (posicao == tamanho)
                txt.SelectionStart = txt.Text.Length;
            else
                txt.SelectionStart = posicao;

            if (pUpdateBinding)
            {
                Funcoes.AtualizaBinding(txt);
            }

            txt.TextChanged += pTextChangedEvent;
        }

        public static string TrataCampoNumerico(string pValor, TipoFiltro pTipoFiltro)
        {
            string retorno = "";
            foreach (var chr in pValor)
                if (char.IsDigit(chr))
                    retorno += chr;
            if (retorno.Length > 0)
            {
                if (pTipoFiltro == TipoFiltro.Valor)
                {
                    try
                    {
                        return (Convert.ToDecimal(retorno) / 100).ToString("N2", new CultureInfo("pt-BR"));
                    }
                    catch (Exception)
                    {
                        return "0,00";
                    }
                }
                else
                {
                    try
                    {
                        Convert.ToInt64(retorno);
                    }
                    catch (Exception)
                    {
                        return "0";
                    }

                    return retorno;
                }
            }
            else
                return retorno;
        }

        public static bool VerificaDigito(Key pKey)
        {
            bool shiftKeyPressd = (Keyboard.Modifiers & ModifierKeys.Shift) != 0;
            if (pKey == Key.Tab || shiftKeyPressd && pKey == Key.Tab)
                return false;
            if (!(((pKey >= Key.D0 && pKey <= Key.D9) || (pKey >= Key.NumPad0 && pKey <= Key.NumPad9)) && !shiftKeyPressd))
                return true;
            else
                return false;
        }

        public static void AtualizaBinding(FrameworkElement pControl)
        {
            BindingExpression binding = null;

            if (pControl.GetType() == typeof(TextBox))
                binding = pControl.GetBindingExpression(TextBox.TextProperty);
            else if (pControl.GetType() == typeof(ComboBox))
                binding = pControl.GetBindingExpression(ComboBox.SelectedValueProperty);
            else if (pControl.GetType() == typeof(TextBlock))
                binding = pControl.GetBindingExpression(TextBlock.TextProperty);
            else if (pControl.GetType() == typeof(CheckBox))
                binding = pControl.GetBindingExpression(CheckBox.IsCheckedProperty);
            else if (pControl.GetType() == typeof(RadioButton))
                binding = pControl.GetBindingExpression(RadioButton.IsCheckedProperty);
            else if (pControl.GetType() == typeof(Label))
                binding = pControl.GetBindingExpression(Label.ContentProperty);

            if (binding != null)
                binding.UpdateSource();
        }

        public static decimal Truncar(decimal pValor)
        {
            string[] valor = pValor.ToString().Split(',');
            return Convert.ToDecimal(valor[0] + "," + valor[1].Substring(0, 2));
        }

        public static void ClearControlError(Control control)
        {
            BindingExpression b = control.GetBindingExpression(Control.TagProperty);

            if (b != null)
            {
                ((ControlValidationHelper)b.DataItem).ThrowValidationError = false;
                b.UpdateSource();
            }
        }

        public static void SetControlError(Control control, string errorMessage, bool notifyValError, bool valExceptions)
        {
            ControlValidationHelper validationHelper = new ControlValidationHelper(errorMessage);
            Binding binding = new Binding("ValidationError")
            {
                Mode = BindingMode.TwoWay,
                NotifyOnValidationError = notifyValError,
                ValidatesOnExceptions = valExceptions,
                UpdateSourceTrigger = UpdateSourceTrigger.Explicit,
                Source = validationHelper
            };

            control.SetBinding(Control.TagProperty, binding);
            control.GetBindingExpression(Control.TagProperty).UpdateSource();
        }

        public static void CopyValues<T>(T target, T source, List<string> listaNomesPropriedades)
        {
            Type t = typeof(T);

            IEnumerable<System.Reflection.PropertyInfo> properties = null;
            if (listaNomesPropriedades != null)
                properties = t.GetProperties().Where(prop => prop.CanRead && prop.CanWrite && listaNomesPropriedades.Contains(prop.Name));
            else
                properties = t.GetProperties().Where(prop => prop.CanRead && prop.CanWrite);

            foreach (var prop in properties)
            {
                var value = prop.GetValue(source, null);
                if (value != null)
                    prop.SetValue(target, value, null);
            }
        }

        public static void CopyPropertyValues(object source, object destination, List<string> listaPropriedades)
        {
            System.Reflection.PropertyInfo[] destProperties = null;
            System.Reflection.PropertyInfo[] sourceProperties = null;

            if (listaPropriedades == null)
            {
                destProperties = destination.GetType().GetProperties();
                sourceProperties = source.GetType().GetProperties();
            }
            else
            {
                destProperties = destination.GetType().GetProperties().Where(a => listaPropriedades.Contains(a.Name)).ToArray();
                sourceProperties = source.GetType().GetProperties().Where(a => listaPropriedades.Contains(a.Name)).ToArray();
            }

            foreach (var sourceProperty in sourceProperties)
            {
                foreach (var destProperty in destProperties)
                {
                    if (destProperty.Name == sourceProperty.Name && destProperty.PropertyType.IsAssignableFrom(sourceProperty.PropertyType))
                    {
                        destProperty.SetValue(destination, sourceProperty.GetValue(source, new object[] { }), new object[] { });
                        break;
                    }
                }
            }
        }

        public static void CallService<T>(Type pTypeClient, EventHandler<T> callback, params object[] parameters) where T : EventArgs
        {
            var proxy = Activator.CreateInstance(pTypeClient);
            string acao = typeof(T).Name.Replace("CompletedEventArgs", String.Empty);

            pTypeClient.GetEvent(acao + "Completed").AddEventHandler(proxy, callback);
            pTypeClient.InvokeMember(acao + "Async", System.Reflection.BindingFlags.InvokeMethod, null, proxy, parameters);
        }

        public static string TrataCampoStringOracle(string pValor)
        {
            if (pValor == null) return pValor;
            return pValor.Replace("'", "''");
        }
    }
}
