using System;
using System.Collections.Generic;

namespace Funda.Infrastructure.Http.Models
{
    public class Project
    {
        public int? AantalKamersTotEnMet { get; set; }
        public int? AantalKamersVan { get; set; }
        public int? AantalKavels { get; set; }
        public string Adres { get; set; }
        public string FriendlyUrl { get; set; }
        public DateTime? GewijzigdDatum { get; set; }
        public string GlobalId { get; set; }
        public string HoofdFoto { get; set; }
        public bool IndIpix { get; set; }
        public bool IndPDF { get; set; }
        public bool IndPlattegrond { get; set; }
        public bool IndTop { get; set; }
        public bool IndVideo { get; set; }
        public string InternalId { get; set; }
        public string MaxWoonoppervlakte { get; set; }
        public string MinWoonoppervlakte { get; set; }
        public string Naam { get; set; }
        public string Omschrijving { get; set; }
        public List<object> OpenHuizen { get; set; }
        public string Plaats { get; set; }
        public string Prijs { get; set; }
        public string PrijsGeformatteerd { get; set; }
        public DateTime? PublicatieDatum { get; set; }
        public int Type { get; set; }
        public string Woningtypen { get; set; }
    }

}
