using NeuChessHu.CommandUtils;
using NeuChessHu.Resources;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;

namespace NeuChessHu.Views.Overlays.MatchOverlays.MatchWindows;

public partial class PromotionWindowView : UserControl
{
    public PromotionWindowView() =>
        Loaded += (s, e) =>
        {
            Content ??= PromotionWindowBuilder();
        };

    static Border PromotionWindowBuilder()
    {
        Border promotionContainer = new()
        {
            BorderThickness = new Thickness(1.8),
            BorderBrush = AppResources.Get<SolidColorBrush>("BorderBrush"),
            Height = 150,
            Width = 150,
            CornerRadius = new CornerRadius(5),
            Background = Brushes.Transparent,
            Child = new Grid()
        };

        Rectangle backgroundEffect = new()
        {
            Fill = Brushes.WhiteSmoke,
            Effect = new BlurEffect { Radius = 5 },
            Opacity = 0.9
        };

        Grid promotion = new()
        {
            Height = 140,
            Width = 140,
            Background = Brushes.Transparent,
        };

        for (int i = 0; i < 2; i++)
        {
            promotion.ColumnDefinitions.Add(new ColumnDefinition 
                { Width = new GridLength(1, GridUnitType.Star) });
            promotion.RowDefinitions.Add(new RowDefinition 
                { Height = new GridLength(1, GridUnitType.Star) });
        }

        for (int i = 0; i < 4; i++)
        {
            Image promotablePiece = new()
            {
                Height = 50,
                Width = 50,
            };

            CommandAttachers.OnLoaded(promotablePiece, "PieceImageSetterCommand", args: i);
            CommandAttachers.OnClickEvent(promotablePiece, "SelectCommand", args: i);

            Binding pieceImageBinding = new($"PieceImages[{i}]") { Mode = BindingMode.OneWay };
            promotablePiece.SetBinding(Image.SourceProperty, pieceImageBinding);

            Grid.SetRow(promotablePiece, i < 2 ? 0 : 1);
            Grid.SetColumn(promotablePiece, i % 2);

            promotion.Children.Add(promotablePiece);
        }

        Panel.SetZIndex(backgroundEffect, 0);
        Panel.SetZIndex(promotion, 1);

        foreach (UIElement element in new UIElement[] { promotion, backgroundEffect })
            (promotionContainer.Child as Grid)!.Children.Add(element);

        return promotionContainer;
    }
}