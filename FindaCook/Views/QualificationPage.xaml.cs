using FindaCook.Maui.ViewModels;
using FindaCook.Models;
using FindaCook.ViewModels;
using Microsoft.Maui.Controls;

namespace FindaCook;

public partial class QualificationPage : ContentPage
{

	public QualificationPage(Person person)
	{
		InitializeComponent();
        BindingContext = new QualificationPageViewModel(person);
    }
   

    
    
}