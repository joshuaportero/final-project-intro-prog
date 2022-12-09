using System.Text.RegularExpressions;
using RepairShop.Model;
using Spectre.Console;

namespace RepairShop.Menu
{
    public static class CustomerMenu
    {
    
        /**
         * <summary>Prompt the client the registration form</summary>
         * <returns>Customer Object following basic constraints</returns>
         */
        public static Customer Prompt()
        {
            var rule = new Rule("[red]REGISTER FORM[/]")
            {
                Alignment = Justify.Left
            };
            AnsiConsole.Write(rule);


            var firstName = AnsiConsole.Ask<string>("What is your [gold1]first[/] name?\n");
            var lastName = AnsiConsole.Ask<string>("What is your [gold1]last[/] name?\n");
            
            string email;
            while (true)
            {
                email = AnsiConsole.Ask<string>("What is your [cyan]email[/]?\n");
                if (ValidateEmail(email))
                {
                    break;
                }
                AnsiConsole.Write(new Markup("[red]That is not a valid email address, please try again![/]\n"));
            }

            string cellphone;
            while (true)
            {
                cellphone = AnsiConsole.Ask<string>("What is your [orange1]cellphone[/]?\n");
                if (cellphone.Trim().Length == 10)
                {
                    break;
                }
                AnsiConsole.Write(new Markup("[red]That is not a cellphone number, please try again![/] [grey37](DO NOT USE DASHES OR SPACES)[/]\n"));
            }

            return new CustomerBuilder()
                .WithFirstName(firstName)
                .WithLastName(lastName)
                .WithEmail(email)
                .WithCellphone(cellphone)
                .Build();
        }

        private static bool ValidateEmail(string email)
        {
            // TODO: Use RFC 5322 Official standard.
            const string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                                   + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                                   + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
            return !string.IsNullOrEmpty(email) && new Regex(pattern, RegexOptions.IgnoreCase).IsMatch(email);
        }
    }
}