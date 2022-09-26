using System.Linq;

namespace Api.ViewModels
{
    public class CompanyViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Exchange { get; set; }

        public string Ticker { get; set; }

        public string ISIN { get; set; }

        public string Website { get; set; }

        public bool CheckISIN()
        {
            if (string.IsNullOrEmpty(this.ISIN) || this.ISIN.Count() < 2)
                return false;

            if (!char.IsLetter(this.ISIN[0]) || !char.IsLetter(this.ISIN[1]))
                return false;

            return true;

        }

        public bool CheckObrigatoryFields()
        {
            if (string.IsNullOrEmpty(this.Name) || string.IsNullOrEmpty(this.Exchange) || string.IsNullOrEmpty(this.Ticker))
                return false;
            return true;
        }
    }
}
