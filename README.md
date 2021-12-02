# Referensimplementation SPAR Personsök program-program

Denna källkod är en referensimplementation av SPAR Personsök program-program version _2021.1_.

Referensimplementationen är skriven för _.NET Core 5.0_ och använder _nuget_ för pakethantering.

Med hjälp av verktyget _svcutil_ har det skapats en tjänst från wsdl-filen som används för att anropa personsöktjänsten.
Det går också bra att skapa upp tjänsten via _Visual Studio_ och menyalternativet '_Add Service Reference..._'.
Utöver det används _NLog_ för att hantera loggningen och _NUnit_ för att hantera enhetstestningen av koden.

För mer information om SPAR på såväl verksamhets- som teknisk nivå se [SPAR:s hemsida](https://www.statenspersonadressregister.se).

## Användning

När projektet byggs hämtar _nuget_ externa beroenden så att koden kan kompilera.

_Personsok_ innehåller en demonstration som gör fem olika sökningar mot kundtestmiljön och loggar utförligt ut resultatet.
_PersonsokTest_ har sex tester som kör mot kundtestmiljön. Dessa verifierar att inget går fel. 

För att köra projektet i Visual Studio, högerklicka på projektet _PersonsokImplementation_ och kör, alternativt debug.
Om du alternativt använder Visual Studio Code så exekverar du följande kommando för att köra projektet:

```sh
dotnet run -p PersonsokImplementation
```

eller följande kommando för att specifikt köra testerna

```sh
dotnet test
```

### Kundtest

Vi rekommenderar att det organisationscertifikat som är tänkt att användas i produktion även används vid tester mot kundtestmiljön,
detta för att i ett tidigt skede verifiera att certifikatet är korrekt.

### Organisationscertifikat

För att använda eget organisationscertifikat, byt ut sökväg och lösenord till certifikat i anropet
'_client.ClientCredentials.ClientCertificate.Certificate = ..._' i funktionen _CreatePersonsokServiceClient_.

### Rootcertifikat

För att verifiera att det är rätt utställare av certifikat hos SPAR så används _X509Certificate2 signerandeCertifikat_
i funktionen _CreatePersonsokServiceClient_. Den använder även en _CustomCertificateValidator_, _SPARCertificateValidator_
som gör extra verifiering av SPARs certifikat.

Vi rekommenderar att verifiering av rootcertifikatet görs även om en annan lösning används.

### Produktion

Om koden används för att integrera mot produktionsmiljön krävs ett giltigt organisationscertifikat, det inkluderade
testcertifikatet fungerar endast i kundtestmiljön. Även indentifieringsinformation behöver vara giltig,
se _KundNrLeveransMottagare_, _KundNrSlutkund_ och _UppdragsId_. För mer information kontakta SPAR:s kundtjänst.
