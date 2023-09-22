using Microsoft.Extensions.FileProviders;
using ServiceReference;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Security;


namespace PersonsokImplementation
{
    public partial class Personsok
    {
        private static PersonsokLogger Logger = PersonsokLogger.CreatePersonsokLogger();

        /// <summary>
        /// Demonstration av SPAR Personsök program-program version 2021.1
        /// </summary>
        public static void Main(string[] args)
        {
            Logger.LogInformation("Demonstration SPAR Personsök program-program version 2021.1");
            PersonsokServiceClient client = CreatePersonsokServiceClient(
                "https://test-personsok.statenspersonadressregister.se/2021.1/",
                "test-personsok.statenspersonadressregister.se",
                "Kommun_A.p12",
                "4611510421732432",
                "DigiCert.pem");
            IdentifieringsinformationTYPE identifieringsInformation = CreateIdentifieringsInformation(
                500243,
                500243,
                637,
                "Anställd X på avdelning Y, Testsökning C# .NET Core");

            Logger.LogInformation("Personsökning med ett giltigt personnummer");
            PersonSokRequest giltigtPersonIdRequest = CreatePersonIdRequest(identifieringsInformation, "197910312391");
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
            PersonSokRequest fonetisktNamnRequest3 = CreateFonetisktNamnRequest(identifieringsInformation, "Efternamn*");
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
        /// <param name="organisationscertifikatPath">Sökvägen till certifikat från Expisoft</param>
        /// <param name="organisationscertifikatLosenord">Lösenord till certifikat från Expisoft</param>
        /// <param name="sparCertifikatSignerarePath">Sökvägen till certifikat som används för att signera SPAR:s certfikikat</param>
        /// <returns>PersonsokServiceClient</returns>
        public static PersonsokServiceClient CreatePersonsokServiceClient(string url, string domannamn, string organisationscertifikatPath, string organisationscertifikatLosenord, string sparCertifikatSignerarePath)
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
                reader.CopyTo(ms);
                signerandeCertifikat = new X509Certificate2(ms.ToArray());
            }

            PersonsokServiceClient client = new PersonsokServiceClient(binding, endpoint);
            client.Endpoint.EndpointBehaviors.Add(new PersonsokEndpointBehavior());
            client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication = new X509ServiceCertificateAuthentication();
            client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication.CertificateValidationMode = X509CertificateValidationMode.Custom;
            client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication.CustomCertificateValidator = new SPARCertificateValidator(signerandeCertifikat, domannamn);

            using (var reader = embeddedProvider.GetFileInfo(organisationscertifikatPath).CreateReadStream())
            using (var ms = new MemoryStream())
            {
                reader.CopyTo(ms);
                client.ClientCredentials.ClientCertificate.Certificate = new X509Certificate2(ms.ToArray(), organisationscertifikatLosenord);
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
        public static IdentifieringsinformationTYPE CreateIdentifieringsInformation(int kundNrLeveransMottagare, int kundNrSlutkund, int uppdragId, string slutAnvandarId)
        {
            // Behörigheter behöver skickas in för att få ut ytterliggare information från SPAR
            List<SlutAnvandarUtokadBehorighetTYPE> behorigheter = new List<SlutAnvandarUtokadBehorighetTYPE>();
            behorigheter.Add(SlutAnvandarUtokadBehorighetTYPE.Relationer);

            return new IdentifieringsinformationTYPE
            {
                KundNrLeveransMottagare = kundNrLeveransMottagare,
                KundNrSlutkund = kundNrSlutkund,
                SlutAnvandarId = slutAnvandarId,
                UppdragId = uppdragId,
                SlutAnvandarUtokadBehorighet = behorigheter.ToArray()
            };
        }

        /// <summary>
        /// Skapa en fråga från ett personId som är antigen ett personnummer eller samordningsnummer
        /// </summary>
        /// <param name="identifieringsInformation">Identifierar frågeställaren</param>
        /// <param name="personId">Personnummer eller samordningsnummer</param>
        /// <returns>PersonSokRequest</returns>
        public static PersonSokRequest CreatePersonIdRequest(IdentifieringsinformationTYPE identifieringsInformation, string personId)
        {
            if(!PersonsokValidator.IsPersonIdValid(personId))
            {
                throw new ArgumentException("PersonId är i fel format");
            }

            List<object> items = new List<object>();
            items.Add(personId);

            List<ItemsChoiceType> itemsChoiceTypes = new List<ItemsChoiceType>();
            itemsChoiceTypes.Add(ItemsChoiceType.IdNummer);

            PersonSokRequest request = new PersonSokRequest();

            request.Identifieringsinformation = identifieringsInformation;
            request.PersonsokningFraga = new PersonsokningFragaTYPE
            {
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
        public static PersonSokRequest CreateFonetisktNamnRequest(IdentifieringsinformationTYPE identifieringsinformation, string namn)
        {
            List<object> items = new List<object>();
            items.Add(JaNejTYPE.JA);
            items.Add(namn);

            List<ItemsChoiceType> itemsChoiceTypes = new List<ItemsChoiceType>();
            itemsChoiceTypes.Add(ItemsChoiceType.FonetiskSokning);
            itemsChoiceTypes.Add(ItemsChoiceType.NamnSokArgument);

            PersonSokRequest request = new PersonSokRequest();
            request.Identifieringsinformation = identifieringsinformation;
            request.PersonsokningFraga = new PersonsokningFragaTYPE
            {
                Items = items.ToArray(),
                ItemsElementName = itemsChoiceTypes.ToArray()
            };            

            return request;
        }
    }
}
