namespace PhonebookSystem
{
    using PhonebookSystem.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Globalization;

    public class Phonebook
    {
        private const string DefaultCountryCode = "+359";
        private const char PlusSymbol = '+';
        private const string AddPhoneCommandName = "AddPhone";
        private const string ChangePhoneCommandName = "ChangePhone";       
        private const string ListCommandName = "List";
        private const string EndCommandName = "End";

        private static readonly char[] CommandParamsSeparators = { ',' };
        private static readonly IPhonebookRepository PhonebookRepository = new PhonebookRepository();
        private static readonly StringBuilder Output = new StringBuilder();

        public static void Main()
        {
            while (true)
            {
                var currentCommandText = Console.ReadLine();
                if (currentCommandText == EndCommandName || currentCommandText == null)
                {
                    break;
                }

                string commandResult = ExecuteCommand(currentCommandText);

                AppendOutput(commandResult);
            }

            Console.Write(Output);
        }

        private static string ExecuteCommand(string currentCommandText)
        {
            var firstIndexOfBracket = currentCommandText.IndexOf('(');

            if (firstIndexOfBracket == -1)
            {
                throw new InvalidOperationException("Invalid command text: doesn't start with '('.");
            }

            if (!currentCommandText.EndsWith(")"))
            {
                throw new InvalidOperationException("Invalid command text: doesn't end with ')'.");
            }

            var commandParamsText = currentCommandText.Substring(
                firstIndexOfBracket + 1,
                currentCommandText.Length - firstIndexOfBracket - 2);

            var commandParams = commandParamsText.Split(CommandParamsSeparators, StringSplitOptions.RemoveEmptyEntries);

            var commandName = currentCommandText.Substring(0, firstIndexOfBracket);

            string commandResult;
            if ((commandName.StartsWith(AddPhoneCommandName)) && commandParams.Length >= 2)
            {
                commandResult = ExecuteCommand(AddPhoneCommandName, commandParams);
            }
            else if ((commandName == ChangePhoneCommandName) && commandParams.Length == 2)
            {
                commandResult = ExecuteCommand(ChangePhoneCommandName, commandParams);
            }
            else if ((commandName == ListCommandName) && commandParams.Length == 2)
            {
                commandResult = ExecuteCommand(ListCommandName, commandParams);
            }
            else
            {
                throw new InvalidOperationException("Invalid command name or command params.");
            }
            return commandResult;
        }

        private static string ExecuteCommand(string commandName, IList<string> commandParams)
        {
            string commandResult;
            if (commandName == AddPhoneCommandName)
            {
                var name = commandParams[0];
                var phoneNumbers = commandParams.Skip(1).ToList();

                commandResult = ExecuteAddPhoneCommand(name, phoneNumbers);
            } 
            else if (commandName == ChangePhoneCommandName)
            {
                var oldPhoneNumber = ConvertToCanonicalForm(commandParams[0]);
                var newPhoneNumber = ConvertToCanonicalForm(commandParams[1]);

                var entriesChangedCount = PhonebookRepository.ChangePhone(oldPhoneNumber, newPhoneNumber);

                commandResult = string.Format("{0} numbers changed", entriesChangedCount);
            }
            else if(commandName == ListCommandName)
            {
                var startIndex = int.Parse(commandParams[0]);
                var count = int.Parse(commandParams[1]);

                commandResult = ExecuteListCommand(startIndex, count);
            }
            else
            {
                throw new ArgumentOutOfRangeException("commandName");
            }

            return commandResult;               
        }

        private static string ExecuteListCommand(int startIndex, int count)
        {
            IEnumerable<PhonebookEntry> entries;
            try
            {
                entries = PhonebookRepository.ListEntries(startIndex, count);
            }
            catch (ArgumentOutOfRangeException)
            {
                return "Invalid range";
            }

            var commandResult = string.Join(Environment.NewLine, entries);

            return commandResult;
        }

        private static string ExecuteAddPhoneCommand(string name, List<string> phoneNumbers)
        {
            ConvertToCanonicalForms(phoneNumbers);

            var isNewEntry = PhonebookRepository.AddPhone(name, phoneNumbers);

            if (isNewEntry)
            {
                return "Phone entry created";
            }

            return "Phone entry merged"; 
        }

        private static void ConvertToCanonicalForms(List<string> phoneNumbers)
        {
            for (int i = 0; i < phoneNumbers.Count; i++)
            {
                phoneNumbers[i] = ConvertToCanonicalForm(phoneNumbers[i]);
            }
        }
        private static string ConvertToCanonicalForm(string phoneNumber)
        {
            var digitsAndPlusPhoneNumber = GetPhoneNumberDigitsAndPlus(phoneNumber);

            var noneZeroLeadingPhoneNumber = digitsAndPlusPhoneNumber.TrimStart('0');

            var phoneNumberCanonicalForm = noneZeroLeadingPhoneNumber;

            if (phoneNumberCanonicalForm.StartsWith(PlusSymbol.ToString(CultureInfo.InvariantCulture)))
            {
                return phoneNumberCanonicalForm;
            }

            var phoneNumberCanonicalFormPlus = string.Format("{0}{1}", PlusSymbol, phoneNumberCanonicalForm);

            if (phoneNumberCanonicalFormPlus.StartsWith(DefaultCountryCode))
            {
                return phoneNumberCanonicalFormPlus;
            }

            return string.Format("{0}{1}", DefaultCountryCode, phoneNumberCanonicalForm);
        }

        private static string GetPhoneNumberDigitsAndPlus(string phoneNumber)
        {
            var result = new StringBuilder();

            foreach (var symbol in phoneNumber)
            {
                if (char.IsDigit(symbol) || symbol == PlusSymbol)
                {
                    result.Append(symbol);
                }               
            }

            return result.ToString();
        }
        private static void AppendOutput(string text)
        {
            Output.AppendLine(text);
        }
    }   
}