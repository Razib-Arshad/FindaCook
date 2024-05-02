using FindaCook.Maui.ViewModels;


namespace FindaCook.Views;


public partial class HomePage : ContentPage
{
    private int currentIndex = 0;
    private readonly int maxIndex = 1;
    private readonly TimeSpan slideInterval = TimeSpan.FromSeconds(3);
    private readonly System.Threading.Timer timer;
    public HomePage()
    {
        InitializeComponent();

        timer = new System.Threading.Timer(TimerCallback, null, slideInterval, slideInterval);
    }

    private void TimerCallback(object state)
    {
        
        currentIndex = (currentIndex + 1) % (maxIndex + 1);   
        MainThread.BeginInvokeOnMainThread(() =>
        {
            imageSlider.Position = currentIndex;
        });
    }

}