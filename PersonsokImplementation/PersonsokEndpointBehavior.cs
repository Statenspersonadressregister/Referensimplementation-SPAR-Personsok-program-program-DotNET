using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace PersonsokImplementation
{
    /// <summary>
    /// Implementerar metoder som kan användas för att utöka körbeteende för en slutpunkt 
    /// i antingen en tjänst eller klientapplikation.
    /// </summary>
    public class PersonsokEndpointBehavior : IEndpointBehavior
    {
        private static PersonsokLogger Logger = PersonsokLogger.CreatePersonsokLogger();

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            Logger.LogInformation("Applying client behaviors");
            clientRuntime.ClientMessageInspectors.Add(new PersonsokMessageInspector());
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }


    }
}