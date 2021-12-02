using System;
using System.Xml;
using System.Xml.Schema;
using System.Reflection;
using System.IO;
using Microsoft.Extensions.FileProviders;
using System.Collections.Generic;
using System.Linq;

namespace PersonsokImplementation
{
    /// <summary>
    /// En validatorklass för att validera personnummer, samt för att validera om ett requestmeddelande
    /// följer reglerna angivet i XML-schemat för SPAR Personsök 
    /// </summary>
    public static class PersonsokValidator 
    {
        private static PersonsokLogger Logger = PersonsokLogger.CreatePersonsokLogger();

        /// <summary>
        /// Kontrollerar ifall det angivna personnumret är giltigt.
        /// </summary>
        /// <returns>bool</returns>
        public static bool IsPersonIdValid(string personId)
        {
            if(personId.Length != 12)
                throw new ArgumentException("PersonId måste vara 12 siffror");
            if(!long.TryParse(personId, out long result))
                throw new ArgumentException("PersonId får endast bestå utav siffror");

            return IsPersonIdChecksumValid(personId.Substring(2));
        }

        /// <summary>
        /// Kontrollerar ifall det angivna personnumret har giltiga kontrollsiffror.
        /// </summary>
        /// <returns>bool</returns>
        private static bool IsPersonIdChecksumValid(string personId)
        {
            int lastDigit = int.Parse(personId.Substring(personId.Length - 1, 1));
            int tal = 0;
            int vikt = 0;
            int sum = 0;

            for (int i = personId.Length - 2; i >= 0; i--) 
            {
                vikt = vikt == 2 ? 1 : 2;
                tal = int.Parse(personId[i].ToString()) * vikt;
                sum += (tal / 10) + (tal % 10);
            }

            sum = 10 - (sum % 10);
            sum = sum == 10 ? 0 : sum;

            return lastDigit == sum;
        }

        /// <summary>
        /// Validerar requestmeddelandet mot xml-schemat.
        /// </summary>
        /// <returns>bool</returns>
        public static bool ValidateXml(XmlReader message)
        {
            try
            {
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.ValidationType = ValidationType.Schema;
                
                List<String> xsdFiles = new List<String>
                {
                    "DatumTid-1.1.xsd",
                    "DeladeMetaElement-1.0.xsd",
                    "IdentifieringsinformationWs-1.1.xsd",
                    "Person-1.2.xsd",
                    "PersonsokningFraga.xsd",
                    "PersonsokningSokparametrar-1.1.xsd",
                    "Sokargument-1.2.xsd",
                    "Typ-1.0.xsd",
                };
                
                foreach (var xsdName in xsdFiles)
                {                    
                    settings.Schemas.Add(loadSchemaFromAssembly(xsdName));
                }
                               
                XmlReader reader = XmlReader.Create(message, settings).ReadSubtree();
                XmlDocument document = new XmlDocument();
                document.Load(reader);
                
                ValidationEventHandler eventHandler = new ValidationEventHandler(ValidationEventHandler);
                document.Validate(eventHandler);
            }
            catch(XmlSchemaValidationException ex) 
            {
                Logger.LogError("Fel vid valideringen av request: " + ex.Message);
                return false;
            }
            catch(XmlSchemaException ex)
            {
                Logger.LogError("Fel vid validaeringen av request: " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Logger.LogError("Oförväntad fel vid valideringen av request: " + ex.Message);
                return false;
            }            

            return true;
        }


        /// <summary>
        /// Tar en xsd-fil från Assemly och skapar en instans av XmlSchema med denna
        /// </summary>
        private static XmlSchema loadSchemaFromAssembly(string Filename) 
        {
            var embeddedProvider = new EmbeddedFileProvider(Assembly.GetExecutingAssembly());
               
            using (var reader = embeddedProvider.GetFileInfo(Filename).CreateReadStream())        
            {        
                try 
                {            
                    return XmlSchema.Read(reader, null);                    
                }
                catch (XmlSchemaException ex) {
                    return null;  
                }
            }
        }

        /// <summary>
        /// Loggar felmeddelanden vid uppkomst av valideringsfel med requestmeddelandet.
        /// </summary>
        /// <returns></returns>
        public static void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            switch (e.Severity)
            {
                case XmlSeverityType.Error:
                    Logger.LogError(e.Message);
                    break;
                case XmlSeverityType.Warning:
                    Logger.LogWarning(e.Message);
                    break;
            }
        }
    }
}