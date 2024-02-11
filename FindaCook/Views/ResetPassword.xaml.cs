namespace FindaCook;

public partial class ResetPassword : ContentPage
{
	public ResetPassword()
	{
		InitializeComponent();
	}
    private void OnUpdateButtonClicked(object sender, System.EventArgs e)
    {
        var newPassword = NewPasswordEntry.Text;
        var retypePassword = RetypeNewPasswordEntry.Text;

        if (newPassword == retypePassword)
        {
            DisplayAlert("Success", "Password Updated Successfully!", "OK");
        }
        else
        {
            DisplayAlert("Error", "Passwords do not match, please retype the new password correctly.", "OK");
        }
    }
}