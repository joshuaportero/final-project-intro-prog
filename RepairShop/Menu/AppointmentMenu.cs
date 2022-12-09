using System;
using System.Collections.Generic;
using System.Globalization;
using RepairShop.Model;
using Spectre.Console;

namespace RepairShop.Menu
{
    public class AppointmentMenu
    {
        public static Appointment Prompt(List<Automobile> automobiles)
        {
            // Vehicles
            var spVehicles = new SelectionPrompt<string>()
                .Title("In which vehicle would you like to perform the service?")
                .PageSize(4)
                .MoreChoicesText("[grey](Move up and down to reveal more vehicles)[/]");
            foreach (var v in automobiles)
            {
                spVehicles.AddChoice(
                    $"{v.Make} {v.Transmission} {v.DriveType} {v.Year} {v.Millage}");
            }

            var vehicle = AutomobileMenu.ParseToAutomobile(AnsiConsole.Prompt(spVehicles));

            // Services
            // TODO: Add more services and parse services string to name and cost.
            var services = AnsiConsole.Prompt(
                new MultiSelectionPrompt<string>()
                    .Title($"What does your [gold1]{vehicle.Make}[/] need?")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more services)[/]")
                    .InstructionsText(
                        "[grey](Press [blue]<space>[/] to toggle a option, " +
                        "[green]<enter>[/] to accept)[/]")
                    .AddChoices(
                        "3750 Mile Interval Service - $44.95",
                        "7500 Mile Interval Service - $74.95",
                        "3750X Mile Interval Service - $44.95",
                        "7500X Mile Interval Service - $74.95",
                        "Tire Rotate and Balance - $44.95",
                        "Disinfect HVAC System - $149.95",
                        "Rotate Tires - $24.95",
                        "Diagnose Engine Light - $88.95",
                        "Diagnose ABS Light - $88.95",
                        "Diagnose Break System - $88.95",
                        "Diagnose Electrical System - $74.95",
                        "Diagnose Engine - $74.95",
                        "Diagnose Engine Performance - $74.95",
                        "Diagnose Heating or AC - $74.95"
                    )
            );

            // TODO: Charge something if proprietary option
            // Transportation
            var transportation = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("While we are servicing your vehicle, Do you need a ride?")
                .PageSize(4)
                .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                .AddChoices(
                    "I Have a Ride",
                    "I'll Wait",
                    "I'll Take a Shuttle",
                    "RideShare(Uber/LYFT)",
                    "Rental"
                )
            );

            // Calendar
            var randomDateTime = RandomDateTime();
            // DisplayDate(randomDateTime);
            AnsiConsole.WriteLine("\n");

            // TODO: Generate and display another date if the user doesn't want to continue
            AnsiConsole.Confirm("Continue?", false);

            return new AppointmentBuilder()
                .WithVehicle(vehicle)
                .WithServices(services.ToArray())
                .WithTransportation(transportation)
                .OnDate(randomDateTime)
                .Build();
        }

        /**
        *                       TODO:
        * Generate a date and time with the following constraints
        * day > today
        * month >= today
        * year >= today
        * hour between 8:00 am to 6:00 pm
        * minute any
        */
        private static DateTime RandomDateTime()
        {

            var now = DateTime.Now;

            var r = new Random();

            var newYear = r.Next(now.Year, now.Year + 1);
            var newMonth = newYear == now.Year ? r.Next(now.Month, 12) : r.Next(1, 12);

            var newDay = newMonth == now.Month && newYear == now.Year ? r.Next(now.Day, DateTime.DaysInMonth(newYear , newMonth)) : r.Next(newYear, newMonth);

            var newHour = r.Next(8, 18);
            var newMinute = r.Next(0, 59);
            
            return new DateTime(newYear,newMonth,newDay,newHour,newMinute,0);
        }


        /**
         * <summary>This method provides the date and time of the appointment.
         * This will be presented once scheduling the appointment.
         * You are able to select a date and time after present time
         * (ex. 10th of December (saturday) of 2022 at 10:00am) </summary>
         * 
         * <param name="dateTime">Any date</param>
         * <returns>Formatted DateTime string</returns>
         */ 
        public static string DateToString(DateTime dateTime)
        {
            var day = dateTime.Day;
            string dayOrdinal;

            switch (day)
            {
                case 1:
                    dayOrdinal = "st";
                    break;
                case 2:
                    dayOrdinal = "nd";
                    break;
                case 3:
                    dayOrdinal = "rd";
                    break;
                default:
                    dayOrdinal = "th";
                    break;
            }

            return $"{dateTime.Day}{dayOrdinal} of {dateTime.Month}({dateTime.DayOfWeek}) of {dateTime.Year} at {FormatDate(dateTime, "hh")}:{FormatDate(dateTime, "mm")}{FormatDate(dateTime, "tt")}";
        }

        private static string FormatDate(DateTime dateTime, string format)
        {
            return dateTime.ToString(format, new CultureInfo("en-US"));
        }
    }
}