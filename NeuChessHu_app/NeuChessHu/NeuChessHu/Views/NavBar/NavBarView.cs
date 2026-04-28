using NeuChessHu.CommandUtils;
using NeuChessHu.Resources;
using SharpVectors.Converters;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace NeuChessHu.Views.NavBar;

public partial class NavBarView : UserControl
{
    public NavBarView() =>
        Loaded += (s, e) =>
        {
            Content ??= NavBarBuilder();
        };

    static Border NavBarBuilder()
    {
        Border navBarBorder = new()
        {
            BorderThickness = new Thickness(0, 0, 0, 1),
            Height = 60,
            VerticalAlignment = VerticalAlignment.Top,
            HorizontalAlignment = HorizontalAlignment.Stretch,
        };

        Grid navBar = new();

        for (int i = 0; i < 3; i++)
            navBar.ColumnDefinitions.Add(new ColumnDefinition()
            {
                Width = i is 1 ? new GridLength(1, GridUnitType.Star) : GridLength.Auto
            });

        SvgViewbox flag = new()
        {
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(20, 0, 0, 0),
            Height = 32,
            Cursor = AppResources.Get<Cursor>("CursorOnButtons"),
            ForceCursor = true
        };

        SvgViewbox logo = new()
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Width = 250,
            Margin = new Thickness(20, 3, 0, 0)
        };

        Image profilePicture = new()
        {
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Center,
            Width = 65,
            Clip = new EllipseGeometry(new Point(29.5, 29.5), 20.5, 20.5),
            Margin = new Thickness(0, 0, 8, 0),
            Cursor = AppResources.Get<Cursor>("CursorOnButtons"),
            ForceCursor = true
        };

        navBar.SetResourceReference(BackgroundProperty, "NavBarBrush");
        navBarBorder.SetResourceReference(Border.BorderBrushProperty, "BorderBrush");

        flag.SetResourceReference(SvgViewbox.SourceProperty, "SelectedLanguageFlagImage");
        logo.SetResourceReference(SvgViewbox.SourceProperty, "LogoImage");

        Binding flagVisibilityBinding = new("FlagVisibility") { Mode = BindingMode.OneWay };
        flag.SetBinding(VisibilityProperty, flagVisibilityBinding);

        Binding profilePictureVisibilityBinding = new("ProfilePictureVisibility") { Mode = BindingMode.OneWay };
        profilePicture.SetBinding(VisibilityProperty, profilePictureVisibilityBinding);

        Binding profilePictureBinding = new("ProfilePicture") { Mode = BindingMode.OneWay };
        profilePicture.SetBinding(Image.SourceProperty, profilePictureBinding);

        CommandAttachers.OnClickEvent(flag, "SwitchLanguageCommand");
        CommandAttachers.OnClickEvent(profilePicture, "ShowMenuPopUpCommand");

        Grid.SetColumn(flag, 0);
        Grid.SetColumn(logo, 1);
        Grid.SetColumn(profilePicture, 2);

        foreach (UIElement child in new UIElement[] { flag, logo, profilePicture })
            navBar.Children.Add(child);

        navBarBorder.Child = navBar;

        return navBarBorder;
    }
}