using NeuChessHu.ViewModels.Board.MatchBoard;
using NeuChessHu.ViewModels.Board.MenuBoard;
using NeuChessHu.ViewModels.MainWindow;
using NeuChessHu.Views.Board;
using System.Reflection;
using System.Windows;

namespace NeuChessHu.Templates;

internal static class UITemplates
{
    internal static void MergeTemplates()
    {
        foreach (Type viewModelType in Assembly.GetExecutingAssembly()
                                               .GetTypes()
                                               .Where(x => x.IsClass &&
                                                           !x.IsAbstract &&
                                                           x.Namespace is not null &&
                                                           x.Namespace.StartsWith("NeuChessHu.ViewModels") &&
                                                           x.Name.EndsWith("ViewModel"))
                                               .ToList())
        {
            if (viewModelType == typeof(MainWindowViewModel))
                continue;

            else if (viewModelType == typeof(MenuBoardViewModel))
                Application.Current.Resources.Add(new DataTemplateKey(typeof(MenuBoardViewModel)),
                    new DataTemplate(typeof(MenuBoardViewModel))
                    {
                        DataType = typeof(MenuBoardViewModel),
                        VisualTree = new FrameworkElementFactory(typeof(BoardView))
                    });

            else if (viewModelType == typeof(MatchBoardViewModel))
                Application.Current.Resources.Add(new DataTemplateKey(typeof(MatchBoardViewModel)),
                    new DataTemplate(typeof(MatchBoardViewModel))
                    {
                        DataType = typeof(MatchBoardViewModel),
                        VisualTree = new FrameworkElementFactory(typeof(BoardView))
                    });

            else
            {
                string viewNamespace = viewModelType.Namespace!.Replace("ViewModels", "Views");

                string viewName = viewModelType.Name.Replace("ViewModel", "View");

                string fullViewName = $"{viewNamespace}.{viewName}";

                Type? viewType = viewModelType.Assembly.GetType(fullViewName)
                    ?? throw new Exception($"View not found for {viewModelType.FullName}: expected {fullViewName}");

                Application.Current.Resources.Add(new DataTemplateKey(viewModelType),
                    new DataTemplate(viewModelType)
                    {
                        DataType = viewModelType,
                        VisualTree = new FrameworkElementFactory(viewType)
                    });
            }
        }
    }
}