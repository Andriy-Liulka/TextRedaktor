using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Task_2_Correct
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TextBlock currentColumn;
        private TextBlock currentLine;
        private TextBox textBox;
        public MainWindow()
        {
            InitializeComponent();
            CreateWindowContent();
        }
        private void CreateWindowContent()
        {
            Separator separator1 = new Separator();
            Separator separator2 = new Separator();

            var menuItem31 = new MenuItem { Header = "Left" };
            menuItem31.Click += OnClickAlignLeft;
            var menuItem32 = new MenuItem { Header = "Center" };
            menuItem32.Click += OnClickAlignCenter;
            var menuItem33 = new MenuItem { Header = "Right" };
            menuItem33.Click += OnClickAlignRight;

            var menuItem21 = new MenuItem { Header = "New" };
            menuItem21.Click += OnClickOpen;
            var menuItem22 = new MenuItem { Header = "Open" };
            menuItem22.Click += OnClickOpen;
            var menuItem23 = new MenuItem { Header = "Exit" };
            menuItem23.Click += OnClickExit;
            var menuItem24 = new MenuItem { Header = "Cut" };
            menuItem24.Click += OnClickCut;
            var menuItem25 = new MenuItem { Header = "Copy" };
            menuItem25.Click += OnClickCopy;
            var menuItem26 = new MenuItem { Header = "Paste" };
            menuItem26.Click += OnClickPaste;
            var menuItem27 = new MenuItem { Header = "Align" };
            menuItem27.Items.Add(menuItem31);
            menuItem27.Items.Add(menuItem32);
            menuItem27.Items.Add(menuItem33);

            var menuItem11 = new MenuItem { Header = "File" };
            menuItem11.Items.Add(menuItem21);
            menuItem11.Items.Add(menuItem22);
            menuItem11.Items.Add(separator1);
            menuItem11.Items.Add(menuItem23);

            var menuItem12 = new MenuItem { Header = "Edit" };
            menuItem12.Items.Add(menuItem24);
            menuItem12.Items.Add(menuItem25);
            menuItem12.Items.Add(menuItem26);

            var menuItem13 = new MenuItem { Header = "Format" };
            menuItem13.Items.Add(menuItem27);

            var menu = new Menu { IsMainMenu = true };
            menu.Items.Add(menuItem11);
            menu.Items.Add(menuItem12);
            menu.Items.Add(menuItem13);

            var image1 = new Image { Source = new BitmapImage(new Uri("Icons\\Cut.png", UriKind.Relative)) };
            var image2 = new Image { Source = new BitmapImage(new Uri("Icons\\Copy.png", UriKind.Relative)) };
            var image3 = new Image { Source = new BitmapImage(new Uri("Icons\\Paste.png", UriKind.Relative)) };
            var image4 = new Image { Source = new BitmapImage(new Uri("Icons\\AlignLeft.png", UriKind.Relative)) };
            var image5 = new Image { Source = new BitmapImage(new Uri("Icons\\AlignCenter.png", UriKind.Relative)) };
            var image6 = new Image { Source = new BitmapImage(new Uri("Icons\\AlignRight.png", UriKind.Relative)) };

            var textBlockTT1 = new TextBlock { Foreground = Brushes.Black, Text = "Cut" };
            var textBlockTT2 = new TextBlock { Foreground = Brushes.Black, Text = "Copy" };
            var textBlockTT3 = new TextBlock { Foreground = Brushes.Black, Text = "Paste" };
            var textBlockTT4 = new TextBlock { Foreground = Brushes.Black, Text = "AlignLeft" };
            var textBlockTT5 = new TextBlock { Foreground = Brushes.Black, Text = "AlignCenter" };
            var textBlockTT6 = new TextBlock { Foreground = Brushes.Black, Text = "AlignRight" };

            var button1 = new Button { Content = image1, Height = 22, ToolTip= textBlockTT1 };
            button1.Click += OnClickCut;
            var button2 = new Button { Content = image2, Height = 22, ToolTip = textBlockTT2 };
            button2.Click += OnClickCopy;
            var button3 = new Button { Content = image3, Height = 22, ToolTip = textBlockTT3 };
            button3.Click += OnClickPaste;
            var button4 = new Button { Content = image4, Height = 22, ToolTip = textBlockTT4 };
            button4.Click += OnClickAlignLeft;
            var button5 = new Button { Content = image5, Height = 22, ToolTip = textBlockTT5 };
            button5.Click += OnClickAlignCenter;
            var button6 = new Button { Content = image6, Height = 22, ToolTip = textBlockTT6 };
            button6.Click += OnClickAlignRight;

            var toolBar1 = new ToolBar();
            toolBar1.Items.Add(button1);
            toolBar1.Items.Add(button2);
            toolBar1.Items.Add(button3);

            var toolBar2 = new ToolBar();
            toolBar2.Items.Add(button4);
            toolBar2.Items.Add(button5);
            toolBar2.Items.Add(button6);

            var toolBarTray = new ToolBarTray();
            toolBarTray.ToolBars.Add(toolBar1);
            toolBarTray.ToolBars.Add(toolBar2);

            currentLine = new TextBlock { Text = "Ln_1", Width = 40.0 };
            currentColumn = new TextBlock { Text = "Col_1", Width = 40.0 };

            var statusBarItem1 = new StatusBarItem { Content = currentLine };
            var statusBarItem2 = new StatusBarItem { Content = currentColumn };

            var statusBar = new StatusBar();
            statusBar.Items.Add(statusBarItem1);
            statusBar.Items.Add(separator2);
            statusBar.Items.Add(statusBarItem2);

            textBox = new TextBox
            {
                AcceptsReturn = true,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto
            };

            textBox.SelectionChanged += OnSelectionChanged;


            var dockPanel = new DockPanel();
            dockPanel.Children.Add(menu);
            dockPanel.Children.Add(toolBarTray);
            dockPanel.Children.Add(statusBar);
            dockPanel.Children.Add(textBox);

            DockPanel.SetDock(menu, Dock.Top);
            DockPanel.SetDock(toolBarTray, Dock.Top);
            DockPanel.SetDock(statusBar, Dock.Bottom);
            Content = dockPanel;
        }

        private void OnClickAlignLeft(object sender, RoutedEventArgs evn)
        {
            AlignLeft();
        }

        private void OnClickAlignCenter(object sender, RoutedEventArgs evn)
        {
            AlignCenter();
        }

        private void OnClickAlignRight(object sender, RoutedEventArgs evn)
        {
            AlignRight();
        }

        private void OnClickCopy(object sender, RoutedEventArgs evn)
        {
            Copy();
        }

        private void OnClickCut(object sender, RoutedEventArgs evn)
        {
            Cut();
        }

        private void OnClickExit(object sender, RoutedEventArgs evn)
        {
            Exit();
        }

        private void OnClickNew(object sender, RoutedEventArgs evn)
        {
            New();
        }

        private void OnClickOpen(object sender, RoutedEventArgs evn)
        {
            Open();
        }

        private void OnClickPaste(object sender, RoutedEventArgs evn)
        {
            Paste();
        }

        private void OnSelectionChanged(object sender, RoutedEventArgs evn)
        {
            UpdateCaretPosition();
        }

        private void Align(TextAlignment textAlignment)
        {
            textBox.TextAlignment = textAlignment;
        }

        private void AlignCenter()
        {
            Align(TextAlignment.Center);
        }

        private void AlignLeft()
        {
            Align(TextAlignment.Left);
        }

        private void AlignRight()
        {
            Align(TextAlignment.Right);
        }

        private void Copy()
        {
            Clipboard.SetText(textBox.SelectedText);
        }

        private void Cut()
        {
            Clipboard.SetText(textBox.SelectedText);
            textBox.SelectedText = string.Empty;
        }

        private void Exit()
        {
            Close();
        }

        private void New()
        {
            textBox.SelectedText = string.Empty;
        }

        private void Open()
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() ?? false)
            {
                using (var reader = new StreamReader(openFileDialog.FileName))
                {
                    textBox.Text = reader.ReadToEnd();
                }
            }
        }

        private void Paste()
        {
            if (Clipboard.ContainsText())
            {
                textBox.SelectedText = Clipboard.GetText();
            }
        }

        private void UpdateCaretPosition()
        {
            int row = textBox.GetLineIndexFromCharacterIndex(textBox.CaretIndex);
            int column = textBox.CaretIndex - textBox.GetLineIndexFromCharacterIndex(row);

            currentColumn.Text = $"Col{column + 1}";
            currentLine.Text = $"Ln {row + 1}";
        }

    }
}
