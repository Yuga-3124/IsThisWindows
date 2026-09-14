using Microsoft.Win32;
using System.Windows;
using System.Windows.Media.Animation;

namespace IsThisWindows;

public partial class MainWindow : Window
{
    private string _productName = "Windows";
    private string _version = "Unknown";

    public MainWindow()
    {
        InitializeComponent();

        Loaded += MainWindow_Loaded;
    }


    private async void MainWindow_Loaded(
        object sender,
        RoutedEventArgs e)
    {
        // =========================================
        // 情報取得
        // =========================================

        GetWindowsInformation();


        // =========================================
        // Phase 0
        // 静かにフェードイン
        // =========================================

        if (FindResource("WindowFade") is Storyboard fade)
        {
            fade.Begin(this);
        }

        await Task.Delay(1200);


        // =========================================
        // Phase 1
        // Windowsロゴ
        // =========================================

        if (FindResource("LogoAnimation") is Storyboard logo)
        {
            logo.Begin(this);
        }

        await Task.Delay(3000);


        // =========================================
        // Phase 2
        // Success
        // =========================================

        if (FindResource("CheckCircleAnimation")
            is Storyboard circle)
        {
            circle.Begin(this);
        }

        await Task.Delay(650);


        // =========================================
        // Phase 3
        // Check
        // =========================================

        if (FindResource("CheckMarkAnimation")
            is Storyboard check)
        {
            check.Begin(this);
        }

        await Task.Delay(1100);


        // =========================================
        // Phase 4
        // Text
        // =========================================

        if (FindResource("TextAnimation")
            is Storyboard text)
        {
            text.Begin(this);
        }

        await Task.Delay(600);


        // =========================================
        // Version
        // =========================================

        VersionText.Text =
            $"{_productName}\nVersion {_version}";

    }


    private void GetWindowsInformation()
    {
        try
        {
            using RegistryKey? key =
                Registry.LocalMachine.OpenSubKey(
                    @"SOFTWARE\Microsoft\Windows NT\CurrentVersion");

            if (key == null)
                return;


            string? productName =
                key.GetValue("ProductName")?.ToString();

            string? displayVersion =
                key.GetValue("DisplayVersion")?.ToString();


            if (!string.IsNullOrWhiteSpace(productName))
            {
                _productName = productName;
            }


            if (!string.IsNullOrWhiteSpace(displayVersion))
            {
                _version = displayVersion;
            }
        }
        catch
        {
            // 情報取得に失敗しても問題なし。
            // このソフトはWindows上で起動している。
        }
    }


    private void CloseButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        Close();
    }
}