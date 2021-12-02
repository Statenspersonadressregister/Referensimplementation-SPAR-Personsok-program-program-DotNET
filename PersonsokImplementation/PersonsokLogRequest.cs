using ServiceReference;
using System.Text;


namespace PersonsokImplementation
{
    public partial class Personsok
    {
        /// <summary>
        /// Logga personsökningsrequestet som skickas till SPAR-tjänsten
        /// </summary>
        /// <param name="request">Requestmeddelandet, innehåller identifieringsinformation och personsökningsfrågan</param>
        /// <returns></returns>
        private static void LogPersonsokningRequest(PersonSokRequest request)
        {
            string slutAnvandarId = request.Identifieringsinformation.SlutAnvandarId;
            string uppdragsId = request.Identifieringsinformation.UppdragId.ToString();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Personsökningen gjordes med följande parametrar:");
            sb.AppendLine("SlutAnvändarId: " + slutAnvandarId);
            sb.AppendLine("UppdragsId: " + uppdragsId);

            Logger.LogInformation(sb.ToString());
        }
    }
}
