using System;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;  
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace ClinVitta.Classes
{
    public class  Filtros : Behavior<Control>
    {
        static bool datainvalida = false;

        public static DependencyProperty FilterProperty =
              DependencyProperty.RegisterAttached("TipoFiltro",
                                          typeof(TipoFiltro),
                                          typeof(TipoFiltro),
                                          new PropertyMetadata(TipoFiltro.Nenhum, VerificaTipo));

        public static void SetTipoFiltro(DependencyObject obj, TipoFiltro Value)
        {
            obj.SetValue(FilterProperty, Value);
        }

        public static TipoFiltro GetTipoFiltro(UIElement obj)
        {
            return (TipoFiltro)obj.GetValue(FilterProperty);
        }

        private static void VerificaTipo(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var txtBox = d as TextBox;
            if (txtBox != null)
            {
                switch ((TipoFiltro)e.NewValue)
                {
                    case TipoFiltro.Valor:
                        txtBox.MaxLength = 38;
                        txtBox.KeyDown += new KeyEventHandler(FiltroDigitosKeyDown);
                        txtBox.TextChanged += new TextChangedEventHandler(FiltroFormataValorTextChanged);
                        break;
                    case TipoFiltro.SomenteNumero:
                        if (txtBox.MaxLength == 0)
                            txtBox.MaxLength = 19;
                        txtBox.KeyDown += new KeyEventHandler(FiltroDigitosKeyDown);
                        txtBox.TextChanged += new TextChangedEventHandler(FiltroSomenteNumeroTextChanged);
                        break;
                    case TipoFiltro.Data:
                        txtBox.MaxLength = 10;
                        txtBox.KeyDown += new KeyEventHandler(FiltroDigitosDataKeyDown);
                        txtBox.TextChanged += new TextChangedEventHandler(FiltroDataTextChanged);
                        txtBox.LostFocus += FiltroDataOnLostFocus;
                        break;
                    case TipoFiltro.DataMesAno:
                        txtBox.MaxLength = 7;
                        txtBox.KeyDown += new KeyEventHandler(FiltroDigitosDataMesAnoKeyDown);
                        txtBox.TextChanged += new TextChangedEventHandler(FiltroDataMesAnoTextChanged);
                        txtBox.LostFocus += FiltroDataMesAnoOnLostFocus;
                        break;
                    case TipoFiltro.UpperCase:
                        txtBox.TextChanged += new TextChangedEventHandler(FiltroUpperCaseTextChanged);
                        txtBox.LostFocus += new RoutedEventHandler(FiltroTrimLostFocus);
                        break;
                }
            }
        }





        static void FiltroTrimLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox txt = sender as TextBox;
            txt.TextChanged -= FiltroUpperCaseTextChanged;
            txt.Text = txt.Text.Trim();
            txt.TextChanged += new TextChangedEventHandler(FiltroUpperCaseTextChanged);
        }

        private static void FiltroDigitosKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = Funcoes.VerificaDigito(e.Key);
        }

        private static void FiltroDigitosDataKeyDown(object sender, KeyEventArgs e)
        {
            TextBox txt = sender as TextBox;
            txt.TextChanged -= FiltroDataTextChanged;
            StringBuilder sb = new StringBuilder();
            int contador = 0;

            while (contador < 10)
            {
                if (contador < txt.Text.Length)
                {
                    if (contador == 2 || contador == 5)
                        sb.Append("/");
                    else if (char.IsDigit(txt.Text[contador]))
                        sb.Append(txt.Text[contador]);
                    else
                        sb.Append("_");
                }
                else
                {
                    if (contador == 2 || contador == 5)
                        sb.Append("/");
                    else
                        sb.Append("_");
                }
                contador++;
            }
            txt.Text = sb.ToString();

            e.Handled = Funcoes.VerificaDigito(e.Key);
            if (!e.Handled && (e.Key != Key.Tab && e.Key != Key.Delete && e.Key != Key.Back && e.Key != Key.Tab))
            {
                if (txt.SelectionStart < 10)
                {
                    int selecao = txt.SelectionStart;
                    if (selecao == 2 || selecao == 5)
                        selecao++;
                    txt.Text = txt.Text.Remove(selecao, 1);
                    txt.SelectionStart = selecao;
                }
            }

            if (txt.Text.Length > 10)
                txt.Text = txt.Text.Remove(10, (10 - txt.Text.Length));

            txt.TextChanged += new TextChangedEventHandler(FiltroDataTextChanged);
        }

        private static void FiltroDigitosDataMesAnoKeyDown(object sender, KeyEventArgs e)
        {
            TextBox txt = sender as TextBox;
            txt.TextChanged -= FiltroDataMesAnoTextChanged;
            StringBuilder sb = new StringBuilder();
            int contador = 0;

            while (contador < 7)
            {
                if (contador < txt.Text.Length)
                {
                    if (contador == 2)
                        sb.Append("/");
                    else if (char.IsDigit(txt.Text[contador]))
                        sb.Append(txt.Text[contador]);
                    else
                        sb.Append("_");
                }
                else
                {
                    if (contador == 2)
                        sb.Append("/");
                    else
                        sb.Append("_");
                }
                contador++;
            }

            txt.Text = sb.ToString();

            e.Handled = Funcoes.VerificaDigito(e.Key);
            if (!e.Handled && (e.Key != Key.Tab && e.Key != Key.Delete && e.Key != Key.Back && e.Key != Key.Tab))
            {
                if (txt.SelectionStart < 7)
                {
                    int selecao = txt.SelectionStart;
                    if (selecao == 2)
                        selecao++;
                    txt.Text = txt.Text.Remove(selecao, 1);
                    txt.SelectionStart = selecao;
                }
            }

            if (txt.Text.Length > 7)
                txt.Text = txt.Text.Remove(7, (7 - txt.Text.Length));

            txt.TextChanged += new TextChangedEventHandler(FiltroDataMesAnoTextChanged);
        }

        private static void FiltroFormataValorTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txt = sender as TextBox;
            Funcoes.TrataTextBox(ref txt, TipoFiltro.Valor, FiltroFormataValorTextChanged, false);
        }

        private static void FiltroSomenteNumeroTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txt = sender as TextBox;
            Funcoes.TrataTextBox(ref txt, TipoFiltro.SomenteNumero, FiltroFormataValorTextChanged, false);
        }

        private static void FiltroUpperCaseTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txt = sender as TextBox;
            int posicao = txt.SelectionStart;
            txt.Text = txt.Text.ToUpper();
            txt.SelectionStart = posicao;
        }

        private static void FiltroDataTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txt = sender as TextBox;
            StringBuilder sb = new StringBuilder();
            txt.TextChanged -= FiltroDataTextChanged;
            int selection = txt.SelectionStart, contador = 0;

            while (contador < 10)
            {
                if (contador < txt.Text.Length)
                {
                    if (contador == 2 || contador == 5)
                        sb.Append("/");
                    else if (char.IsDigit(txt.Text[contador]))
                        sb.Append(txt.Text[contador]);
                    else
                        sb.Append("_");
                }
                else
                {
                    if (contador == 2 || contador == 5)
                        sb.Append("/");
                    else
                        sb.Append("_");
                }
                contador++;
            }

            bool ok = true;
            for (int i = 0; i < sb.Length; i++)
            {
                if ((i == 2 || i == 5) && sb[i] != '/')
                    ok = false;
                else if (i != 2 && i != 5 && !char.IsDigit(sb[i]))
                    ok = false;
            }

            if (ok)
            {
                try
                {
                    datainvalida = false;
                    DateTime.Parse(sb.ToString());
                    Funcoes.ClearControlError(txt);
                }
                catch (Exception)
                {
                    sb.Clear();
                    sb.Append("__/__/____");
                    selection = 0;

                    BindingExpression b = txt.GetBindingExpression(Control.TagProperty);
                    if (b != null)
                    {
                        if (!((ControlValidationHelper)b.DataItem).ThrowValidationError)
                        {
                            Funcoes.SetControlError(txt, "Data inválida.", true, true);
                            datainvalida = true;
                        }
                    }
                    else
                    {
                        Funcoes.SetControlError(txt, "Data inválida.", true, true);
                        datainvalida = true;
                    }
                }
            }

            txt.Text = sb.ToString();
            txt.TextChanged += new TextChangedEventHandler(FiltroDataTextChanged);
            txt.SelectionStart = selection;
            //Funcoes.AtualizaBinding(txt);
        }

        private static void FiltroDataMesAnoTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txt = sender as TextBox;
            StringBuilder sb = new StringBuilder();
            txt.TextChanged -= FiltroDataMesAnoTextChanged;
            int selection = txt.SelectionStart, contador = 0;

            while (contador < 7)
            {
                if (contador < txt.Text.Length)
                {
                    if (contador == 2)
                        sb.Append("/");
                    else if (char.IsDigit(txt.Text[contador]))
                        sb.Append(txt.Text[contador]);
                    else
                        sb.Append("_");
                }
                else
                {
                    if (contador == 2)
                        sb.Append("/");
                    else
                        sb.Append("_");
                }
                contador++;
            }

            bool ok = true;
            for (int i = 0; i < sb.Length; i++)
            {
                if ((i == 2) && sb[i] != '/')
                    ok = false;
                else if (i != 2 && !char.IsDigit(sb[i]))
                    ok = false;
            }

            if (ok)
            {
                try
                {
                    datainvalida = false;
                    DateTime.Parse("01/" + sb.ToString());
                    Funcoes.ClearControlError(txt);
                }
                catch (Exception)
                {
                    sb.Clear();
                    sb.Append("__/____");
                    selection = 0;

                    BindingExpression b = txt.GetBindingExpression(Control.TagProperty);
                    if (b != null)
                    {
                        if (!((ControlValidationHelper)b.DataItem).ThrowValidationError)
                        {
                            Funcoes.SetControlError(txt, "Data inválida.", true, true);
                            datainvalida = true;
                        }
                    }
                    else
                    {
                        Funcoes.SetControlError(txt, "Data inválida.", true, true);
                        datainvalida = true;
                    }
                }
            }

            txt.Text = sb.ToString();
            txt.TextChanged += new TextChangedEventHandler(FiltroDataMesAnoTextChanged);
            txt.SelectionStart = selection;
            //Funcoes.AtualizaBinding(txt);
        }

        private static void FiltroDataOnLostFocus(object sender, RoutedEventArgs routedEventArgs)
        {
            TextBox txt = sender as TextBox;
            StringBuilder sb = new StringBuilder();
            txt.TextChanged -= FiltroDataTextChanged;
            int contador = 0;

            while (contador < 10)
            {
                if (contador < txt.Text.Length)
                {
                    if (contador == 2 || contador == 5)
                        sb.Append("/");
                    else if (char.IsDigit(txt.Text[contador]))
                        sb.Append(txt.Text[contador]);
                    else
                        sb.Append("_");
                }
                else
                {
                    if (contador == 2 || contador == 5)
                        sb.Append("/");
                    else
                        sb.Append("_");
                }
                contador++;
            }

            try
            {
                DateTime.Parse(sb.ToString());
                Funcoes.ClearControlError(txt);
            }
            catch (Exception)
            {
                sb.Clear();
                sb.Append("__/__/____");
                if (datainvalida)
                    Funcoes.ClearControlError(txt);
                txt.Text = sb.ToString();
            }

            txt.TextChanged += new TextChangedEventHandler(FiltroDataTextChanged);
            //Funcoes.AtualizaBinding(txt);
        }

        private static void FiltroDataMesAnoOnLostFocus(object sender, RoutedEventArgs routedEventArgs)
        {
            TextBox txt = sender as TextBox;
            StringBuilder sb = new StringBuilder();
            txt.TextChanged -= FiltroDataMesAnoTextChanged;
            int contador = 0;

            while (contador < 7)
            {
                if (contador < txt.Text.Length)
                {
                    if (contador == 2)
                        sb.Append("/");
                    else if (char.IsDigit(txt.Text[contador]))
                        sb.Append(txt.Text[contador]);
                    else
                        sb.Append("_");
                }
                else
                {
                    if (contador == 2)
                        sb.Append("/");
                    else
                        sb.Append("_");
                }

                contador++;
            }

            try
            {
                DateTime.Parse(sb.ToString());
                Funcoes.ClearControlError(txt);
            }
            catch (Exception)
            {
                sb.Clear();
                sb.Append("__/____");
                if (datainvalida)
                    Funcoes.ClearControlError(txt);
                txt.Text = sb.ToString();
            }

            txt.TextChanged += new TextChangedEventHandler(FiltroDataMesAnoTextChanged);
            //Funcoes.AtualizaBinding(txt);
        }
    }
}