using System.Collections.Generic;
using RepairShop.Model;
using Spectre.Console;

namespace RepairShop.Menu
{
    /**
     * This class handles the view of the menu of the model
     */
    public static class MainMenu
    {
        public static void TitleFiglet(string version, string release)
        {
            AnsiConsole.Write(
                new FigletText("REPAIR SHOP")
                    .Centered()
                    .Color(Color.Gold1)
            );
            var ver = version.Split('.');
            AnsiConsole.Write(
                new Markup($"{ver[0]}.[springgreen2_1]{ver[1]}[/].[orange1]{ver[2]}[/].[red]{ver[3]}[/]" +
                           (release != string.Empty ? "-" + release.ToUpper() : ""))
                    .Centered()
            );
            AnsiConsole.WriteLine("\n\n");
        }

        public static string ProcessesMenu(Customer customer, List<Automobile> automobiles,
            List<Appointment> appointments)
        {
            var choices = new List<string>();

            choices.AddRange(customer != null
                ? new[]
                {
                    "Add Vehicle", "Remove Vehicle", "View Vehicle", "Create Appointment", "View Appointment",
                    "Cancel Appointment", "View Invoice", "Logout", "Exit"
                }
                : new[] { "Add Customer", "Exit" }
            );

            switch (automobiles.Count)
            {
                case 0:
                    choices.Remove("Create Appointment");
                    choices.Remove("Add Another Vehicle");
                    choices.Remove("Remove Vehicle");
                    choices.Remove("Remove Vehicles");
                    choices.Remove("View Vehicle");
                    choices.Remove("View Vehicles");
                    break;
                case 1:
                    choices.Remove("Add Vehicle");
                    choices.Remove("Remove Vehicles");
                    choices.Remove("View Vehicles");
                    break;
                default:
                    choices.Remove("Add Vehicle");
                    choices.Remove("Remove Vehicle");
                    choices.Remove("View Vehicle");
                    break;
            }

            switch (appointments.Count)
            {
                case 0:
                    choices.Remove("Create Another Appointment");
                    choices.Remove("View Appointment");
                    choices.Remove("View Appointments");
                    choices.Remove("Cancel Appointment");
                    choices.Remove("Cancel Appointments");
                    choices.Remove("View Invoice");
                    break;
                case 1:
                    choices.Remove("Create Appointment");
                    choices.Remove("View Appointments");
                    choices.Remove("Cancel Appointments");
                    break;
                default:
                    choices.Remove("Create Appointment");
                    choices.Remove("View Appointment");
                    choices.Remove("Cancel Appointment");
                    break;
            }

            var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .HighlightStyle(Style.Parse("springgreen2_1"))
                    .Title("What would you like to do?")
                    .PageSize(6)
                    .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                    .AddChoices(choices)
            );
            return option;
        }

        public static void WarningMessage(string msg)
        {
            AnsiConsole.WriteLine("\n");
            var table = new Table
            {
                Border = TableBorder.Rounded,
                BorderStyle = Style.Parse("red"),
            };
            table.AddColumn("[red]" + msg + "[/]");

            AnsiConsole.Write(table.Centered());
        }
    }
}