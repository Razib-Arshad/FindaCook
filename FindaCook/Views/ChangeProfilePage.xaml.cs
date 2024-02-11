
namespace FindaCook;

public partial class ChangeProfilePage : ContentPage
{
    public ChangeProfilePage()
    {
        InitializeComponent();
    }

//    if(KeyBoardExtensions.IsSoftKeyboardShowing(myEntry))
//        {
//        await KeyboardExtensions.HideKeyboardAsync(myEntry);
//}
//else{
//await KeyboardExtensions.ShowKeyboardAsync(myEntry);
//}

    private async void ChangeProfilePictureButton_Clicked(object sender, EventArgs e)
    {
        // Use MediaPicker to select or capture a new profile picture.
        try
        {
            var mediaResult = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Select a profile picture"
            });

            if (mediaResult != null)
            {
                // Update the profile image with the selected image.
                ProfileImage.Source = ImageSource.FromStream(() => mediaResult.OpenReadAsync().Result);
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions or display an error message.
            await DisplayAlert("Error", "Unable to change profile picture: " + ex.Message, "OK");
        }
    }
    private void SaveChangesButton_Clicked(object sender, EventArgs e)
    {
        // Get the user's input from the Entry and Editor controls
        string name = NameEntry.Text;
        string phoneNumber = PhoneEntry.Text;
        string address = AddressEditor.Text;
        

        // Here, you can save the changes to the user's profile, for example, by updating a database.

        // Display a confirmation message
        DisplayAlert("Profile Updated", "Your profile has been updated.", "OK");
    }
    private async void ChangePasswordButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ResetPassword());
    }
}