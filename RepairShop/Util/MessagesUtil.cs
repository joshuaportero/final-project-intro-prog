using Spectre.Console;

namespace RepairShop.Util
{
    public static class MessagesUtil
    {
        /**
         * <summary>Confirmation prompt -
         * waiting for the user to press enter.</summary>
         */
        public static void ContinuePrompt()
        {
            AnsiConsole.WriteLine("\n");
            AnsiConsole.Write(new Markup("[grey37]Press [[[grey53]ENTER[/]]] to Continue![/]").Centered());
            var textPrompt = new TextPrompt<string>("")
            {
                AllowEmpty = true
            };
            textPrompt.Show(AnsiConsole.Console);
        }
    }
}