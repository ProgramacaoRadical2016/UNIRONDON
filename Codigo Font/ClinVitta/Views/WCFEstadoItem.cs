using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace ClinVitta.Views
{
    public enum WCFEstadoItem : int
    {

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Novo = 0,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Original = 1,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Modificado = 2,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Deletado = 3,
    }
}