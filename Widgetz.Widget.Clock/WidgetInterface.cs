using System.Windows;
using System.Windows.Controls;
using Widgetz.Interface;

namespace Widgetz.Widget.Clock {
    public class WidgetInterface : IWidget {
        public string WidgetName { get; init; } = "Clock";
        public UserControl WidgetControl { get; init; } = new Clock();

        public Window SettingWindow { get; init; } = new SettingWindow();
    }
}
