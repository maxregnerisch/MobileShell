using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace MobSDes
{
    /// <summary>
    /// Main page of the Windows 11 Mobile-like shell
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private DispatcherTimer _timer;
        private ObservableCollection<TileItem> _tileItems;

        public MainPage()
        {
            this.InitializeComponent();
            
            // Initialize timer for clock
            _timer = new DispatcherTimer();
            _timer.Tick += Timer_Tick;
            _timer.Interval = new TimeSpan(0, 0, 1); // Update every second
            _timer.Start();
            
            // Initialize tiles
            InitializeTiles();
            
            // Set initial time
            UpdateTime();
            
            // Register for window size changed event
            Window.Current.SizeChanged += Current_SizeChanged;
        }

        private void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            // Adjust UI based on window size
            if (e.Size.Width < 640)
            {
                // Compact mode for small screens
                TileGridView.ItemsPanel = new ItemsPanelTemplate()
                {
                    Template = XamlReader.Load("<ItemsWrapGrid Orientation=\"Horizontal\" MaximumRowsOrColumns=\"2\" xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"/>") as ItemsWrapGrid
                };
            }
            else
            {
                // Normal mode for larger screens
                TileGridView.ItemsPanel = new ItemsPanelTemplate()
                {
                    Template = XamlReader.Load("<ItemsWrapGrid Orientation=\"Horizontal\" MaximumRowsOrColumns=\"4\" xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"/>") as ItemsWrapGrid
                };
            }
        }

        private void Timer_Tick(object sender, object e)
        {
            UpdateTime();
        }

        private void UpdateTime()
        {
            TimeDisplay.Text = DateTime.Now.ToString("HH:mm");
        }

        private void InitializeTiles()
        {
            _tileItems = new ObservableCollection<TileItem>
            {
                new TileItem { Name = "Phone", Icon = "\uE717", TileColor = new SolidColorBrush(Color.FromArgb(255, 0, 120, 215)) },
                new TileItem { Name = "Messages", Icon = "\uE8BD", TileColor = new SolidColorBrush(Color.FromArgb(255, 0, 153, 188)) },
                new TileItem { Name = "Mail", Icon = "\uE715", TileColor = new SolidColorBrush(Color.FromArgb(255, 106, 0, 255)) },
                new TileItem { Name = "Photos", Icon = "\uEB9F", TileColor = new SolidColorBrush(Color.FromArgb(255, 0, 204, 106)) },
                new TileItem { Name = "Calendar", Icon = "\uE787", TileColor = new SolidColorBrush(Color.FromArgb(255, 255, 140, 0)) },
                new TileItem { Name = "Store", Icon = "\uE719", TileColor = new SolidColorBrush(Color.FromArgb(255, 104, 118, 138)) },
                new TileItem { Name = "Maps", Icon = "\uE707", TileColor = new SolidColorBrush(Color.FromArgb(255, 76, 74, 72)) },
                new TileItem { Name = "Weather", Icon = "\uE753", TileColor = new SolidColorBrush(Color.FromArgb(255, 0, 183, 195)) },
                new TileItem { Name = "Camera", Icon = "\uE722", TileColor = new SolidColorBrush(Color.FromArgb(255, 232, 17, 35)) },
                new TileItem { Name = "Settings", Icon = "\uE713", TileColor = new SolidColorBrush(Color.FromArgb(255, 96, 96, 96)) },
                new TileItem { Name = "Music", Icon = "\uE8D6", TileColor = new SolidColorBrush(Color.FromArgb(255, 234, 0, 94)) },
                new TileItem { Name = "Files", Icon = "\uE8B7", TileColor = new SolidColorBrush(Color.FromArgb(255, 16, 137, 62)) }
            };

            TileGridView.ItemsSource = _tileItems;
        }

        private void TileGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var clickedItem = e.ClickedItem as TileItem;
            if (clickedItem != null)
            {
                // Show a dialog with the tile name
                ShowTileClickDialog(clickedItem);
            }
        }

        private async void ShowTileClickDialog(TileItem tile)
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = tile.Name,
                Content = $"You clicked on the {tile.Name} tile. This feature is not implemented yet.",
                CloseButtonText = "OK"
            };

            await dialog.ShowAsync();
            
            // Easter egg: If user clicks on Settings 5 times, show the Windows logo
            if (tile.Name == "Settings")
            {
                tile.ClickCount++;
                if (tile.ClickCount >= 5)
                {
                    WindowsLogo.Visibility = Visibility.Visible;
                    await Task.Delay(3000); // Show for 3 seconds
                    WindowsLogo.Visibility = Visibility.Collapsed;
                    tile.ClickCount = 0;
                }
            }
        }
    }

    /// <summary>
    /// Represents a tile item in the Start screen
    /// </summary>
    public class TileItem
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public Brush TileColor { get; set; }
        public int ClickCount { get; set; } = 0;
    }
}
