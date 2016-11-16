using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace ClinVitta
{
    public partial class FrmAgendaConsulta : ChildWindow
    {
        public FrmAgendaConsulta()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnPesquisaPaciente_Click(object sender, RoutedEventArgs e)
        {
            frmConsultaPaciente pespaciente = new frmConsultaPaciente();
            pespaciente.Show();
        }

        private void btnPesquisaMedico_Click(object sender, RoutedEventArgs e)
        {
            frmPesquisaMedico pesmedico = new frmPesquisaMedico();
            pesmedico.Show();
        }
    }
}

