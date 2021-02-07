namespace Funda.Models
{
    public class MakelaarObject
    {
        public Makelaar Makelaar { get; set; }
        public int ObjectsCount => Objects != null ? Objects.Length : 0;
        public Object[] Objects { get; set; }

    }
}
