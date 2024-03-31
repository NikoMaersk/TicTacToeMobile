using Microsoft.Maui.Controls.Shapes;

namespace TicTacToeMobile.Utility
{
    public static class StyleUtil
    {
        public static Style ButtonStyles { get; } = new Style(typeof(Button))
        {
            Setters =
            {
                new Setter { Property = VisualElement.BackgroundColorProperty, Value = Color.FromArgb("#333333") },
                new Setter { Property = Button.BorderWidthProperty, Value = 1 },
                new Setter { Property = Button.BorderColorProperty, Value = Color.FromArgb("#FFFFFF") },
                new Setter { Property = Button.CornerRadiusProperty, Value = 10 },
                new Setter { Property = View.MarginProperty, Value = 2 },
                new Setter { Property = Button.TextColorProperty, Value = Color.FromArgb("#FFFFFF") },
                new Setter { Property = Button.FontAttributesProperty, Value = FontAttributes.Bold },
                new Setter { Property = Button.FontSizeProperty, Value = 75 },
			}
        };
	}
}
