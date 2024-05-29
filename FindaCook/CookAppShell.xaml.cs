using Microsoft.Maui.Controls;

namespace FindaCook
{
    public partial class CookAppShell : Shell
    {
        private readonly string _cookInfoId;

        public CookAppShell(string cookInfoId)
        {
            InitializeComponent();
            _cookInfoId = cookInfoId;
            // Pass cookInfoId to the necessary pages
        }
    }
}
