namespace Funda.Synchronizer.DomainModels
{
    public class Object
    {
        public string Id { get; set; }
        public string Adres { get; set; }
        public string BronCode { get; set; }
        public string Foto { get; set; }
        public double? Huurprijs { get; set; }
        public int MakelaarId { get; set; }
        public string MakelaarNaam { get; set; }
        public string ProjectNaam { get; set; }
        public bool HasTuin { get; set; }
        public string MigrationVersion { get; set; }


    }
}
