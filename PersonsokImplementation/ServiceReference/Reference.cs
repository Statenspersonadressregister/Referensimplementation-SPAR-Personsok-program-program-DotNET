using System;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Threading.Tasks;
using System.Xml.Serialization;

//------------------------------------------------------------------------------
// Majoriteten av kod i denna fil har autogenererats med verktyg,
// men en del ändringar har gjorts i efterhand.
//
// Om ni väljer att generera egna proxyfiler och proxyklasser så är
// det rekommenderat att ni går igenom koden här nedan.
//
// Verktyg för autogenerering: WCF via svcutil.exe.
// Notera att WCF inte längre stöds i .NET Core.
//------------------------------------------------------------------------------
namespace ServiceReference
{

    [ServiceContractAttribute(Namespace="http://statenspersonadressregister.se/personsok/2021.1", ConfigurationName="PersonsokService")]
    public interface PersonsokService
    {
        [OperationContractAttribute(Action="http://skatteverket.se/spar/personsok/2021.1/PersonsokService/Personsok", ReplyAction="*")]
        [XmlSerializerFormatAttribute(SupportFaults=true)]
        PersonSokResponse PersonSok(PersonSokRequest request);

        [OperationContractAttribute(Action="http://skatteverket.se/spar/personsok/2021.1/PersonsokService/Personsok", ReplyAction="*")]
        Task<PersonSokResponse> PersonSokAsync(PersonSokRequest request);
    }

    [DebuggerStepThroughAttribute()]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://statenspersonadressregister.se/schema/personsok/2021.1/personsokningfraga")]
    public partial class SPARPersonsokningFraga
    {
        
        private IdentifieringsinformationTYPE identifieringsinformationField;
        
        private PersonsokningFragaTYPE personsokningFragaField;
        
        [XmlElementAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/metadata/identifieringsinformationWs-1.1", Order=0)]
        public IdentifieringsinformationTYPE Identifieringsinformation
        {
            get
            {
                return this.identifieringsinformationField;
            }
            set
            {
                this.identifieringsinformationField = value;
            }
        }
        
        [XmlElementAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/sok/personsokningsokparametrar-1.1", Order=1)]
        public PersonsokningFragaTYPE PersonsokningFraga
        {
            get
            {
                return this.personsokningFragaField;
            }
            set
            {
                this.personsokningFragaField = value;
            }
        }
    }

    [DebuggerStepThroughAttribute()]
    [XmlTypeAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/metadata/identifieringsinformationWs-1.1")]
    public partial class IdentifieringsinformationTYPE
    {
        
        private int kundNrLeveransMottagareField;
        
        private int kundNrSlutkundField;
        
        private long uppdragIdField;
        
        private string slutAnvandarIdField;
        
        private SlutAnvandarUtokadBehorighetTYPE[] slutAnvandarUtokadBehorighetField;
        
        [XmlElementAttribute(Order=0)]
        public int KundNrLeveransMottagare
        {
            get
            {
                return this.kundNrLeveransMottagareField;
            }
            set
            {
                this.kundNrLeveransMottagareField = value;
            }
        }
        
        [XmlElementAttribute(Order=1)]
        public int KundNrSlutkund
        {
            get
            {
                return this.kundNrSlutkundField;
            }
            set
            {
                this.kundNrSlutkundField = value;
            }
        }
        
        [XmlElementAttribute(Order=2)]
        public long UppdragId
        {
            get
            {
                return this.uppdragIdField;
            }
            set
            {
                this.uppdragIdField = value;
            }
        }
        
        [XmlElementAttribute(Order=3)]
        public string SlutAnvandarId
        {
            get
            {
                return this.slutAnvandarIdField;
            }
            set
            {
                this.slutAnvandarIdField = value;
            }
        }
        
        [XmlElementAttribute("SlutAnvandarUtokadBehorighet", Order=4)]
        public SlutAnvandarUtokadBehorighetTYPE[] SlutAnvandarUtokadBehorighet
        {
            get
            {
                return this.slutAnvandarUtokadBehorighetField;
            }
            set
            {
                this.slutAnvandarUtokadBehorighetField = value;
            }
        }
    }

    [XmlTypeAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/metadata/identifieringsinformationWs-1.1")]
    public enum SlutAnvandarUtokadBehorighetTYPE
    {
        
        Relationer,
        
        Medborgarskap,
        
        Taxering,
        
        Sekretess,
    }

    [DebuggerStepThroughAttribute()]
    [XmlTypeAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/sok/undantag-1.0")]
    public partial class OverstigerMaxAntalSvarsposterTYPE
    {
        
        private int antalPosterField;
        
        private int maxAntalSvarsposterField;
        
        [XmlElementAttribute(Order=0)]
        public int AntalPoster
        {
            get
            {
                return this.antalPosterField;
            }
            set
            {
                this.antalPosterField = value;
            }
        }
        
        [XmlElementAttribute(Order=1)]
        public int MaxAntalSvarsposter
        {
            get
            {
                return this.maxAntalSvarsposterField;
            }
            set
            {
                this.maxAntalSvarsposterField = value;
            }
        }
    }

    [DebuggerStepThroughAttribute()]
    [XmlTypeAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/sok/undantag-1.0")]
    public partial class UndantagTYPE
    {
        
        private string kodField;
        
        private string beskrivningField;
        
        [XmlElementAttribute(Order=0)]
        public string Kod
        {
            get
            {
                return this.kodField;
            }
            set
            {
                this.kodField = value;
            }
        }
        
        [XmlElementAttribute(Order=1)]
        public string Beskrivning
        {
            get
            {
                return this.beskrivningField;
            }
            set
            {
                this.beskrivningField = value;
            }
        }
    }

    [DebuggerStepThroughAttribute()]
    [XmlTypeAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/fastighetstaxering-1.2")]
    public partial class FastighetDelTYPE
    {
        
        private string taxeringsidentitetField;
        
        private string fastighetBeteckningField;
        
        private string andelstalTaljareField;
        
        private string andelstalNamnareField;
        
        [XmlElementAttribute(Order=0)]
        public string Taxeringsidentitet
        {
            get
            {
                return this.taxeringsidentitetField;
            }
            set
            {
                this.taxeringsidentitetField = value;
            }
        }
        
        [XmlElementAttribute(Order=1)]
        public string FastighetBeteckning
        {
            get
            {
                return this.fastighetBeteckningField;
            }
            set
            {
                this.fastighetBeteckningField = value;
            }
        }
        
        [XmlElementAttribute(DataType="integer", Order=2)]
        public string AndelstalTaljare
        {
            get
            {
                return this.andelstalTaljareField;
            }
            set
            {
                this.andelstalTaljareField = value;
            }
        }
        
        [XmlElementAttribute(DataType="integer", Order=3)]
        public string AndelstalNamnare
        {
            get
            {
                return this.andelstalNamnareField;
            }
            set
            {
                this.andelstalNamnareField = value;
            }
        }
    }

    [DebuggerStepThroughAttribute()]
    [XmlTypeAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/fastighetstaxering-1.2")]
    public partial class FastighetTYPE
    {
        
        private string taxeringsenhetsnummerField;
        
        private string lanKodField;
        
        private string kommunKodField;
        
        private string fastighetKodField;
        
        private string taxeringsarField;
        
        private string taxeringsvardeField;
        
        private FastighetDelTYPE[] fastighetDelField;
        
        [XmlElementAttribute(Order=0)]
        public string Taxeringsenhetsnummer
        {
            get
            {
                return this.taxeringsenhetsnummerField;
            }
            set
            {
                this.taxeringsenhetsnummerField = value;
            }
        }
        
        [XmlElementAttribute(Order=1)]
        public string LanKod
        {
            get
            {
                return this.lanKodField;
            }
            set
            {
                this.lanKodField = value;
            }
        }
        
        [XmlElementAttribute(Order=2)]
        public string KommunKod
        {
            get
            {
                return this.kommunKodField;
            }
            set
            {
                this.kommunKodField = value;
            }
        }
        
        [XmlElementAttribute(Order=3)]
        public string FastighetKod
        {
            get
            {
                return this.fastighetKodField;
            }
            set
            {
                this.fastighetKodField = value;
            }
        }
        
        [XmlElementAttribute(Order=4)]
        public string Taxeringsar
        {
            get
            {
                return this.taxeringsarField;
            }
            set
            {
                this.taxeringsarField = value;
            }
        }
        
        [XmlElementAttribute(Order=5)]
        public string Taxeringsvarde
        {
            get
            {
                return this.taxeringsvardeField;
            }
            set
            {
                this.taxeringsvardeField = value;
            }
        }
        
        [XmlElementAttribute("FastighetDel", Order=6)]
        public FastighetDelTYPE[] FastighetDel
        {
            get
            {
                return this.fastighetDelField;
            }
            set
            {
                this.fastighetDelField = value;
            }
        }
    }

    [DebuggerStepThroughAttribute()]
    [XmlTypeAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/relation-1.2")]
    public partial class RelationTYPE
    {
        
        private DateTime datumFromField;
        
        private DateTime datumTillField;
        
        private RelationstypTYPE relationstypField;
        
        private string idNummerField;
        
        private string fornamnField;
        
        private string mellannamnField;
        
        private string efternamnField;
        
        private DateTime fodelsetidField;
        
        private bool fodelsetidFieldSpecified;
        
        private string avregistreringsorsakKodField;
        
        private string avregistreringsdatumField;
        
        private string avlidendatumField;
        
        [XmlElementAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/generellt/datumtid-1.1", DataType="date", Order=0)]
        public DateTime DatumFrom
        {
            get
            {
                return this.datumFromField;
            }
            set
            {
                this.datumFromField = value;
            }
        }
        
        [XmlElementAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/generellt/datumtid-1.1", DataType="date", Order=1)]
        public DateTime DatumTill
        {
            get
            {
                return this.datumTillField;
            }
            set
            {
                this.datumTillField = value;
            }
        }
        
        [XmlElementAttribute(Order=2)]
        public RelationstypTYPE Relationstyp
        {
            get
            {
                return this.relationstypField;
            }
            set
            {
                this.relationstypField = value;
            }
        }
        
        [XmlElementAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/person-1.2", Order=3)]
        public string IdNummer
        {
            get
            {
                return this.idNummerField;
            }
            set
            {
                this.idNummerField = value;
            }
        }
        
        [XmlElementAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/namn/deladenamnelement-1.0", Order=4)]
        public string Fornamn
        {
            get
            {
                return this.fornamnField;
            }
            set
            {
                this.fornamnField = value;
            }
        }
        
        [XmlElementAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/namn/deladenamnelement-1.0", Order=5)]
        public string Mellannamn
        {
            get
            {
                return this.mellannamnField;
            }
            set
            {
                this.mellannamnField = value;
            }
        }
        
        [XmlElementAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/namn/deladenamnelement-1.0", Order=6)]
        public string Efternamn
        {
            get
            {
                return this.efternamnField;
            }
            set
            {
                this.efternamnField = value;
            }
        }
        
        [XmlElementAttribute(DataType="date", Order=7)]
        public DateTime Fodelsetid
        {
            get
            {
                return this.fodelsetidField;
            }
            set
            {
                this.fodelsetidField = value;
            }
        }
        
        [XmlIgnoreAttribute()]
        public bool FodelsetidSpecified
        {
            get
            {
                return this.fodelsetidFieldSpecified;
            }
            set
            {
                this.fodelsetidFieldSpecified = value;
            }
        }
        
        [XmlElementAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/avregistrering-1.0", Order=8)]
        public string AvregistreringsorsakKod
        {
            get
            {
                return this.avregistreringsorsakKodField;
            }
            set
            {
                this.avregistreringsorsakKodField = value;
            }
        }
        
        [XmlElementAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/avregistrering-1.0", Order=9)]
        public string Avregistreringsdatum
        {
            get
            {
                return this.avregistreringsdatumField;
            }
            set
            {
                this.avregistreringsdatumField = value;
            }
        }
        
        [XmlElementAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/avregistrering-1.0", Order=10)]
        public string Avlidendatum
        {
            get
            {
                return this.avlidendatumField;
            }
            set
            {
                this.avlidendatumField = value;
            }
        }
    }

    [XmlTypeAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/relation-1.2")]
    public enum RelationstypTYPE
    {
        
        VÅRDNADSHAVARE,
        
        [XmlEnumAttribute("MAKE/MAKA/REGISTRERAD PARTNER")]
        MAKEMAKAREGISTRERADPARTNER,
    }

    [DebuggerStepThroughAttribute()]
    [XmlTypeAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/adress/kontaktadress-1.0")]
    public partial class KontaktadressTYPE
    {
        
        private object itemField;
        
        [XmlElementAttribute("InternationellAdress", typeof(InternationellAdressTYPE), Namespace="http://statenspersonadressregister.se/schema/komponent/person/adress/deladeadresselement-1.1", Order=0)]
        [XmlElementAttribute("SvenskAdress", typeof(SvenskAdressTYPE), Namespace="http://statenspersonadressregister.se/schema/komponent/person/adress/deladeadresselement-1.1", Order=0)]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }

    [DebuggerStepThroughAttribute()]
    [XmlTypeAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/adress/deladeadresselement-1.1")]
    public partial class InternationellAdressTYPE
    {
        
        private DateTime datumFromField;
        
        private DateTime datumTillField;
        
        private string utdelningsadress1Field;
        
        private string utdelningsadress2Field;
        
        private string utdelningsadress3Field;
        
        private string landField;
        
        [XmlElementAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/generellt/datumtid-1.1", DataType="date", Order=0)]
        public DateTime DatumFrom
        {
            get
            {
                return this.datumFromField;
            }
            set
            {
                this.datumFromField = value;
            }
        }
        
        [XmlElementAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/generellt/datumtid-1.1", DataType="date", Order=1)]
        public DateTime DatumTill
        {
            get
            {
                return this.datumTillField;
            }
            set
            {
                this.datumTillField = value;
            }
        }
        
        [XmlElementAttribute(Order=2)]
        public string Utdelningsadress1
        {
            get
            {
                return this.utdelningsadress1Field;
            }
            set
            {
                this.utdelningsadress1Field = value;
            }
        }
        
        [XmlElementAttribute(Order=3)]
        public string Utdelningsadress2
        {
            get
            {
                return this.utdelningsadress2Field;
            }
            set
            {
                this.utdelningsadress2Field = value;
            }
        }
        
        [XmlElementAttribute(Order=4)]
        public string Utdelningsadress3
        {
            get
            {
                return this.utdelningsadress3Field;
            }
            set
            {
                this.utdelningsadress3Field = value;
            }
        }
        
        [XmlElementAttribute(Order=5)]
        public string Land
        {
            get
            {
                return this.landField;
            }
            set
            {
                this.landField = value;
            }
        }
    }

    [DebuggerStepThroughAttribute()]
    [XmlTypeAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/adress/deladeadresselement-1.1")]
    public partial class SvenskAdressTYPE
    {
        
        private DateTime datumFromField;
        
        private DateTime datumTillField;
        
        private string careOfField;
        
        private string utdelningsadress1Field;
        
        private string utdelningsadress2Field;
        
        private string postNrField;
        
        private string postortField;
        
        [XmlElementAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/generellt/datumtid-1.1", DataType="date", Order=0)]
        public DateTime DatumFrom
        {
            get
            {
                return this.datumFromField;
            }
            set
            {
                this.datumFromField = value;
            }
        }
        
        [XmlElementAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/generellt/datumtid-1.1", DataType="date", Order=1)]
        public DateTime DatumTill
        {
            get
            {
                return this.datumTillField;
            }
            set
            {
                this.datumTillField = value;
            }
        }
        
        [XmlElementAttribute(Order=2)]
        public string CareOf
        {
            get
            {
                return this.careOfField;
            }
            set
            {
                this.careOfField = value;
            }
        }
        
        [XmlElementAttribute(Order=3)]
        public string Utdelningsadress1
        {
            get
            {
                return this.utdelningsadress1Field;
            }
            set
            {
                this.utdelningsadress1Field = value;
            }
        }
        
        [XmlElementAttribute(Order=4)]
        public string Utdelningsadress2
        {
            get
            {
                return this.utdelningsadress2Field;
            }
            set
            {
                this.utdelningsadress2Field = value;
            }
        }
        
        [XmlElementAttribute(Order=5)]
        public string PostNr
        {
            get
            {
                return this.postNrField;
            }
            set
            {
                this.postNrField = value;
            }
        }
        
        [XmlElementAttribute(Order=6)]
        public string Postort
        {
            get
            {
                return this.postortField;
            }
            set
            {
                this.postortField = value;
            }
        }
    }

    [DebuggerStepThroughAttribute()]
    [XmlTypeAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/adress/utlandsadress-1.1")]
    public partial class UtlandsadressTYPE
    {
        
        private InternationellAdressTYPE internationellAdressField;
        
        [XmlElementAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/adress/deladeadresselement-1.1", Order=0)]
        public InternationellAdressTYPE InternationellAdress
        {
            get
            {
                return this.internationellAdressField;
            }
            set
            {
                this.internationellAdressField = value;
            }
        }
    }

    [DebuggerStepThroughAttribute()]
    [XmlTypeAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/adress/sarskildpostadress-1.1")]
    public partial class SarskildPostadressTYPE
    {
        
        private object itemField;
        
        [XmlElementAttribute("InternationellAdress", typeof(InternationellAdressTYPE), Namespace="http://statenspersonadressregister.se/schema/komponent/person/adress/deladeadresselement-1.1", Order=0)]
        [XmlElementAttribute("SvenskAdress", typeof(SvenskAdressTYPE), Namespace="http://statenspersonadressregister.se/schema/komponent/person/adress/deladeadresselement-1.1", Order=0)]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }

    [DebuggerStepThroughAttribute()]
    [XmlTypeAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/adress/folkbokforingsadress-1.1")]
    public partial class FolkbokforingsadressTYPE
    {
        
        private SvenskAdressTYPE svenskAdressField;
        
        [XmlElementAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/adress/deladeadresselement-1.1", Order=0)]
        public SvenskAdressTYPE SvenskAdress
        {
            get
            {
                return this.svenskAdressField;
            }
            set
            {
                this.svenskAdressField = value;
            }
        }
    }

    [DebuggerStepThroughAttribute()]
    [XmlTypeAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/folkbokforing-1.0")]
    public partial class FolkbokforingTYPE
    {
        
        private DateTime datumFromField;
        
        private DateTime datumTillField;
        
        private string folkbokfordLanKodField;
        
        private string folkbokfordKommunKodField;
        
        private HemvistTYPE hemvistField;
        
        private bool hemvistFieldSpecified;
        
        private DateTime folkbokforingsdatumField;
        
        private bool folkbokforingsdatumFieldSpecified;
        
        private string distriktKodField;
        
        [XmlElementAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/generellt/datumtid-1.1", DataType="date", Order=0)]
        public DateTime DatumFrom
        {
            get
            {
                return this.datumFromField;
            }
            set
            {
                this.datumFromField = value;
            }
        }
        
        [XmlElementAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/generellt/datumtid-1.1", DataType="date", Order=1)]
        public DateTime DatumTill
        {
            get
            {
                return this.datumTillField;
            }
            set
            {
                this.datumTillField = value;
            }
        }
        
        [XmlElementAttribute(Order=2)]
        public string FolkbokfordLanKod
        {
            get
            {
                return this.folkbokfordLanKodField;
            }
            set
            {
                this.folkbokfordLanKodField = value;
            }
        }
        
        [XmlElementAttribute(Order=3)]
        public string FolkbokfordKommunKod
        {
            get
            {
                return this.folkbokfordKommunKodField;
            }
            set
            {
                this.folkbokfordKommunKodField = value;
            }
        }
        
        [XmlElementAttribute(Order=4)]
        public HemvistTYPE Hemvist
        {
            get
            {
                return this.hemvistField;
            }
            set
            {
                this.hemvistField = value;
            }
        }
        
        [XmlIgnoreAttribute()]
        public bool HemvistSpecified
        {
            get
            {
                return this.hemvistFieldSpecified;
            }
            set
            {
                this.hemvistFieldSpecified = value;
            }
        }
        
        [XmlElementAttribute(DataType="date", Order=5)]
        public DateTime Folkbokforingsdatum
        {
            get
            {
                return this.folkbokforingsdatumField;
            }
            set
            {
                this.folkbokforingsdatumField = value;
            }
        }
        
        [XmlIgnoreAttribute()]
        public bool FolkbokforingsdatumSpecified
        {
            get
            {
                return this.folkbokforingsdatumFieldSpecified;
            }
            set
            {
                this.folkbokforingsdatumFieldSpecified = value;
            }
        }
        
        [XmlElementAttribute(Order=6)]
        public string DistriktKod
        {
            get
            {
                return this.distriktKodField;
            }
            set
            {
                this.distriktKodField = value;
            }
        }
    }

    [XmlTypeAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/folkbokforing-1.0")]
    public enum HemvistTYPE
    {
        
        [XmlEnumAttribute("Skriven på adressen")]
        Skrivenpåadressen,
        
        [XmlEnumAttribute("På kommunen skriven")]
        Påkommunenskriven,
        
        [XmlEnumAttribute("Utan känt hemvist")]
        Utankänthemvist,
    }

    [DebuggerStepThroughAttribute()]
    [XmlTypeAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/hanvisning-1.0")]
    public partial class HanvisningTYPE
    {
        
        private string idNummerField;
        
        private HanvisningTypTYPE typField;
        
        private bool typFieldSpecified;
        
        [XmlElementAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/person-1.2", Order=0)]
        public string IdNummer
        {
            get
            {
                return this.idNummerField;
            }
            set
            {
                this.idNummerField = value;
            }
        }
        
        [XmlElementAttribute(Order=1)]
        public HanvisningTypTYPE Typ
        {
            get
            {
                return this.typField;
            }
            set
            {
                this.typField = value;
            }
        }
        
        [XmlIgnoreAttribute()]
        public bool TypSpecified
        {
            get
            {
                return this.typFieldSpecified;
            }
            set
            {
                this.typFieldSpecified = value;
            }
        }
    }

    [XmlTypeAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/hanvisning-1.0")]
    public enum HanvisningTypTYPE
    {
        
        Till,
        
        Fran,
    }

    [DebuggerStepThroughAttribute()]
    [XmlTypeAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/persondetaljer-1.2")]
    public partial class PersondetaljerTYPE
    {
        
        private DateTime datumFromField;
        
        private DateTime datumTillField;
        
        private SekretessmarkeringMedAttributTYPE sekretessmarkeringField;
        
        private JaNejTYPE skyddadFolkbokforingField;
        
        private bool skyddadFolkbokforingFieldSpecified;
        
        private string avregistreringsorsakKodField;
        
        private string avregistreringsdatumField;
        
        private string avlidendatumField;
        
        private string antraffadDodDatumField;
        
        private DateTime fodelsedatumField;
        
        private bool fodelsedatumFieldSpecified;
        
        private string fodelselanKodField;
        
        private string fodelseforsamlingField;
        
        private KonTYPE konField;
        
        private bool konFieldSpecified;
        
        private JaNejTYPE svenskMedborgareField;
        
        private bool svenskMedborgareFieldSpecified;
        
        private HanvisningTYPE[] hanvisningField;
        
        private string snStatusField;
        
        private string snTilldelningsdatumField;
        
        private string snPreliminartVilandeforklaringsdatumField;
        
        private string snFornyelsedatumField;
        
        private string snVilandeorsakField;
        
        private string snVilandeforklaringsdatumField;
        
        private string snAvlidendatumField;
        
        [XmlElementAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/generellt/datumtid-1.1", DataType="date", Order=0)]
        public DateTime DatumFrom
        {
            get
            {
                return this.datumFromField;
            }
            set
            {
                this.datumFromField = value;
            }
        }
        
        [XmlElementAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/generellt/datumtid-1.1", DataType="date", Order=1)]
        public DateTime DatumTill
        {
            get
            {
                return this.datumTillField;
            }
            set
            {
                this.datumTillField = value;
            }
        }
        
        [XmlElementAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/skyddadepersonuppgifter-1.1", Order=2)]
        public SekretessmarkeringMedAttributTYPE Sekretessmarkering
        {
            get
            {
                return this.sekretessmarkeringField;
            }
            set
            {
                this.sekretessmarkeringField = value;
            }
        }
        
        [XmlElementAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/skyddadepersonuppgifter-1.1", Order=3)]
        public JaNejTYPE SkyddadFolkbokforing
        {
            get
            {
                return this.skyddadFolkbokforingField;
            }
            set
            {
                this.skyddadFolkbokforingField = value;
            }
        }
        
        [XmlIgnoreAttribute()]
        public bool SkyddadFolkbokforingSpecified
        {
            get
            {
                return this.skyddadFolkbokforingFieldSpecified;
            }
            set
            {
                this.skyddadFolkbokforingFieldSpecified = value;
            }
        }
        
        [XmlElementAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/avregistrering-1.0", Order=4)]
        public string AvregistreringsorsakKod
        {
            get
            {
                return this.avregistreringsorsakKodField;
            }
            set
            {
                this.avregistreringsorsakKodField = value;
            }
        }
        
        [XmlElementAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/avregistrering-1.0", Order=5)]
        public string Avregistreringsdatum
        {
            get
            {
                return this.avregistreringsdatumField;
            }
            set
            {
                this.avregistreringsdatumField = value;
            }
        }
        
        [XmlElementAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/avregistrering-1.0", Order=6)]
        public string Avlidendatum
        {
            get
            {
                return this.avlidendatumField;
            }
            set
            {
                this.avlidendatumField = value;
            }
        }
        
        [XmlElementAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/avregistrering-1.0", Order=7)]
        public string AntraffadDodDatum
        {
            get
            {
                return this.antraffadDodDatumField;
            }
            set
            {
                this.antraffadDodDatumField = value;
            }
        }
        
        [XmlElementAttribute(DataType="date", Order=8)]
        public DateTime Fodelsedatum
        {
            get
            {
                return this.fodelsedatumField;
            }
            set
            {
                this.fodelsedatumField = value;
            }
        }
        
        [XmlIgnoreAttribute()]
        public bool FodelsedatumSpecified
        {
            get
            {
                return this.fodelsedatumFieldSpecified;
            }
            set
            {
                this.fodelsedatumFieldSpecified = value;
            }
        }
        
        [XmlElementAttribute(Order=9)]
        public string FodelselanKod
        {
            get
            {
                return this.fodelselanKodField;
            }
            set
            {
                this.fodelselanKodField = value;
            }
        }
        
        [XmlElementAttribute(Order=10)]
        public string Fodelseforsamling
        {
            get
            {
                return this.fodelseforsamlingField;
            }
            set
            {
                this.fodelseforsamlingField = value;
            }
        }
        
        [XmlElementAttribute(Order=11)]
        public KonTYPE Kon
        {
            get
            {
                return this.konField;
            }
            set
            {
                this.konField = value;
            }
        }
        
        [XmlIgnoreAttribute()]
        public bool KonSpecified
        {
            get
            {
                return this.konFieldSpecified;
            }
            set
            {
                this.konFieldSpecified = value;
            }
        }
        
        [XmlElementAttribute(Order=12)]
        public JaNejTYPE SvenskMedborgare
        {
            get
            {
                return this.svenskMedborgareField;
            }
            set
            {
                this.svenskMedborgareField = value;
            }
        }
        
        [XmlIgnoreAttribute()]
        public bool SvenskMedborgareSpecified
        {
            get
            {
                return this.svenskMedborgareFieldSpecified;
            }
            set
            {
                this.svenskMedborgareFieldSpecified = value;
            }
        }
        
        [XmlElementAttribute("Hanvisning", Namespace="http://statenspersonadressregister.se/schema/komponent/person/hanvisning-1.0", Order=13)]
        public HanvisningTYPE[] Hanvisning
        {
            get
            {
                return this.hanvisningField;
            }
            set
            {
                this.hanvisningField = value;
            }
        }
        
        [XmlElementAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/samordningsnummer-1.0", Order=14)]
        public string SnStatus
        {
            get
            {
                return this.snStatusField;
            }
            set
            {
                this.snStatusField = value;
            }
        }
        
        [XmlElementAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/samordningsnummer-1.0", Order=15)]
        public string SnTilldelningsdatum
        {
            get
            {
                return this.snTilldelningsdatumField;
            }
            set
            {
                this.snTilldelningsdatumField = value;
            }
        }
        
        [XmlElementAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/samordningsnummer-1.0", Order=16)]
        public string SnPreliminartVilandeforklaringsdatum
        {
            get
            {
                return this.snPreliminartVilandeforklaringsdatumField;
            }
            set
            {
                this.snPreliminartVilandeforklaringsdatumField = value;
            }
        }
        
        [XmlElementAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/samordningsnummer-1.0", Order=17)]
        public string SnFornyelsedatum
        {
            get
            {
                return this.snFornyelsedatumField;
            }
            set
            {
                this.snFornyelsedatumField = value;
            }
        }
        
        [XmlElementAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/samordningsnummer-1.0", Order=18)]
        public string SnVilandeorsak
        {
            get
            {
                return this.snVilandeorsakField;
            }
            set
            {
                this.snVilandeorsakField = value;
            }
        }
        
        [XmlElementAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/samordningsnummer-1.0", Order=19)]
        public string SnVilandeforklaringsdatum
        {
            get
            {
                return this.snVilandeforklaringsdatumField;
            }
            set
            {
                this.snVilandeforklaringsdatumField = value;
            }
        }
        
        [XmlElementAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/samordningsnummer-1.0", Order=20)]
        public string SnAvlidendatum
        {
            get
            {
                return this.snAvlidendatumField;
            }
            set
            {
                this.snAvlidendatumField = value;
            }
        }
    }

    [DebuggerStepThroughAttribute()]
    [XmlTypeAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/skyddadepersonuppgifter-1.1")]
    public partial class SekretessmarkeringMedAttributTYPE
    {
        
        private SekretessSattAvSPARTYPE sattAvSPARField;
        
        private bool sattAvSPARFieldSpecified;
        
        private JaNejTYPE valueField;
        
        [XmlAttributeAttribute()]
        public SekretessSattAvSPARTYPE sattAvSPAR
        {
            get
            {
                return this.sattAvSPARField;
            }
            set
            {
                this.sattAvSPARField = value;
            }
        }
        
        [XmlIgnoreAttribute()]
        public bool sattAvSPARSpecified
        {
            get
            {
                return this.sattAvSPARFieldSpecified;
            }
            set
            {
                this.sattAvSPARFieldSpecified = value;
            }
        }
        
        [XmlTextAttribute()]
        public JaNejTYPE Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    [XmlTypeAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/skyddadepersonuppgifter-1.1")]
    public enum SekretessSattAvSPARTYPE
    {
        
        JA,
    }

    [XmlTypeAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/generellt/typ-1.0")]
    public enum JaNejTYPE
    {
        
        JA,
        
        NEJ,
    }

    [XmlTypeAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/generellt/typ-1.0")]
    public enum KonTYPE
    {
        
        MAN,
        
        KVINNA,
    }

    [DebuggerStepThroughAttribute()]
    [XmlTypeAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/namn/namn-1.0")]
    public partial class NamnTYPE
    {
        
        private DateTime datumFromField;
        
        private DateTime datumTillField;
        
        private string aviseringsnamnField;
        
        private string fornamnField;
        
        private int tilltalsnamnField;
        
        private bool tilltalsnamnFieldSpecified;
        
        private string mellannamnField;
        
        private string efternamnField;
        
        [XmlElementAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/generellt/datumtid-1.1", DataType="date", Order=0)]
        public DateTime DatumFrom
        {
            get
            {
                return this.datumFromField;
            }
            set
            {
                this.datumFromField = value;
            }
        }
        
        [XmlElementAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/generellt/datumtid-1.1", DataType="date", Order=1)]
        public DateTime DatumTill
        {
            get
            {
                return this.datumTillField;
            }
            set
            {
                this.datumTillField = value;
            }
        }
        
        [XmlElementAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/namn/deladenamnelement-1.0", Order=2)]
        public string Aviseringsnamn
        {
            get
            {
                return this.aviseringsnamnField;
            }
            set
            {
                this.aviseringsnamnField = value;
            }
        }
        
        [XmlElementAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/namn/deladenamnelement-1.0", Order=3)]
        public string Fornamn
        {
            get
            {
                return this.fornamnField;
            }
            set
            {
                this.fornamnField = value;
            }
        }
        
        [XmlElementAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/namn/deladenamnelement-1.0", Order=4)]
        public int Tilltalsnamn
        {
            get
            {
                return this.tilltalsnamnField;
            }
            set
            {
                this.tilltalsnamnField = value;
            }
        }
        
        [XmlIgnoreAttribute()]
        public bool TilltalsnamnSpecified
        {
            get
            {
                return this.tilltalsnamnFieldSpecified;
            }
            set
            {
                this.tilltalsnamnFieldSpecified = value;
            }
        }
        
        [XmlElementAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/namn/deladenamnelement-1.0", Order=5)]
        public string Mellannamn
        {
            get
            {
                return this.mellannamnField;
            }
            set
            {
                this.mellannamnField = value;
            }
        }
        
        [XmlElementAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/namn/deladenamnelement-1.0", Order=6)]
        public string Efternamn
        {
            get
            {
                return this.efternamnField;
            }
            set
            {
                this.efternamnField = value;
            }
        }
    }

    [DebuggerStepThroughAttribute()]
    [XmlTypeAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/person-1.2")]
    public partial class PersonIdTYPE
    {
        
        private string idNummerField;
        
        private string typField;
        
        [XmlElementAttribute(Order=0)]
        public string IdNummer
        {
            get
            {
                return this.idNummerField;
            }
            set
            {
                this.idNummerField = value;
            }
        }
        
        [XmlElementAttribute(Order=1)]
        public string Typ
        {
            get
            {
                return this.typField;
            }
            set
            {
                this.typField = value;
            }
        }
    }

    [DebuggerStepThroughAttribute()]
    [XmlTypeAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/aviseringpost-1.2")]
    public partial class AviseringPostTYPE
    {
        
        private PersonIdTYPE personIdField;
        
        private SekretessmarkeringMedAttributTYPE sekretessmarkeringField;
        
        private DateTime sekretessDatumField;
        
        private bool sekretessDatumFieldSpecified;
        
        private JaNejTYPE skyddadFolkbokforingField;
        
        private DateTime skyddadFolkbokforingDatumField;
        
        private bool skyddadFolkbokforingDatumFieldSpecified;
        
        private DateTime senasteAndringSPARField;
        
        private bool senasteAndringSPARFieldSpecified;
        
        private string summeradInkomstField;
        
        private string inkomstArField;
        
        private NamnTYPE[] namnField;
        
        private PersondetaljerTYPE[] persondetaljerField;
        
        private FolkbokforingTYPE[] folkbokforingField;
        
        private FolkbokforingsadressTYPE[] folkbokforingsadressField;
        
        private SarskildPostadressTYPE[] sarskildPostadressField;
        
        private UtlandsadressTYPE[] utlandsadressField;
        
        private KontaktadressTYPE[] kontaktadressField;
        
        private RelationTYPE[] relationField;
        
        private FastighetTYPE[] fastighetField;
        
        [XmlElementAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/person-1.2", Order=0)]
        public PersonIdTYPE PersonId
        {
            get
            {
                return this.personIdField;
            }
            set
            {
                this.personIdField = value;
            }
        }
        
        [XmlElementAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/skyddadepersonuppgifter-1.1", Order=1)]
        public SekretessmarkeringMedAttributTYPE Sekretessmarkering
        {
            get
            {
                return this.sekretessmarkeringField;
            }
            set
            {
                this.sekretessmarkeringField = value;
            }
        }
        
        [XmlElementAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/skyddadepersonuppgifter-1.1", DataType="date", Order=2)]
        public DateTime SekretessDatum
        {
            get
            {
                return this.sekretessDatumField;
            }
            set
            {
                this.sekretessDatumField = value;
            }
        }
        
        [XmlIgnoreAttribute()]
        public bool SekretessDatumSpecified
        {
            get
            {
                return this.sekretessDatumFieldSpecified;
            }
            set
            {
                this.sekretessDatumFieldSpecified = value;
            }
        }
        
        [XmlElementAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/skyddadepersonuppgifter-1.1", Order=3)]
        public JaNejTYPE SkyddadFolkbokforing
        {
            get
            {
                return this.skyddadFolkbokforingField;
            }
            set
            {
                this.skyddadFolkbokforingField = value;
            }
        }
        
        [XmlElementAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/skyddadepersonuppgifter-1.1", DataType="date", Order=4)]
        public DateTime SkyddadFolkbokforingDatum
        {
            get
            {
                return this.skyddadFolkbokforingDatumField;
            }
            set
            {
                this.skyddadFolkbokforingDatumField = value;
            }
        }
        
        [XmlIgnoreAttribute()]
        public bool SkyddadFolkbokforingDatumSpecified
        {
            get
            {
                return this.skyddadFolkbokforingDatumFieldSpecified;
            }
            set
            {
                this.skyddadFolkbokforingDatumFieldSpecified = value;
            }
        }
        
        [XmlElementAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/person-1.2", DataType="date", Order=5)]
        public DateTime SenasteAndringSPAR
        {
            get
            {
                return this.senasteAndringSPARField;
            }
            set
            {
                this.senasteAndringSPARField = value;
            }
        }
        
        [XmlIgnoreAttribute()]
        public bool SenasteAndringSPARSpecified
        {
            get
            {
                return this.senasteAndringSPARFieldSpecified;
            }
            set
            {
                this.senasteAndringSPARFieldSpecified = value;
            }
        }
        
        [XmlElementAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/inkomsttaxering-1.2", Order=6)]
        public string SummeradInkomst
        {
            get
            {
                return this.summeradInkomstField;
            }
            set
            {
                this.summeradInkomstField = value;
            }
        }
        
        [XmlElementAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/person/inkomsttaxering-1.2", Order=7)]
        public string InkomstAr
        {
            get
            {
                return this.inkomstArField;
            }
            set
            {
                this.inkomstArField = value;
            }
        }
        
        [XmlElementAttribute("Namn", Namespace="http://statenspersonadressregister.se/schema/komponent/person/namn/namn-1.0", Order=8)]
        public NamnTYPE[] Namn
        {
            get
            {
                return this.namnField;
            }
            set
            {
                this.namnField = value;
            }
        }
        
        [XmlElementAttribute("Persondetaljer", Namespace="http://statenspersonadressregister.se/schema/komponent/person/persondetaljer-1.2", Order=9)]
        public PersondetaljerTYPE[] Persondetaljer
        {
            get
            {
                return this.persondetaljerField;
            }
            set
            {
                this.persondetaljerField = value;
            }
        }
        
        [XmlElementAttribute("Folkbokforing", Namespace="http://statenspersonadressregister.se/schema/komponent/person/folkbokforing-1.0", Order=10)]
        public FolkbokforingTYPE[] Folkbokforing
        {
            get
            {
                return this.folkbokforingField;
            }
            set
            {
                this.folkbokforingField = value;
            }
        }
        
        [XmlElementAttribute("Folkbokforingsadress", Namespace="http://statenspersonadressregister.se/schema/komponent/person/adress/folkbokforingsadress-1.1", Order=11)]
        public FolkbokforingsadressTYPE[] Folkbokforingsadress
        {
            get
            {
                return this.folkbokforingsadressField;
            }
            set
            {
                this.folkbokforingsadressField = value;
            }
        }
        
        [XmlElementAttribute("SarskildPostadress", Namespace="http://statenspersonadressregister.se/schema/komponent/person/adress/sarskildpostadress-1.1", Order=12)]
        public SarskildPostadressTYPE[] SarskildPostadress
        {
            get
            {
                return this.sarskildPostadressField;
            }
            set
            {
                this.sarskildPostadressField = value;
            }
        }
        
        [XmlElementAttribute("Utlandsadress", Namespace="http://statenspersonadressregister.se/schema/komponent/person/adress/utlandsadress-1.1", Order=13)]
        public UtlandsadressTYPE[] Utlandsadress
        {
            get
            {
                return this.utlandsadressField;
            }
            set
            {
                this.utlandsadressField = value;
            }
        }
        
        [XmlElementAttribute("Kontaktadress", Namespace="http://statenspersonadressregister.se/schema/komponent/person/adress/kontaktadress-1.0", Order=14)]
        public KontaktadressTYPE[] Kontaktadress
        {
            get
            {
                return this.kontaktadressField;
            }
            set
            {
                this.kontaktadressField = value;
            }
        }
        
        [XmlElementAttribute("Relation", Namespace="http://statenspersonadressregister.se/schema/komponent/person/relation-1.2", Order=15)]
        public RelationTYPE[] Relation
        {
            get
            {
                return this.relationField;
            }
            set
            {
                this.relationField = value;
            }
        }
        
        [XmlElementAttribute("Fastighet", Namespace="http://statenspersonadressregister.se/schema/komponent/person/fastighetstaxering-1.2", Order=16)]
        public FastighetTYPE[] Fastighet
        {
            get
            {
                return this.fastighetField;
            }
            set
            {
                this.fastighetField = value;
            }
        }
    }

    [DebuggerStepThroughAttribute()]
    [XmlTypeAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/sok/personsokningsokparametrar-1.1")]
    public partial class PersonsokningFragaTYPE
    {
        
        private object[] itemsField;
        
        private ItemsChoiceType[] itemsElementNameField;
        
        [XmlElementAttribute("IdNummer", typeof(string), Namespace="http://statenspersonadressregister.se/schema/komponent/person/person-1.2", Order=0)]
        [XmlElementAttribute("DistriktKod", typeof(string), Namespace="http://statenspersonadressregister.se/schema/komponent/sok/sokargument-1.2", Order=0)]
        [XmlElementAttribute("DistriktKodFrom", typeof(string), Namespace="http://statenspersonadressregister.se/schema/komponent/sok/sokargument-1.2", Order=0)]
        [XmlElementAttribute("DistriktKodTom", typeof(string), Namespace="http://statenspersonadressregister.se/schema/komponent/sok/sokargument-1.2", Order=0)]
        [XmlElementAttribute("Fodelsedatum", typeof(DateTime), Namespace="http://statenspersonadressregister.se/schema/komponent/sok/sokargument-1.2", DataType="date", Order=0)]
        [XmlElementAttribute("FodelsedatumFrom", typeof(DateTime), Namespace="http://statenspersonadressregister.se/schema/komponent/sok/sokargument-1.2", DataType="date", Order=0)]
        [XmlElementAttribute("FodelsedatumTom", typeof(DateTime), Namespace="http://statenspersonadressregister.se/schema/komponent/sok/sokargument-1.2", DataType="date", Order=0)]
        [XmlElementAttribute("FonetiskSokning", typeof(JaNejTYPE), Namespace="http://statenspersonadressregister.se/schema/komponent/sok/sokargument-1.2", Order=0)]
        [XmlElementAttribute("FornamnSokArgument", typeof(string), Namespace="http://statenspersonadressregister.se/schema/komponent/sok/sokargument-1.2", Order=0)]
        [XmlElementAttribute("KommunKod", typeof(string), Namespace="http://statenspersonadressregister.se/schema/komponent/sok/sokargument-1.2", Order=0)]
        [XmlElementAttribute("Kon", typeof(KonTYPE), Namespace="http://statenspersonadressregister.se/schema/komponent/sok/sokargument-1.2", Order=0)]
        [XmlElementAttribute("LanKod", typeof(string), Namespace="http://statenspersonadressregister.se/schema/komponent/sok/sokargument-1.2", Order=0)]
        [XmlElementAttribute("MellanEfternamnSokArgument", typeof(string), Namespace="http://statenspersonadressregister.se/schema/komponent/sok/sokargument-1.2", Order=0)]
        [XmlElementAttribute("NamnSokArgument", typeof(string), Namespace="http://statenspersonadressregister.se/schema/komponent/sok/sokargument-1.2", Order=0)]
        [XmlElementAttribute("PostNr", typeof(string), Namespace="http://statenspersonadressregister.se/schema/komponent/sok/sokargument-1.2", Order=0)]
        [XmlElementAttribute("PostNrFrom", typeof(string), Namespace="http://statenspersonadressregister.se/schema/komponent/sok/sokargument-1.2", Order=0)]
        [XmlElementAttribute("PostNrTom", typeof(string), Namespace="http://statenspersonadressregister.se/schema/komponent/sok/sokargument-1.2", Order=0)]
        [XmlElementAttribute("PostortSokArgument", typeof(string), Namespace="http://statenspersonadressregister.se/schema/komponent/sok/sokargument-1.2", Order=0)]
        [XmlElementAttribute("UtdelningsadressSokArgument", typeof(string), Namespace="http://statenspersonadressregister.se/schema/komponent/sok/sokargument-1.2", Order=0)]
        [XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
        
        [XmlElementAttribute("ItemsElementName", Order=1)]
        [XmlIgnoreAttribute()]
        public ItemsChoiceType[] ItemsElementName
        {
            get
            {
                return this.itemsElementNameField;
            }
            set
            {
                this.itemsElementNameField = value;
            }
        }
    }

    [XmlTypeAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/sok/personsokningsokparametrar-1.1", IncludeInSchema=false)]
    public enum ItemsChoiceType
    {
        
        [XmlEnumAttribute("http://statenspersonadressregister.se/schema/komponent/person/person-1.2:IdNummer")]
        IdNummer,
        
        [XmlEnumAttribute("http://statenspersonadressregister.se/schema/komponent/sok/sokargument-1.2:DistriktKod")]
        DistriktKod,
        
        [XmlEnumAttribute("http://statenspersonadressregister.se/schema/komponent/sok/sokargument-1.2:DistriktKodFrom")]
        DistriktKodFrom,
        
        [XmlEnumAttribute("http://statenspersonadressregister.se/schema/komponent/sok/sokargument-1.2:DistriktKodTom")]
        DistriktKodTom,
        
        [XmlEnumAttribute("http://statenspersonadressregister.se/schema/komponent/sok/sokargument-1.2:Fodelsedatum")]
        Fodelsedatum,
        
        [XmlEnumAttribute("http://statenspersonadressregister.se/schema/komponent/sok/sokargument-1.2:FodelsedatumFrom")]
        FodelsedatumFrom,
        
        [XmlEnumAttribute("http://statenspersonadressregister.se/schema/komponent/sok/sokargument-1.2:FodelsedatumTom")]
        FodelsedatumTom,
        
        [XmlEnumAttribute("http://statenspersonadressregister.se/schema/komponent/sok/sokargument-1.2:FonetiskSokning")]
        FonetiskSokning,
        
        [XmlEnumAttribute("http://statenspersonadressregister.se/schema/komponent/sok/sokargument-1.2:FornamnSokArgument")]
        FornamnSokArgument,
        
        [XmlEnumAttribute("http://statenspersonadressregister.se/schema/komponent/sok/sokargument-1.2:KommunKod")]
        KommunKod,
        
        [XmlEnumAttribute("http://statenspersonadressregister.se/schema/komponent/sok/sokargument-1.2:Kon")]
        Kon,
        
        [XmlEnumAttribute("http://statenspersonadressregister.se/schema/komponent/sok/sokargument-1.2:LanKod")]
        LanKod,
        
        [XmlEnumAttribute("http://statenspersonadressregister.se/schema/komponent/sok/sokargument-1.2:MellanEfternamnSokArgument")]
        MellanEfternamnSokArgument,
        
        [XmlEnumAttribute("http://statenspersonadressregister.se/schema/komponent/sok/sokargument-1.2:NamnSokArgument")]
        NamnSokArgument,
        
        [XmlEnumAttribute("http://statenspersonadressregister.se/schema/komponent/sok/sokargument-1.2:PostNr")]
        PostNr,
        
        [XmlEnumAttribute("http://statenspersonadressregister.se/schema/komponent/sok/sokargument-1.2:PostNrFrom")]
        PostNrFrom,
        
        [XmlEnumAttribute("http://statenspersonadressregister.se/schema/komponent/sok/sokargument-1.2:PostNrTom")]
        PostNrTom,
        
        [XmlEnumAttribute("http://statenspersonadressregister.se/schema/komponent/sok/sokargument-1.2:PostortSokArgument")]
        PostortSokArgument,
        
        [XmlEnumAttribute("http://statenspersonadressregister.se/schema/komponent/sok/sokargument-1.2:UtdelningsadressSokArgument")]
        UtdelningsadressSokArgument,
    }

    [DebuggerStepThroughAttribute()]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://statenspersonadressregister.se/schema/personsok/2021.1/personsokningsvar")]
    public partial class SPARPersonsokningSvar
    {
        
        private PersonsokningFragaTYPE personsokningFragaField;
        
        private object[] itemsField;
        
        private string uUIDField;
        
        [XmlElementAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/sok/personsokningsokparametrar-1.1", Order=0)]
        public PersonsokningFragaTYPE PersonsokningFraga
        {
            get
            {
                return this.personsokningFragaField;
            }
            set
            {
                this.personsokningFragaField = value;
            }
        }
        
        [XmlElementAttribute("OverstigerMaxAntalSvarsposter", typeof(OverstigerMaxAntalSvarsposterTYPE), Namespace="http://statenspersonadressregister.se/schema/komponent/sok/undantag-1.0", Order=1)]
        [XmlElementAttribute("Undantag", typeof(UndantagTYPE), Namespace="http://statenspersonadressregister.se/schema/komponent/sok/undantag-1.0", Order=1)]
        [XmlElementAttribute("PersonsokningSvarspost", typeof(AviseringPostTYPE), Order=1)]
        public object[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
        
        [XmlElementAttribute(Order=2)]
        public string UUID
        {
            get
            {
                return this.uUIDField;
            }
            set
            {
                this.uUIDField = value;
            }
        }
    }

    //[EditorBrowsableAttribute(EditorBrowsableState.Advanced)]
    [DebuggerStepThroughAttribute()]
    [MessageContractAttribute(
			WrapperName="SPARPersonsokningFraga", 
			WrapperNamespace="http://statenspersonadressregister.se/schema/personsok/2021.1/personsokningfraga",
			IsWrapped=true)]
    public partial class PersonSokRequest
    {
        
        [MessageBodyMemberAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/metadata/identifieringsinformationWs-1.1", Order=0)]
        [XmlElementAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/metadata/identifieringsinformationWs-1.1")]
        public IdentifieringsinformationTYPE Identifieringsinformation;

        [MessageBodyMemberAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/sok/personsokningsokparametrar-1.1", Order=1)]
        [XmlElementAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/sok/personsokningsokparametrar-1.1")]
        public PersonsokningFragaTYPE PersonsokningFraga;
        
        
        
        public PersonSokRequest()
        {
        }
        
        public PersonSokRequest(
                IdentifieringsinformationTYPE Identifieringsinformation,
                PersonsokningFragaTYPE PersonsokningFraga)
        {
            this.Identifieringsinformation = Identifieringsinformation;
            this.PersonsokningFraga = PersonsokningFraga;
        }
    }

    [DebuggerStepThroughAttribute()]
    [MessageContractAttribute(
			WrapperName="SPARPersonsokningSvar", 
			WrapperNamespace="http://statenspersonadressregister.se/schema/personsok/2021.1/personsokningsvar",
			IsWrapped=true)]
    public partial class PersonSokResponse
    {
        [MessageBodyMemberAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/sok/personsokningsokparametrar-1.1", Order=0)]
        [XmlElementAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/sok/personsokningsokparametrar-1.1")]
        public PersonsokningFragaTYPE PersonsokningFraga;

        [MessageBodyMemberAttribute(Namespace="http://statenspersonadressregister.se/schema/komponent/sok/personsokningsvar-1.1", Order=1)]
        [XmlElementAttribute("OverstigerMaxAntalSvarsposter", typeof(OverstigerMaxAntalSvarsposterTYPE), Namespace="http://statenspersonadressregister.se/schema/komponent/sok/undantag-1.0")]
        [XmlElementAttribute("Undantag", typeof(UndantagTYPE), Namespace="http://statenspersonadressregister.se/schema/komponent/sok/undantag-1.0")]
        [XmlElementAttribute("PersonsokningSvarspost", typeof(AviseringPostTYPE))]
        [XmlElementAttribute("UUID", typeof(string))]
        public object[] Items;

        public PersonSokResponse()
        {
        }   
    }

    public interface PersonsokServiceChannel : PersonsokService, IClientChannel
    {
    }

    [DebuggerStepThroughAttribute()]
    public partial class PersonsokServiceClient : ClientBase<PersonsokService>, PersonsokService
    {
        
        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(ServiceEndpoint serviceEndpoint, ClientCredentials clientCredentials);
        
        public PersonsokServiceClient() : 
                base(PersonsokServiceClient.GetDefaultBinding(), PersonsokServiceClient.GetDefaultEndpointAddress())
        {
            this.Endpoint.Name = EndpointConfiguration.PersonsokServiceSOAP.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public PersonsokServiceClient(EndpointConfiguration endpointConfiguration) : 
                base(PersonsokServiceClient.GetBindingForEndpoint(endpointConfiguration), PersonsokServiceClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public PersonsokServiceClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(PersonsokServiceClient.GetBindingForEndpoint(endpointConfiguration), new EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public PersonsokServiceClient(EndpointConfiguration endpointConfiguration, EndpointAddress remoteAddress) : 
                base(PersonsokServiceClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public PersonsokServiceClient(Binding binding, EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        [EditorBrowsableAttribute(EditorBrowsableState.Advanced)]
        Task<PersonSokResponse> PersonsokService.PersonSokAsync(PersonSokRequest request)
        {
            return base.Channel.PersonSokAsync(request);
        }
    
        [EditorBrowsableAttribute(EditorBrowsableState.Advanced)]
        PersonSokResponse PersonsokService.PersonSok(PersonSokRequest request)
        {
            return base.Channel.PersonSok(request);
        }
        
        public PersonSokResponse PersonSok(PersonSokRequest request)
        {
            return base.Channel.PersonSok(request);
        }
        
        public virtual Task OpenAsync()
        {
            return Task.Factory.FromAsync(((ICommunicationObject)(this)).BeginOpen(null, null), new Action<IAsyncResult>(((ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual Task CloseAsync()
        {
            return Task.Factory.FromAsync(((ICommunicationObject)(this)).BeginClose(null, null), new Action<IAsyncResult>(((ICommunicationObject)(this)).EndClose));
        }
        
        private static Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.PersonsokServiceSOAP))
            {
                BasicHttpBinding result = new BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            throw new InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.PersonsokServiceSOAP))
            {
                return new EndpointAddress("http://localhost:8101/");
            }
            throw new InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static Binding GetDefaultBinding()
        {
            return PersonsokServiceClient.GetBindingForEndpoint(EndpointConfiguration.PersonsokServiceSOAP);
        }
        
        private static EndpointAddress GetDefaultEndpointAddress()
        {
            return PersonsokServiceClient.GetEndpointAddress(EndpointConfiguration.PersonsokServiceSOAP);
        }
        
        public enum EndpointConfiguration
        {
            
            PersonsokServiceSOAP,
        }
    }
}
