using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace ClinVitta.Classes
{
    public class ValidacaoClienteSummaryItem
    {
        public CampoInvalido Campo { get; set; }
        public int? NumeroEndereco { get; set; }
        public ValidationSummaryItem ItemValidationSummary { get; set; }

        public ValidacaoClienteSummaryItem(CampoInvalido pCampo, int? pNumeroEndereco, ValidationSummaryItem pItemValidationSummary)
        {
            Campo = pCampo;
            NumeroEndereco = pNumeroEndereco;
            ItemValidationSummary = pItemValidationSummary;
        }
    }
}