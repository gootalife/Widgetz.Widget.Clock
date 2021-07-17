using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Widgetz.Widget.Clock {
    /// <summary>
    /// Clock.xaml の相互作用ロジック
    /// </summary>
    public partial class Clock : UserControl {

        private readonly string[,] fonts = new string[11, 6] {
            {
                "┌──┐",
                "│┌┐│",
                "││││",
                "││││",
                "│└┘│",
                "└──┘",
            },{
                "　　┌┐",
                "　　││",
                "　　││",
                "　　││",
                "　　││",
                "　　└┘",
            },{
                "┌──┐",
                "└─┐│",
                "┌─┘│",
                "│┌─┘",
                "│└─┐",
                "└──┘",
            },{
                "┌──┐",
                "└─┐│",
                "┌─┘│",
                "└─┐│",
                "┌─┘│",
                "└──┘",
            },{
                "┌┐┌┐",
                "││││",
                "│└┘│",
                "└─┐│",
                "　　││",
                "　　└┘",
            },{
                "┌──┐",
                "│┌─┘",
                "│└─┐",
                "└─┐│",
                "┌─┘│",
                "└──┘",
            },{
                "┌──┐",
                "│┌─┘",
                "│└─┐",
                "│┌┐│",
                "│└┘│",
                "└──┘",
            },{
                "┌──┐",
                "│┌┐│",
                "└┘││",
                "　　││",
                "　　││",
                "　　└┘",
            },{
                "┌──┐",
                "│┌┐│",
                "│└┘│",
                "│┌┐│",
                "│└┘│",
                "└──┘",
            },{
                "┌──┐",
                "│┌┐│",
                "│└┘│",
                "└─┐│",
                "┌─┘│",
                "└──┘",
            },{
                "　　",
                "┌┐",
                "└┘",
                "┌┐",
                "└┘",
                "　　",
            }
        };

        private readonly string[,] fontsBold = new string[11, 6] {
            {
                "┏━━┓",
                "┃┏┓┃",
                "┃┃┃┃",
                "┃┃┃┃",
                "┃┗┛┃",
                "┗━━┛",
            },{
                "　　┏┓",
                "　　┃┃",
                "　　┃┃",
                "　　┃┃",
                "　　┃┃",
                "　　┗┛",
            },{
                "┏━━┓",
                "┗━┓┃",
                "┏━┛┃",
                "┃┏━┛",
                "┃┗━┓",
                "┗━━┛",
            },{
                "┏━━┓",
                "┗━┓┃",
                "┏━┛┃",
                "┗━┓┃",
                "┏━┛┃",
                "┗━━┛",
            },{
                "┏┓┏┓",
                "┃┃┃┃",
                "┃┗┛┃",
                "┗━┓┃",
                "　　┃┃",
                "　　┗┛",
            },{
                "┏━━┓",
                "┃┏━┛",
                "┃┗━┓",
                "┗━┓┃",
                "┏━┛┃",
                "┗━━┛",
            },{
                "┏━━┓",
                "┃┏━┛",
                "┃┗━┓",
                "┃┏┓┃",
                "┃┗┛┃",
                "┗━━┛",
            },{
                "┏━━┓",
                "┃┏┓┃",
                "┗┛┃┃",
                "　　┃┃",
                "　　┃┃",
                "　　┗┛",
            },{
                "┏━━┓",
                "┃┏┓┃",
                "┃┗┛┃",
                "┃┏┓┃",
                "┃┗┛┃",
                "┗━━┛",
            },{
                "┏━━┓",
                "┃┏┓┃",
                "┃┗┛┃",
                "┗━┓┃",
                "┏━┛┃",
                "┗━━┛",
            },{
                "　　",
                "┏┓",
                "┗┛",
                "┏┓",
                "┗┛",
                "　　",
            }
        };

        private const int ClockStrWidth = 28;

        public Clock() {
            InitializeComponent();
        }

        private DispatcherTimer CreateTimer(double time) {
            var timer = new DispatcherTimer(DispatcherPriority.SystemIdle) {
                // タイマーイベントの発生間隔を設定
                Interval = TimeSpan.FromMilliseconds(time),
            };
            // タイマーイベントの定義
            timer.Tick += async (s, e) => {
                var strTime = DateTime.Now.ToString("HH:mm:ss", DateTimeFormatInfo.InvariantInfo);
                await SetTime(strTime);
            };
            // 生成したタイマーを返す
            return timer;
        }

        private async Task SetTime(string strTime) {
            await Task.Run(async () => {
                await Dispatcher.BeginInvoke(new Action(() => {
                    var elements = strTime.Split(":");
                    var strList = new List<int>();
                    // hh mm ss
                    for(var i = 0; i < 3; i++) {
                        for(var j = 0; j < 2; j++) {
                            if(i > 0 && j == 0) {
                                strList.Add(10);
                            }
                            strList.Add(int.Parse(elements[i][j].ToString(), DateTimeFormatInfo.InvariantInfo));
                        }
                    }
                    var clock = "┏".PadRight(ClockStrWidth + 1, '━') + "┓\n";
                    for(var height = 0; height < fonts.GetLength(1); height++) {
                        clock += "┃";
                        foreach(var num in strList) {
                            clock += fonts[num, height];
                        }
                        clock += "┃\n";
                    }
                    clock += "┗".PadRight(ClockStrWidth + 1, '━') + "┛";
                    TextClock.Text = clock;
                }), null);
            });
        }

        private void Clock_Loaded(object sender, RoutedEventArgs e) {
            // タイマーイベントの発生間隔を設定
            var timer = CreateTimer(100);
            timer.Start();
        }
    }
}
