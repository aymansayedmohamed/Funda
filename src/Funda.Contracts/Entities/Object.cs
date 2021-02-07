using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Funda.Data.Entities
{
    public class Object
    {
        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
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
