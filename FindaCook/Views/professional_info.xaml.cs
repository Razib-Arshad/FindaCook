using FindaCook.Models;
using FindaCook.ViewModels;
using FindaCook.Views;
using Microsoft.Maui.Controls;

namespace FindaCook
{
    public partial class professional_info : ContentPage
    {

        public ProfessionalInfoViewModel ViewModel { get; set; }

        public professional_info(Person person,QualificationInfo qualification)
        {
            InitializeComponent();
            BindingContext = new ProfessionalInfoViewModel(person, qualification);
        }


       

    }
}


