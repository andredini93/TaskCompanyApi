using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class Company
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Name { get; set; }

        public string Exchange { get; set; }

        public string Ticker { get; set; }

        public string ISIN { get; set; }

        public string Website { get; set; }
    }
}