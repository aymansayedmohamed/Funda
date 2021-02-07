using System.Collections.Generic;

namespace Funda.Infrastructure.Http.Models
{
    public class FundaApiResponse
    {
        //public int AccountStatus { get; set; }
        //public bool EmailNotConfirmed { get; set; }
        //public bool ValidationFailed { get; set; }
        //public string ValidationReport { get; set; }
        //public int Website { get; set; }
        //public Metadata Metadata { get; set; }
        public List<Object> Objects { get; set; }
        //public Paging Paging { get; set; }
        //public int TotaalAantalObjecten { get; set; }
    }

}
