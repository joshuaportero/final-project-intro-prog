using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Spectre.Console;

namespace RepairShop.Menu
{
    public class InvoiceMenu
    {
        public static void Print(IEnumerable<string> services)
    {
        var enumerable = services as string[] ?? services.ToArray();
        
        var servicesCost = (from service in enumerable
            select service.Split('-')
            into splitService
            select splitService[1]
            into serviceCost
            select double.Parse(serviceCost, NumberStyles.AllowCurrencySymbol | NumberStyles.Currency)).Sum();

        var invoiceTable = new Table();

        invoiceTable.AddColumn(new TableColumn("Service").Centered());
        invoiceTable.AddColumn(new TableColumn("Cost").Centered());
        invoiceTable.AddColumn(new TableColumn("Total").Centered());

        var serviceCount = 0;
        
        foreach (var service in enumerable)
        {
            var splitService = service.Split('-');
            invoiceTable.AddRow(new Markup(splitService[0]), new Markup($"[red]{splitService[1]}[/]"));
            serviceCount++;
        }

        invoiceTable.AddRow(new Markup(""), new Markup(""), new Markup(""));
        invoiceTable.AddRow(new Markup(""), new Markup(""), new Markup(""));

        // TODO: The service includes de work 
        var taxGrossTotal = CalculatePercentage(servicesCost, 6.625);
        var work = serviceCount * 59.99;
        var taxWork = CalculatePercentage(work, 6.625);
        var netTotal = servicesCost + taxWork + work + taxGrossTotal;
        
        // Gross total and tax
        invoiceTable.AddRow(new Markup(""), new Markup("GROSS Total")
            , new Markup($"[yellow]{DoubleToCurrency(servicesCost)}[/]"));
        invoiceTable.AddRow(new Markup(""), new Markup("TAX ([red]6.625[/]%)")
            , new Markup($"[yellow]{DoubleToCurrency(taxGrossTotal)}[/]"));
        invoiceTable.AddRow(new Markup(""), new Markup(""), new Markup(""));
        
        // Work total and tax
        invoiceTable.AddRow(new Markup(""), new Markup("WORK Total")
            , new Markup($"[yellow]{DoubleToCurrency(work)}[/]"));
        invoiceTable.AddRow(new Markup(""), new Markup("TAX ([red]6.625[/]%)")
            , new Markup($"[yellow]{DoubleToCurrency(taxWork)}[/]"));
        invoiceTable.AddRow(new Markup(""), new Markup(""), new Markup(""));
        
        // Net Total
        invoiceTable.AddRow(new Markup(""), new Markup("NET Total"),
            new Markup($"[lime]{DoubleToCurrency(netTotal)}[/]"));

        AnsiConsole.Write(invoiceTable.Centered());

    }

    /**
     * <summary>Print Double as USD Currency</summary>
     * <param name="value">Any double</param>
     * <returns>Formatted string as currency of value.</returns>
     */
    private static string DoubleToCurrency(double value)
    {
        return value.ToString("C", new CultureInfo("en-US"));
    }

    private static double CalculatePercentage(double value, double percentage)
    {
        return value * percentage / 100;
    }
    }
}