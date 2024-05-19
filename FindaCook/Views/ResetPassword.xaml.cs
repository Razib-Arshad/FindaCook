using FindaCook.Services;

namespace FindaCook;

public partial class ResetPassword : ContentPage
{
    private readonly LoginService _loginService;
	public ResetPassword()
	{
		InitializeComponent();
        _loginService = new LoginService();
	}
    private async void OnUpdateButtonClicked(object sender, System.EventArgs e)
    {
        var oldPassword = OldPasswordEntry.Text;
        var newPassword = NewPasswordEntry.Text;
        var retypePassword = RetypeNewPasswordEntry.Text;

        if (newPassword != retypePassword)
        {
            await DisplayAlert("Error", "Passwords do not match, please retype the new password correctly.", "OK");

        }
        if(oldPassword == newPassword)
        {
            await DisplayAlert("Error", "Old Password and New Password cannot be same", "OK");

        }

        var isPasswordReset = await _loginService.ResetPassword(oldPassword, newPassword);

        if (isPasswordReset)
        {
            await DisplayAlert("Success", "Password Reset Successfully", "Ok");

        }
        else
        {
            await DisplayAlert("Failed", "Password Reset Failed", "Ok");
        }
    }
}