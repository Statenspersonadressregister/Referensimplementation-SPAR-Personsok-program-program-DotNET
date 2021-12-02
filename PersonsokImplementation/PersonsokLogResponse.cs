using ServiceReference;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace PersonsokImplementation
{
    public partial class Personsok
    {
        /// <summary>
        /// Logga personsökningssvaret som tas emot från SPAR-tjänsten
        /// Loggningen visar även hur man kan ta ut information från responsen
        /// </summary>
        /// <param name="response">Responsemeddelandet, innehåller svaret från anropet</param>
        /// <returns></returns>
        private static void LogPersonsokningResponse(PersonSokResponse response)
        {
            List<object> svarsposter = response.Items.ToList();
            List<AviseringPostTYPE> aviseringsposter = svarsposter
                .FindAll(a => a.GetType() == typeof(AviseringPostTYPE))
                .Select(a => (AviseringPostTYPE)a)
                .ToList();
            List<UndantagTYPE> undantagsposter = svarsposter
                .FindAll(u => u.GetType() == typeof(UndantagTYPE))
                .Select(u => (UndantagTYPE)u)
                .ToList();
            List<OverstigerMaxAntalSvarsposterTYPE> maxantalsposter = svarsposter
                .FindAll(m => m.GetType() == typeof(OverstigerMaxAntalSvarsposterTYPE))
                .Select(m => (OverstigerMaxAntalSvarsposterTYPE)m)
                .ToList();
            List<string> uuidList = svarsposter
                .FindAll(m => m.GetType() == typeof(string))
                .Select(m => (string)m)
                .ToList();
            string uuid = uuidList == null || !uuidList.Any() ? "<not set>" : uuidList[0];

            int antalAviseringsposter = aviseringsposter == null ? 0 : aviseringsposter.Count();
            int antalUndantagsposter = undantagsposter == null ? 0 : undantagsposter.Count();
            int antalMaxantalsposter = maxantalsposter == null ? 0 : maxantalsposter.Count();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Personsökningen gav " + antalAviseringsposter + " sökträff(ar)");
            sb.AppendLine("UUID: " + uuid);

            sb = antalAviseringsposter <= 0 ? sb : LogAviseringsPoster(sb, aviseringsposter);
            sb = antalUndantagsposter <= 0 ? sb : LogUndantagsPoster(sb, undantagsposter);
            sb = antalMaxantalsposter <= 0 ? sb : LogMaxantalsPoster(sb, maxantalsposter);

            Logger.LogInformation(sb.ToString());
        }

        private static StringBuilder LogAviseringsPoster(StringBuilder sb, List<AviseringPostTYPE> aviseringsposter)
        {
            foreach (AviseringPostTYPE aviseringPost in aviseringsposter)
            {
                sb.AppendLine("--------------------------------");
                sb.AppendLine("PersonId: " + aviseringPost.PersonId.IdNummer);
                sb.AppendLine("PersonTyp: " + aviseringPost.PersonId.Typ);

                sb.AppendLine("Sekretessmarkering: " + aviseringPost.Sekretessmarkering.Value.ToString());
                if (aviseringPost.Sekretessmarkering.sattAvSPARSpecified)
                    sb.AppendLine("Sekretess satt av SPAR: " + aviseringPost.Sekretessmarkering.sattAvSPAR.ToString());
                if (aviseringPost.SekretessDatumSpecified)
                    sb.AppendLine("Sekretess datum: " + aviseringPost.SekretessDatum.ToString("yyyy-MM-dd"));

                sb.AppendLine("Skyddad Folkbokföring: " + aviseringPost.SkyddadFolkbokforing.ToString());
                if (aviseringPost.SkyddadFolkbokforingDatumSpecified)
                    sb.AppendLine("Skyddad folkbokföring datum: " + aviseringPost.SkyddadFolkbokforingDatum.ToString("yyyy-MM-dd"));

                sb.AppendLine("Summerad inkomst: " + aviseringPost.SummeradInkomst);
                sb.AppendLine("Inkomstår: " + aviseringPost.InkomstAr);

                LoggaNamn(sb, aviseringPost);
                LogAllaAdresser(sb, aviseringPost);

                LoggaPersondetaljer(sb, aviseringPost);
                LoggaFolkbokforing(sb, aviseringPost);
                LoggaRelationer(sb, aviseringPost);
                LoggaFastighet(sb, aviseringPost);

                sb.AppendLine();
            }

            return sb;
        }

        private static void LoggaNamn(StringBuilder sb, AviseringPostTYPE aviseringPost)
        {
            if (aviseringPost.Namn != null)
                foreach (NamnTYPE namn in aviseringPost.Namn)
                {
                    sb.AppendLine();
                    sb.AppendLine("Namn:");
                    sb.AppendLine("DatumFrom: " + namn.DatumFrom.ToString("yyyy-MM-dd"));
                    sb.AppendLine("DatumTill: " + namn.DatumTill.ToString("yyyy-MM-dd"));
                   
                    sb.AppendLine("Aviseringsnamn: " + namn.Aviseringsnamn);
                    sb.AppendLine("Förnamn: " + namn.Fornamn);
                    sb.AppendLine("Mellannamn: " + namn.Mellannamn);
                    sb.AppendLine("Efternamn: " + namn.Efternamn);
                    sb.AppendLine("Tilltalsnamn: " + namn.Tilltalsnamn);
                }
        }

        private static void LoggaPersondetaljer(StringBuilder sb, AviseringPostTYPE aviseringPost)
        {
            if (aviseringPost.Persondetaljer != null)
                foreach (PersondetaljerTYPE detalj in aviseringPost.Persondetaljer)
                {
                    sb.AppendLine();
                    sb.AppendLine("Persondetalj:");
                    sb.AppendLine("DatumFrom: " + detalj.DatumFrom.ToString("yyyy-MM-dd"));
                    sb.AppendLine("DatumTill: " + detalj.DatumTill.ToString("yyyy-MM-dd"));

                    sb.AppendLine("Sekretessmakering: " + detalj.Sekretessmarkering.Value);
                    if (detalj.Sekretessmarkering.sattAvSPARSpecified)
                        sb.AppendLine("Sekretessmarkering satt av SPAR: " + detalj.Sekretessmarkering.sattAvSPAR);
                    sb.AppendLine("Skyddad folkbokföring: " + detalj.SkyddadFolkbokforing);

                    sb.AppendLine("Avregistreringsorsak kod: " + detalj.AvregistreringsorsakKod);
                    sb.AppendLine("Avregistreringsdatum: " + detalj.Avregistreringsdatum);
                    sb.AppendLine("Avlidendatum: " + detalj.Avlidendatum);
                    sb.AppendLine("Anträffad död datum: " + detalj.AntraffadDodDatum);

                    if (detalj.FodelsedatumSpecified)
                        sb.AppendLine("Födelsedatum: " + detalj.Fodelsedatum.ToString("yyyy-MM-dd"));
                    sb.AppendLine("Födelselän kod: " + detalj.FodelselanKod);
                    sb.AppendLine("Födelseförsamling: " + detalj.Fodelseforsamling);
                    if (detalj.KonSpecified)
                        sb.AppendLine("Kön: " + detalj.Kon.ToString());

                    if (detalj.SvenskMedborgareSpecified)
                        sb.AppendLine("Svensk medborgare: " + detalj.SvenskMedborgareSpecified);

                    sb.AppendLine("Samordningsnummer status: " + detalj.SnStatus);
                    sb.AppendLine("Samordningsnummer tilldelningsdatum: " + detalj.SnTilldelningsdatum);
                    sb.AppendLine("Samordningsnummer preliminärt vilandeförklaringsdatum: " + detalj.SnPreliminartVilandeforklaringsdatum);
                    sb.AppendLine("Samordningsnummer förnyelsedatum: " + detalj.SnFornyelsedatum);
                    sb.AppendLine("Samordningsnummer vilandeorsak: " + detalj.SnVilandeorsak);
                    sb.AppendLine("Samordningsnummer vilandeförklaringsdatum: " + detalj.SnVilandeforklaringsdatum);
                    sb.AppendLine("Samordningsnummer avlidendatum: " + detalj.SnAvlidendatum);

                    if (detalj.Hanvisning != null)
                        foreach (HanvisningTYPE hanvisning in detalj.Hanvisning)
                        {
                            sb.AppendLine("Hänvisning:");
                            sb.AppendLine("  Id nummer: " + hanvisning.IdNummer);
                            if (hanvisning.TypSpecified)
                                sb.AppendLine("  Typ: " + hanvisning.Typ);
                        }
                }
        }

        private static void LoggaFolkbokforing(StringBuilder sb, AviseringPostTYPE aviseringPost)
        {
            if (aviseringPost.Folkbokforing != null)
                foreach (FolkbokforingTYPE folkbokforing in aviseringPost.Folkbokforing)
                {
                    sb.AppendLine();
                    sb.AppendLine("Folkbokföring:");
                    sb.AppendLine("DatumFrom: " + folkbokforing.DatumFrom.ToString("yyyy-MM-dd"));
                    sb.AppendLine("DatumTill: " + folkbokforing.DatumTill.ToString("yyyy-MM-dd"));

                    sb.AppendLine("Folkbokförd län kod: " + folkbokforing.FolkbokfordLanKod);
                    sb.AppendLine("Folkbokförd kommun kod: " + folkbokforing.FolkbokfordKommunKod);
                    if (folkbokforing.HemvistSpecified)
                        sb.AppendLine("Hemvist: " + folkbokforing.Hemvist);
                    if (folkbokforing.FolkbokforingsdatumSpecified)
                        sb.AppendLine("Folkbokföringsdatum: " + folkbokforing.Folkbokforingsdatum.ToString("yyyy-MM-dd"));
                    sb.AppendLine("Distrikt kod: " + folkbokforing.DistriktKod);
                }
        }

        private static void LoggaRelationer(StringBuilder sb, AviseringPostTYPE aviseringPost)
        {
            if (aviseringPost.Relation != null)
                foreach (RelationTYPE relation in aviseringPost.Relation)
                {
                    sb.AppendLine();
                    sb.AppendLine("Relation:");
                    sb.AppendLine("DatumFrom: " + relation.DatumFrom.ToString("yyyy-MM-dd"));
                    sb.AppendLine("DatumTill: " + relation.DatumTill.ToString("yyyy-MM-dd"));

                    sb.AppendLine("Typ: " + relation.Relationstyp);
                    sb.AppendLine("Id nummer: " + relation.IdNummer);

                    sb.AppendLine("Förnamn: " + relation.Fornamn);
                    sb.AppendLine("Mellannamn: " + relation.Mellannamn);
                    sb.AppendLine("Efternamn:" + relation.Efternamn);

                    if (relation.FodelsetidSpecified)
                        sb.AppendLine("Födelsetid: " + relation.Fodelsetid.ToString("yyyy-MM-dd"));

                    sb.AppendLine("Avregistreringsorsak kod: " + relation.AvregistreringsorsakKod);
                    sb.AppendLine("Avregistreringsorsakdatum: " + relation.Avregistreringsdatum);
                    sb.AppendLine("Avlidendatum: " + relation.Avlidendatum);

                }
        }

        private static void LoggaFastighet(StringBuilder sb, AviseringPostTYPE aviseringPost)
        {
            if (aviseringPost.Fastighet != null)
                foreach (FastighetTYPE fastighet in aviseringPost.Fastighet)
                {
                    sb.AppendLine();
                    sb.AppendLine("Fastighet:");
                    sb.AppendLine("Fastighet kod: " + fastighet.FastighetKod);
                    sb.AppendLine("Taxeringsenhetsnummer: " + fastighet.Taxeringsenhetsnummer);
                    sb.AppendLine("Taxeringsår: " + fastighet.Taxeringsar);
                    sb.AppendLine("Taxeringsvärde: " + fastighet.Taxeringsvarde);
                    sb.AppendLine("Län kod: " + fastighet.LanKod);
                    sb.AppendLine("Kommun kod: " + fastighet.KommunKod);

                    if (fastighet.FastighetDel != null)
                        foreach (FastighetDelTYPE fastighetDel in fastighet.FastighetDel)
                        {
                            sb.AppendLine("Fastighet del:");
                            sb.AppendLine("Taxeringsidentitet: " + fastighetDel.Taxeringsidentitet);
                            sb.AppendLine("Fastighet beteckning: " + fastighetDel.FastighetBeteckning);
                            sb.AppendLine("Andelstal Täljare: " + fastighetDel.AndelstalTaljare);
                            sb.AppendLine("Andelstal Nämnare: " + fastighetDel.AndelstalNamnare);
                        }
                }
        }

        private static void LogAllaAdresser(StringBuilder sb, AviseringPostTYPE aviseringPost)
        {
            if (aviseringPost.Folkbokforingsadress != null)
                foreach (FolkbokforingsadressTYPE folkbokforingsadress in aviseringPost.Folkbokforingsadress)
                {
                    sb.AppendLine();
                    sb.AppendLine("Folkbokföringsadress:");
                    LogAdress(sb, folkbokforingsadress.SvenskAdress);
                }


            if (aviseringPost.SarskildPostadress != null)
                foreach (SarskildPostadressTYPE sarskildPostadress in aviseringPost.SarskildPostadress)
                {
                    sb.AppendLine();
                    sb.AppendLine("Särskild postadress:");
                    LogAdress(sb, sarskildPostadress.Item);
                }

            if (aviseringPost.Kontaktadress != null)
                foreach (KontaktadressTYPE kontaktadress in aviseringPost.Kontaktadress)
                {
                    sb.AppendLine();
                    sb.AppendLine("Kontaktadress:");
                    LogAdress(sb, kontaktadress.Item);
                }

            if (aviseringPost.Utlandsadress != null)
                foreach (UtlandsadressTYPE utlandsadress in aviseringPost.Utlandsadress)
                {
                    sb.AppendLine();
                    sb.AppendLine("Utlandsadress:");
                    LogAdress(sb, utlandsadress.InternationellAdress);
                }
        }

        private static void LogAdress(StringBuilder sb, object adress)
        {
            if (adress.GetType() == typeof(SvenskAdressTYPE))
                LogSvenskAdress(sb, (SvenskAdressTYPE)adress);
            else if (adress.GetType() == typeof(InternationellAdressTYPE))
                LogInternationellAdress(sb, (InternationellAdressTYPE)adress);
        }

        private static void LogSvenskAdress(StringBuilder sb, SvenskAdressTYPE svenskAdress)
        {
            sb.AppendLine("Svensk adress:");
            sb.AppendLine("DatumFrom: " + svenskAdress.DatumFrom.ToString("yyyy-MM-dd"));
            sb.AppendLine("DatumTill: " + svenskAdress.DatumTill.ToString("yyyy-MM-dd"));
            sb.AppendLine("CareOf: " + svenskAdress.CareOf);
            sb.AppendLine("Utdelningsadress1: " + svenskAdress.Utdelningsadress1);
            sb.AppendLine("Utdelningsadress2: " + svenskAdress.Utdelningsadress2);
            sb.AppendLine("PostNr: " + svenskAdress.PostNr);
            sb.AppendLine("Postort: " + svenskAdress.Postort);
        }

        private static void LogInternationellAdress(StringBuilder sb, InternationellAdressTYPE internationellAdress)
        {
            sb.AppendLine("Internationell adress:");
            sb.AppendLine("DatumFrom: " + internationellAdress.DatumFrom.ToString("yyyy-MM-dd"));
            sb.AppendLine("DatumTill: " + internationellAdress.DatumTill.ToString("yyyy-MM-dd"));
            sb.AppendLine("Utdelningsadress1: " + internationellAdress.Utdelningsadress1);
            sb.AppendLine("Utdelningsadress2: " + internationellAdress.Utdelningsadress2);
            sb.AppendLine("Utdelningsadress3: " + internationellAdress.Utdelningsadress3);
            sb.AppendLine("Land: " + internationellAdress.Land);
        }

        private static StringBuilder LogUndantagsPoster(StringBuilder sb, List<UndantagTYPE> undantagsposter)
        {
            foreach (UndantagTYPE undantagspost in undantagsposter)
            {
                sb.AppendLine("Kod: " + undantagspost.Kod);
                sb.AppendLine("Beskrivning: " + undantagspost.Beskrivning);
            }

            return sb;
        }

        private static StringBuilder LogMaxantalsPoster(StringBuilder sb, List<OverstigerMaxAntalSvarsposterTYPE> maxantalsposter)
        {
            foreach (OverstigerMaxAntalSvarsposterTYPE maxantalspost in maxantalsposter)
            {
                sb.AppendLine("Antal poster:" + maxantalspost.AntalPoster);
                sb.AppendLine("Max antal poster: " + maxantalspost.MaxAntalSvarsposter);
            }

            return sb;
        }
    }
}
