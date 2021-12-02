using NUnit.Framework;
using PersonsokImplementation;
using ServiceReference;

namespace Tests
{   
    /// <summary>
    /// En testklass med enhetstester för att testa projektets integration med SPARs Personsöktjänst
    /// </summary>
    public class PersonsokTests
    {
        private PersonsokServiceClient Client;
        private IdentifieringsinformationTYPE Identifieringsinformation;

        /// <summary>
        /// SetUp metod för att initiera PersonsokServiceClient och IdentifieringsInformation
        /// </summar>
        [SetUp]
        public void Setup()
        {
            Client = Personsok.CreatePersonsokServiceClient(
                "https://kt-ext-ws.statenspersonadressregister.se/2021.1/",                 
                "kt-ext-ws.statenspersonadressregister.se", 
                "Kommun_A.p12",
                "5761213661378233",
                "DigiCert.pem");
            Identifieringsinformation = Personsok.CreateIdentifieringsInformation(
                500243, 
                500243, 
                637,
                "Testsökning C# .NET Core");
        }

        /// <summary>
        /// Enhetstest för personsök med ett personnummer som förväntas att få träff
        /// </summar>
        [Test]
        public void PersonsokGiltigtPersonIdTest()
        {
            PersonSokResponse response = Client.PersonSok(Personsok.CreatePersonIdRequest(Identifieringsinformation, "197912122384"));
            Assert.IsNotNull(response.Items);
            Assert.AreEqual(1, response.Items.Length);
            Assert.IsInstanceOf<AviseringPostTYPE>(response.Items[0]);
        }

        /// <summary>
        /// Enhetstest för personsök med ett personnummer som förväntas att inte få träff
        /// </summar>
        [Test]
        public void PersonsokOgiltigtPersonIdTest()
        {
            PersonSokResponse response = Client.PersonSok(Personsok.CreatePersonIdRequest(Identifieringsinformation, "191212121212"));
            Assert.IsEmpty(response.Items);
        }

        /// <summary>
        /// Enhetstest för personsök med fonetiskt sökning som förväntas att få träff
        /// </summar>
        [Test]
        public void PersonsokFonetisktTraffarTest()
        {
            PersonSokResponse response = Client.PersonSok(Personsok.CreateFonetisktNamnRequest(Identifieringsinformation, "Mikael M*"));
            Assert.IsNotNull(response.Items);
            Assert.IsNotInstanceOf<OverstigerMaxAntalSvarsposterTYPE>(response.Items[0].GetType());
        }

        /// <summary>
        /// Enhetstest för personsök med fonetiskt sökning som förväntas att inte få någon träff
        /// </summar>
        [Test]
        public void PersonsokFonetisktIngaTraffarTest()
        {
            PersonSokResponse response = Client.PersonSok(Personsok.CreateFonetisktNamnRequest(Identifieringsinformation, "NamnSomFörhoppningsvisInteFinns"));
            Assert.IsEmpty(response.Items);   
        }

        /// <summary>
        /// Enhetstest för personsök med fonetiskt sökning som förväntas att få för många träffar
        /// </summar>
        [Test]
        public void PersonsokFonetisktMangaTraffarTest()
        {
            PersonSokResponse response = Client.PersonSok(Personsok.CreateFonetisktNamnRequest(Identifieringsinformation, "An*"));
            Assert.IsNotNull(response.Items);
            Assert.IsInstanceOf<OverstigerMaxAntalSvarsposterTYPE>(response.Items[0]);
        }

        [Test]
        public void PersonsokValidatorTest()
        {
            Assert.True(PersonsokValidator.IsPersonIdValid("197910312391"));
        }
    }
}