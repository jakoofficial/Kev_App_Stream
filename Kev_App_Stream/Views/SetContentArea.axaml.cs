using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Kev_App_Stream.ViewModels;

namespace Kev_App_Stream;

public partial class SetContentArea : UserControl
{
    static SetContentArea Instance;

    public SetContentArea()
    {
        InitializeComponent();

        if (Instance == null) Instance = this;
    }

    public static void Navigate(ViewModelBase content)
    {
        if (Instance == null) return;
        Instance.Content = null;
        Instance.Content = content;
    }
}