using Microsoft.Extensions.FileProviders;
using ServiceReference;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Text;


namespace PersonsokImplementation
{
    public class Personsok
    {
        private static PersonsokLogger Logger = PersonsokLogger.CreatePersonsokLogger();

        /// <summary>
        /// Demonstration av SPAR Personsök program-program version 2019.1
        /// </summary>
        public static void Main(string[] args)
        {
            Logger.LogInformation("Demonstration SPAR Personsök program-program version 2019.1");
            PersonsokServiceClient client = CreatePersonsokServiceClient(
                "https://kt-ext-ws.statenspersonadressregister.se/2019.1/",                 
                "kt-ext-ws.statenspersonadressregister.se", 
                "Kommun_A.p12",
                "5761213661378233", 
                "DigiCert.pem");
            IdentifieringsInformationTYPE identifieringsInformation = CreateIdentifieringsInformation(
                500243, 
                500243, 
                "Testsökning C# .NET Core");
                
            Logger.LogInformation("Personsökning med ett giltigt personnummer");
            PersonSokRequest giltigtPersonIdRequest = CreatePersonIdRequest(identifieringsInformation, "197912122384");
            LogPersonsokningRequest(giltigtPersonIdRequest);
            PersonSokResponse giltigtPersonIdResponse = client.PersonSok(giltigtPersonIdRequest);
            LogPersonsokningResponse(giltigtPersonIdResponse);

            Logger.LogInformation("Personsökning med ett ogiltigt personnummer");
            PersonSokRequest ogiltigtPersonIdRequest = CreatePersonIdRequest(identifieringsInformation, "191212121212");
            LogPersonsokningRequest(ogiltigtPersonIdRequest);
            PersonSokResponse ogiltigtPersonIdResponse = client.PersonSok(ogiltigtPersonIdRequest);
            LogPersonsokningResponse(ogiltigtPersonIdResponse);

            Logger.LogInformation("Personsökning med ett fonetiskt namn, med förväntad träff");
            PersonSokRequest fonetisktNamnRequest = CreateFonetisktNamnRequest(identifieringsInformation, "Mikael Efter*");
            LogPersonsokningRequest(fonetisktNamnRequest);
            PersonSokResponse fonetisktNamnResponse = client.PersonSok(fonetisktNamnRequest);
            LogPersonsokningResponse(fonetisktNamnResponse);

            Logger.LogInformation("Personsökning med ett fonetiskt namn, utan förväntad träff");
            PersonSokRequest fonetisktNamnRequest2 = CreateFonetisktNamnRequest(identifieringsInformation, "NamnSomFörhoppningsvisInteFinns");
            LogPersonsokningRequest(fonetisktNamnRequest2);
            PersonSokResponse fonetisktNamnResponse2 = client.PersonSok(fonetisktNamnRequest2);
            LogPersonsokningResponse(fonetisktNamnResponse2);

            Logger.LogInformation("Personsökning med ett fonetiskt namn, med många träffar");
            PersonSokRequest fonetisktNamnRequest3 = CreateFonetisktNamnRequest(identifieringsInformation, "An*");
            LogPersonsokningRequest(fonetisktNamnRequest3);
            PersonSokResponse fonetisktNamnResponse3 = client.PersonSok(fonetisktNamnRequest3);
            LogPersonsokningResponse(fonetisktNamnResponse3);
        }

        /// <summary>
        /// Hämtar absolute path till angiven fil. Utgår ifrån att filen ligger i projektrooten under mappen Certificates.
        /// Pathen kan variera beroende på vilket körläge det är (Debug, Release)
        /// </summary>
        /// <param name="filename">Filnamnet</param>
        /// <returns>string</returns>
        public static string GetFilePath(string filename)
        {
            string currentDir = Directory.GetCurrentDirectory();
            string dirName = Path.GetDirectoryName(currentDir);
            Logger.LogInformation("currdir " + currentDir);
            Logger.LogInformation("dirname " + dirName);
            return Path.Combine(currentDir, "Certificates", filename);
        }

        /// <summary>
        /// Skapar en klient som används för att prata med webbtjänsten SPAR Personsök program-program
        /// </summary>
        /// <param name="url">Address till tjänsten</param>
        /// <param name="domannamn">Domännamn till tjänsten</param>
        /// <param name="klientCerfikatPath">Sökvägen till certifikat från Expisoft</param>
        /// <param name="klientCertifikatLosenord">Lösenord till certifikat från Expisoft</param>
        /// <param name="sparCertifikatSignerarePath">Sökvägen till certifikat som används för att signera SPAR:s certfikikat</param>
        /// <returns>PersonsokServiceClient</returns>
        public static PersonsokServiceClient CreatePersonsokServiceClient(string url, string domannamn, string klientCerfikatPath, string klientCertifikatLosenord, string sparCertifikatSignerarePath)
        {
            BasicHttpsBinding binding = new BasicHttpsBinding();
            binding.Security.Mode = BasicHttpsSecurityMode.Transport;
            binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;

            EndpointAddress endpoint = new EndpointAddress(url);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            X509Certificate2 signerandeCertifikat;

            var embeddedProvider = new EmbeddedFileProvider(Assembly.GetExecutingAssembly());
            
            
            using (var reader = embeddedProvider.GetFileInfo(sparCertifikatSignerarePath).CreateReadStream())
            using (var ms = new MemoryStream())
            {
                //
                reader.CopyTo(ms);
                signerandeCertifikat = new X509Certificate2(ms.ToArray());
            }
           
            PersonsokServiceClient client = new PersonsokServiceClient(binding, endpoint);
            client.Endpoint.EndpointBehaviors.Add(new PersonsokEndpointBehavior());
            client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication = new X509ServiceCertificateAuthentication();
            client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication.CertificateValidationMode = X509CertificateValidationMode.Custom;
            client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication.CustomCertificateValidator = new SPARCertificateValidator(signerandeCertifikat, domannamn);
            
            using (var reader = embeddedProvider.GetFileInfo(klientCerfikatPath).CreateReadStream())
            using (var ms = new MemoryStream())
            {
                reader.CopyTo(ms);
                client.ClientCredentials.ClientCertificate.Certificate = new X509Certificate2(ms.ToArray(), klientCertifikatLosenord);
            }
       
            return client;
        }

        /// <summary>
        /// Skapa identifieringsinformation för att användas i frågor mot SPAR personsök program-program
        /// </summary>
        /// <param name="kundNrLeveransMottagare">Kundnummer för leveransmottagare</param>
        /// <param name="kundNrSlutkund">Kundnummer för slutkund</param>
        /// <param name="slutAnvandarId">Fritext för att lättare identifiera frågor</param>
        /// <returns>IdentifieringsInformation</returns>
        public static IdentifieringsInformationTYPE CreateIdentifieringsInformation(int kundNrLeveransMottagare, int kundNrSlutkund, string slutAnvandarId) 
        { 
            // Behörigheter behöver skickas in för att få ut ytterliggare information från SPAR
            List<SlutAnvandarUtokadBehorighetTYPE> behorigheter = new List<SlutAnvandarUtokadBehorighetTYPE>();
            behorigheter.Add(SlutAnvandarUtokadBehorighetTYPE.Relationer);

            return new IdentifieringsInformationTYPE 
            {
                KundNrLeveransMottagare = kundNrLeveransMottagare,
                KundNrSlutkund = kundNrSlutkund,
                OrgNrSlutkund = "0000000000",
                SlutAnvandarId = slutAnvandarId,
                Tidsstampel = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")),
                UppdragsId = 637,
                SlutAnvandarUtokadBehorighet = behorigheter.ToArray()
            };
        }

        /// <summary>
        /// Skapa en fråga från ett personId som är antigen ett personnummer eller samordningsnummer
        /// </summary>
        /// <param name="identifieringsInformation">Identifierar frågeställaren</param>
        /// <param name="personId">Personnummer eller samordningsnummer</param>
        /// <returns>PersonSokRequest</returns>
        public static PersonSokRequest CreatePersonIdRequest(IdentifieringsInformationTYPE identifieringsInformation, string personId)
        { 
            if(!PersonsokValidator.IsPersonIdValid(personId))
            {
                throw new ArgumentException("PersonId är i fel format");
            }
            
            List<object> items = new List<object>();
            items.Add(new PersonIdTYPE { FysiskPersonId = personId });

            List<ItemsChoiceType> itemsChoiceTypes = new List<ItemsChoiceType>();
            itemsChoiceTypes.Add(ItemsChoiceType.PersonId);

            PersonSokRequest request = new PersonSokRequest();
            request.IdentifieringsInformation = identifieringsInformation;
            request.PersonsokningFraga = new PersonsokningFragaTYPE{
                Items = items.ToArray(),
                ItemsElementName = itemsChoiceTypes.ToArray()
            };

            return request;
        }

        /// <summary>
        /// Skapa en fråga från ett namn, namnet kommer i SPAR hanteras fonetiserat
        /// </summary>
        /// <param name="identifieringsInformation">Identifierar frågeställaren</param>
        /// <param name="namn">Namn, fritext</param>
        /// <returns>PersonSokRequest</returns>
        public static PersonSokRequest CreateFonetisktNamnRequest(IdentifieringsInformationTYPE identifieringsInformation, string namn)
        {
            List<object> items = new List<object>();
            items.Add(FonetiskSokningTYPE.J);
            items.Add(namn);

            List<ItemsChoiceType> itemsChoiceTypes = new List<ItemsChoiceType>();
            itemsChoiceTypes.Add(ItemsChoiceType.FonetiskSokning);
            itemsChoiceTypes.Add(ItemsChoiceType.NamnSokArgument);

            PersonSokRequest request = new PersonSokRequest();
            request.IdentifieringsInformation = identifieringsInformation;
            request.PersonsokningFraga = new PersonsokningFragaTYPE{
                Items = items.ToArray(),
                ItemsElementName = itemsChoiceTypes.ToArray()
            };

            return request;
        }

        /// <summary>
        /// Logga personsökningsrequestet som skickas till SPAR-tjänsten
        /// </summary>
        /// <param name="request">Requestmeddelandet, innehåller identifieringsinformation och personsökningsfrågan</param>
        /// <returns></returns>
        private static void LogPersonsokningRequest(PersonSokRequest request)
        {
            string slutAnvandarId = request.IdentifieringsInformation.SlutAnvandarId;
            string tidsstampel = request.IdentifieringsInformation.Tidsstampel.ToString("yyyy-MM-dd hh:mm:ss.fff");
            string uppdragsId = request.IdentifieringsInformation.UppdragsId.ToString();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Personsökningen gjordes med följande parametrar:");
            sb.AppendLine("SlutAnvändarId: " + slutAnvandarId);
            sb.AppendLine("Tidsstämpel: " + tidsstampel);
            sb.AppendLine("UppdragsId: " + uppdragsId);
            
            Logger.LogInformation(sb.ToString());
        }

        /// <summary>
        /// Logga personsökningssvaret som tas emot från SPAR-tjänsten
        /// Loggningen visar även hur man kan ta ut information från responsen
        /// </summary>
        /// <param name="response">Responsemeddelandet, innehåller svaret från anropet</param>
        /// <returns></returns>
        private static void LogPersonsokningResponse(PersonSokResponse response)
        {   
            List<object> svarsposter = response.Items.ToList();
            List<AviseringsPostTYPE> aviseringsposter = svarsposter
                .FindAll(a => a.GetType() == typeof(AviseringsPostTYPE))
                .Select(a => (AviseringsPostTYPE) a)
                .ToList();
            List<UndantagTYPE> undantagsposter = svarsposter
                .FindAll(u => u.GetType() == typeof(UndantagTYPE))
                .Select(u => (UndantagTYPE) u)
                .ToList();
            List<OverstigerMaxAntalSvarsposterTYPE> maxantalsposter = svarsposter
                .FindAll(m => m.GetType() == typeof(OverstigerMaxAntalSvarsposterTYPE))
                .Select(m => (OverstigerMaxAntalSvarsposterTYPE) m)
                .ToList();

            int antalSvarsposter = svarsposter == null ? 0 : svarsposter.Count();
            int antalAviseringsposter = aviseringsposter == null ? 0 : aviseringsposter.Count();
            int antalUndantagsposter = undantagsposter == null ? 0 : undantagsposter.Count();
            int antalMaxantalsposter = maxantalsposter == null ? 0 : maxantalsposter.Count();
            
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Personsökningen gav " + antalSvarsposter + " sökträff(ar)");
            sb.AppendLine("Antal aviseringsposter: " + antalAviseringsposter);
            sb.AppendLine("Antal undantagsposter: " + antalUndantagsposter);
            sb.AppendLine("Antal maxantalsposter: " + antalMaxantalsposter);

            sb = antalAviseringsposter <= 0 ? sb : LogAviseringsPoster(sb, aviseringsposter);
            sb = antalUndantagsposter <= 0 ? sb : LogUndantagsPoster(sb, undantagsposter);
            sb = antalMaxantalsposter <= 0 ? sb : LogMaxantalsPoster(sb, maxantalsposter);

            Logger.LogInformation(sb.ToString());
        }

        private static StringBuilder LogAviseringsPoster(StringBuilder sb, List<AviseringsPostTYPE> aviseringsposter)
        {
            foreach(AviseringsPostTYPE aviseringspost in aviseringsposter)
            {   
                PersondetaljerTYPE persondetaljer = aviseringspost.Persondetaljer[0];
                sb.AppendLine("PersonId: " + aviseringspost.PersonId.FysiskPersonId);
                sb.AppendLine("Förnamn: " + persondetaljer.Fornamn);
                sb.AppendLine("Efternamn: " + persondetaljer.Efternamn);
                sb.AppendLine();
            }
            
            return sb;
        }

        private static StringBuilder LogUndantagsPoster(StringBuilder sb, List<UndantagTYPE> undantagsposter)
        {
            foreach(UndantagTYPE undantagspost in undantagsposter)
            {
                sb.AppendLine("Kod: " + undantagspost.Kod);
                sb.AppendLine("Beskrivning: " + undantagspost.Beskrivning);
            }

            return sb;
        }

        private static StringBuilder LogMaxantalsPoster(StringBuilder sb, List<OverstigerMaxAntalSvarsposterTYPE> maxantalsposter)
        {
            foreach(OverstigerMaxAntalSvarsposterTYPE maxantalspost in maxantalsposter)
            {
                sb.AppendLine("Antal poster:" + maxantalspost.AntalPoster);
                sb.AppendLine("Max antal poster: " + maxantalspost.MaxAntalSvarsPoster);
            }

            return sb;
        }
    }
}
