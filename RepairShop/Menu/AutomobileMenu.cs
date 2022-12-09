using System;
using System.Collections.Generic;
using RepairShop.Model;
using RepairShop.Model.Vehicle;
using RepairShop.Util;
using Spectre.Console;

namespace RepairShop.Menu
{
    public static class AutomobileMenu
    {
        public static Automobile Prompt()
        {
            // Makes
            var spMakes = new SelectionPrompt<string>()
                .HighlightStyle(Style.Parse("gold1"))
                .Title("Tell us about your [yellow1]vehicle[/], what is your car's [royalblue1]make[/]?")
                .PageSize(6)
                .MoreChoicesText("[grey](Move up and down to reveal more makes)[/]");
            foreach (var v in typeof(Make).GetEnumNames())
            {
                spMakes.AddChoice(v);
            }

            var make = AnsiConsole.Prompt(spMakes);
            Enum.TryParse(make, out Make parsedMake);

            // Transmissions
            var spTransmissions = new SelectionPrompt<string>()
                .HighlightStyle(Style.Parse("gold1"))
                .Title("Tell us about your [yellow1]vehicle[/], what is your car's [royalblue1]transmission[/]?")
                .PageSize(6)
                .MoreChoicesText("[grey](Move up and down to reveal more options)[/]");
            foreach (var x in typeof(Transmission).GetEnumNames())
            {
                spTransmissions.AddChoice(StringUtil.FixFromNamingConvention(x));
            }

            var transmission = AnsiConsole.Prompt(spTransmissions);
            Enum.TryParse(transmission, out Transmission parsedTransmission);

            var spDriveTypes = new SelectionPrompt<string>()
                .HighlightStyle(Style.Parse("gold1"))
                .Title("Tell us about your [yellow1]vehicle[/], what is your car's [royalblue1]drive type[/]?")
                .PageSize(6)
                .MoreChoicesText("[grey](Move up and down to reveal more options)[/]");
            foreach (var x in typeof(DriveType).GetEnumNames())
            {
                spDriveTypes.AddChoice(StringUtil.FixFromNamingConvention(x));
            }

            // Drive types
            var driveType = AnsiConsole.Prompt(spDriveTypes);
            Enum.TryParse(driveType, out DriveType parsedDriveType);

            var spYears = new SelectionPrompt<int>()
                .HighlightStyle(Style.Parse("gold1"))
                .Title("Tell us about your [yellow1]vehicle[/], what is your car's [royalblue1]year[/]?")
                .PageSize(6)
                .MoreChoicesText("[grey](Move up and down to reveal more years)[/]");
            for (var x = DateTime.Now.Year + 1; x >= 1950; x--)
            {
                spYears.AddChoice(x);
            }

            var year = AnsiConsole.Prompt(spYears);

            // Millage
            var millage = AnsiConsole.Ask<int>("Tell us about your [yellow1]vehicle[/], what is your car's [royalblue1]millage[/]?");

            // Finished Object
            return new Automobile(parsedMake, parsedTransmission, parsedDriveType, year, millage);
        }

        public static Automobile RemovePrompt(List<Automobile> automobiles)
        {
            var spAutomobiles = new SelectionPrompt<string>()
                .HighlightStyle(Style.Parse("gold1"))
                .Title("Which [yellow1]vehicle[/] would you like to [red]remove[/]?")
                .PageSize(4)
                .MoreChoicesText("[grey](Move up and down to reveal more vehicles)[/]");
            foreach (var v in automobiles)
            {
                spAutomobiles.AddChoice(
                    $"{v.Make} {v.Transmission} {v.DriveType} {v.Year} {v.Millage}");
            }

            return ParseToAutomobile(AnsiConsole.Prompt(spAutomobiles));
        }

        public static void DisplayList(List<Automobile> automobiles)
        {
            var table = new Table();

            table.Title(new TableTitle("[orange1]YOUR VEHICLES[/]", Style.Parse("bold")));

            table.AddColumn(new TableColumn("[lime]Make[/]").Centered());
            table.AddColumn(new TableColumn("[yellow]Transmission[/]").Centered());
            table.AddColumn(new TableColumn("[gold1]DriveType[/]").Centered());
            table.AddColumn(new TableColumn("[purple_2]Year[/]").Centered());
            table.AddColumn(new TableColumn("[red]Millage[/]").RightAligned());

            automobiles.Sort((v1, v2) => v1.Millage.CompareTo(v2.Millage));

            foreach (var automobile in automobiles)
            {
                table.AddRow(new Markup($"{automobile.Make}"), new Markup($"{automobile.Transmission}"),
                    new Markup($"{automobile.DriveType}"), new Markup($"{automobile.Year}"),
                    new Markup($"{automobile.Millage}"));
            }

            AnsiConsole.Write(table.Centered());
        }

        public static int FindIndexFromVehicleList(Automobile automobile, List<Automobile> automobiles)
        {
            var index = 0;
            for (var i = 0; i < automobiles.Count; i++)
            {
                if (automobile.Equals(automobiles[i]))
                {
                    index = i;
                }
            }

            return index;
        }

        // TODO: Fix whole method, sometimes not working properly
        public static Automobile ParseToAutomobile(string automobile)
        {
            var parsedAutomobile = automobile.Split(' ');
            if (parsedAutomobile.Length == 5)
            {
                // TODO: Handle exceptions if fail to parse
                Enum.TryParse(parsedAutomobile[0], out Make make);
                Enum.TryParse(parsedAutomobile[1], out Transmission transmission);
                Enum.TryParse(parsedAutomobile[2], out DriveType driveType);
                return new Automobile(make, transmission, driveType, Int32.Parse(parsedAutomobile[4]),
                    Int32.Parse(parsedAutomobile[4]));
            }

            AnsiConsole.Clear();
            return null;
        }
    }
}