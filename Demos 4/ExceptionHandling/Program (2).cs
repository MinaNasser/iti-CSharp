

using Serilog;
using Serilog.Core;

public struct  Program
{
    public static int Main()
    {

        Console.Clear();
        App.ShowHomeMenu();
        UserOption Option  = App.GetUserOption();
        App.ProcessUserOption(Option);

        return 0;
    }
}