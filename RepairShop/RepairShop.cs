using System.Collections.Generic;
using RepairShop.Menu;
using RepairShop.Model;
using RepairShop.Util;
using Spectre.Console;

namespace RepairShop
{
    /**
     * Main Class: What this class represents the all of the choices in the menu. when running the code you are given a listr of multiple different options 
     * "Add Vehicle", "Add Another Vehicle", "Remove Vehicle", "Remove Vehicles", "View Vehicle", "View Vehicles", "Create Appointment", "Create Another Appointment",
     * "View Appointment", "View Appointments", "Cancel Appointment", "Cancel Appointments", "View Invoice", "Logout", "Exit". Collects data in it's memory.
     */
    internal class RepairShop
    {
        private const string Version = "v.1.0.1";
        private const string Release = "";

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
                    // customer = CustomerMenu.Prompt();
                    customer = new Customer();
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
                                "Are you sure you want to [red]delete[/] this [yellow]vehicle[/]? [grey](If there is an appointment linked to the vehicle, it will also be canceled)[/]",
                                false))
                        {
                            automobiles.RemoveAt(index);
                        }
                    }

                    goto main_menu;
                case "View Vehicle":
                    AutomobileMenu.DisplayList(automobiles);
                    MessagesUtil.ContinuePrompt();
                    goto main_menu;
                case "Create Appointment":
                    appointments.Add(AppointmentMenu.Prompt(automobiles));
                    goto main_menu;
                case "View Appointment":
                    AppointmentMenu.DisplayDate(appointments[0].Date);
                    MessagesUtil.ContinuePrompt();
                    goto main_menu;
                case "Cancel Appointment":
                    AppointmentMenu.DisplayDate(appointments[0].Date);
                    AnsiConsole.WriteLine("\n\n");
                    if (AnsiConsole.Confirm("Are you sure you want to [red]cancel[/] your [yellow]appointment[/]?", false))
                    {
                        appointments.Remove(appointments[0]);
                    }
                    goto main_menu;
                case "View Invoice":
                    InvoiceMenu.Print(appointments[0].Services);
                    MessagesUtil.ContinuePrompt();
                    goto main_menu;
                case "Logout":
                    CustomerMenu.Show(customer);
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
                        MainMenu.WarningMessage("You have been logged out!");
                        MessagesUtil.ContinuePrompt();
                    }

                    goto main_menu;
                case "Exit":
                    MainMenu.WarningMessage(appointments.Count > 0 ? "Thank you! See you soon!" : "Have a good rest of your day!");
                    MessagesUtil.ContinuePrompt();
                    AnsiConsole.Clear();
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
    }
}