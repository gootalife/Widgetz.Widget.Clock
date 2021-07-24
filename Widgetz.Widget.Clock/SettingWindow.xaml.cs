using Newtonsoft.Json;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Widgetz.Widget.Clock {
    /// <summary>
    /// SettingWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class SettingWindow : Window {
        private Setting setting;
        private readonly string settingPath = $@"{Directory.GetCurrentDirectory()}\Widgets\ClockSetting.json";

        public SettingWindow() {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            // 設定の読み込み
            if(File.Exists(settingPath)) {
                var json = File.ReadAllText(settingPath);
                setting = JsonConvert.DeserializeObject<Setting>(json);
            }
            if(setting is null) {
                setting = new Setting();
            }
            SetSettingInformation();
            UpdateColorSample();
        }

        private void UpdateColorSample() {
            BGColor.Fill = new SolidColorBrush(System.Windows.Media.Color.FromArgb((byte)setting.BackgroundColor.A,
                                                                                   (byte)setting.BackgroundColor.R,
                                                                                   (byte)setting.BackgroundColor.G,
                                                                                   (byte)setting.BackgroundColor.B));
            FGColor.Fill = new SolidColorBrush(System.Windows.Media.Color.FromArgb((byte)setting.ForegroundColor.A,
                                                                                   (byte)setting.ForegroundColor.R,
                                                                                   (byte)setting.ForegroundColor.G,
                                                                                   (byte)setting.ForegroundColor.B));
        }

        private async void ApplyButton_Click(object sender, RoutedEventArgs e) {
            // 設定の保存
            var json = JsonConvert.SerializeObject(setting, Formatting.Indented);
            await File.WriteAllTextAsync(settingPath, json);
            _ = MessageBox.Show("設定を保存しました。", "設定保存");
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e) {
            Close();
        }

        private void SetSettingInformation() {
            // 背景色
            BGColorR.Text = setting.BackgroundColor.R.ToString(CultureInfo.InvariantCulture);
            BGColorG.Text = setting.BackgroundColor.G.ToString(CultureInfo.InvariantCulture);
            BGColorB.Text = setting.BackgroundColor.B.ToString(CultureInfo.InvariantCulture);
            BGColorA.Text = setting.BackgroundColor.A.ToString(CultureInfo.InvariantCulture);
            // 前景色
            FGColorR.Text = setting.ForegroundColor.R.ToString(CultureInfo.InvariantCulture);
            FGColorG.Text = setting.ForegroundColor.G.ToString(CultureInfo.InvariantCulture);
            FGColorB.Text = setting.ForegroundColor.B.ToString(CultureInfo.InvariantCulture);
            FGColorA.Text = setting.ForegroundColor.A.ToString(CultureInfo.InvariantCulture);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            e.Cancel = true;
            Visibility = Visibility.Hidden;
        }

        private void Color_LostFocus(object sender, RoutedEventArgs e) {
            var textBox = (TextBox)sender;
            // 不正値
            if(!int.TryParse(textBox.Text, out var input) || input > 255 || input < 0) {
                _ = MessageBox.Show("0～255で入力してください", "入力値不正", MessageBoxButton.OK, MessageBoxImage.Error);
                textBox.Text = "0";
            }
            // 背景色
            setting.BackgroundColor.R = int.Parse(BGColorR.Text, CultureInfo.InvariantCulture);
            setting.BackgroundColor.G = int.Parse(BGColorG.Text, CultureInfo.InvariantCulture);
            setting.BackgroundColor.B = int.Parse(BGColorB.Text, CultureInfo.InvariantCulture);
            setting.BackgroundColor.A = int.Parse(BGColorA.Text, CultureInfo.InvariantCulture);
            // 前景色
            setting.ForegroundColor.R = int.Parse(FGColorR.Text, CultureInfo.InvariantCulture);
            setting.ForegroundColor.G = int.Parse(FGColorG.Text, CultureInfo.InvariantCulture);
            setting.ForegroundColor.B = int.Parse(FGColorB.Text, CultureInfo.InvariantCulture);
            setting.ForegroundColor.A = int.Parse(FGColorA.Text, CultureInfo.InvariantCulture);
            UpdateColorSample();
        }
    }
}
