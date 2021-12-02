using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace PersonsokImplementation
{
    /// <summary>
    /// Definierar ett meddelandeinspektörsobjekt som kan läggas till i meddelandekontrollens 
    /// samling för att visa eller ändra meddelanden. 
    /// </summary>
    public class PersonsokMessageInspector : IClientMessageInspector
    {
        private static PersonsokLogger Logger = PersonsokLogger.CreatePersonsokLogger();

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            Logger.LogInformation("Received response");
        }

        /// <summary>
        /// Kopierar requestmeddelandet och anropar validering,
        /// om meddelandet är ej giltigt med xml-scheman så avbryts förfrågningen till personsök.
        /// </summary>
        /// <returns>object</returns>
        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            MessageBuffer buffer = request.CreateBufferedCopy(int.MaxValue);
            Message copy = buffer.CreateMessage();

            Logger.LogInformation("Validating request");
            bool isRequestValid = PersonsokValidator.ValidateXml(copy.GetReaderAtBodyContents());

            if (!isRequestValid)
            {
                Logger.LogError("Request is not valid, aborting!");
                channel.Abort();
            }
            else
            {
                Logger.LogInformation("Sending request");
                request = buffer.CreateMessage();
            }

            return request;
        }
    }
}