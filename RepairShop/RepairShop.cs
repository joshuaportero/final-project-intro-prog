using System.Collections.Generic;
using RepairShop.Menu;
using RepairShop.Model;
using Spectre.Console;

namespace RepairShop
{
    internal class RepairShop
    {
        private const string Version = "v.0.7.4";
        private const string Release = "ALPHA";

        private static void Main()
        {
            // Main class Instantiation
            var repairShop = new RepairShop();

            Customer customer = null;
            var automobiles = new List<Automobile>();
            var appointments = new List<Appointment>();

            // Go-To statement to improve readability
            main_menu:

            repairShop.Title();

            switch (MainMenu.ProcessesMenu(customer, automobiles, appointments))
            {
                case "Add Customer":
                    // TODO: Remove comment when done with testing
                    customer = CustomerMenu.Prompt();
                    // customer = new Customer();
                    goto main_menu;
                case "Add Vehicle":
                    automobiles.Add(AutomobileMenu.Prompt());
                    goto main_menu;
                case "Remove Vehicle":

                    if (automobiles.Count > 0)
                    {
                        var vehicle = AutomobileMenu.RemovePrompt(automobiles);
                        var index = AutomobileMenu.FindIndexFromVehicleList(vehicle, automobiles);

                        // Find appointment linked to vehicle and remove it
                        AutomobileMenu.DisplayList(new List<Automobile> { vehicle });
                        AnsiConsole.WriteLine("\n\n");
                        if (AnsiConsole.Confirm(
                                "Are you sure you want to [red]delete[/] this [yellow]vehicle[/]? [grey](The appointment will also be canceled)[/]",
                                false))
                        {
                            automobiles.RemoveAt(index);
                        }
                    }

                    goto main_menu;
                case "Vehicle List":
                    goto main_menu;
                case "Create Appointment":
                    goto main_menu;
                case "View Appointment's Date":
                    goto main_menu;
                case "Cancel Appointment":
                    goto main_menu;
                case "View Invoice":
                    goto main_menu;
                case "Logout":
                    if (AnsiConsole.Confirm(
                            "Are you sure you want to [red]logout[/]? [grey53](Your details/vehicles/appointmets" +
                            " will be removed)[/]",
                            false))
                    {
                        // Clear everything
                        customer = null;
                        automobiles.Clear();
                        appointments.Clear();
                        // Output a message
                        repairShop.Title();
                        MainMenu.LogoutMessage();
                        repairShop.ContinuePrompt();
                    }

                    goto main_menu;
                case "Exit":
                    return;
                default:
                    goto main_menu;
            }
        }

        /**
         * <summary>Display the application's name and version</summary>
         */
        private void Title()
        {
            AnsiConsole.Clear();
            MainMenu.TitleFiglet(Version, Release);
        }

        /**
         * <summary>Confirmation prompt -
         * waiting for the user to press enter.</summary>
         */
        private void ContinuePrompt()
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