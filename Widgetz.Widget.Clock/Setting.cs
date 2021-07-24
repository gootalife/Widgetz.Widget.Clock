namespace Widgetz.Widget.Clock {
    public record Setting {
        public Color BackgroundColor { get; set; } = new Color { R = 0, G = 0, B = 0, A = 0 };
        public Color ForegroundColor { get; set; } = new Color { R = 0, G = 0, B = 0, A = 0 };
    }

    public record Color {
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }
        public int A { get; set; }
    }
}