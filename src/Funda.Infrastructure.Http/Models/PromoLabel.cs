using System.Collections.Generic;

namespace Funda.Infrastructure.Http.Models
{
    public class PromoLabel
    {
        public bool HasPromotionLabel { get; set; }
        public List<string> PromotionPhotos { get; set; }
        public string PromotionPhotosSecure { get; set; }
        public int PromotionType { get; set; }
        public int RibbonColor { get; set; }
        public string RibbonText { get; set; }
        public string Tagline { get; set; }
    }

}
