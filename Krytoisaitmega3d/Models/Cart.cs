namespace Krytoisaitmega3d.Models
{
    public class Cart
    {
        public Cart()
        {
            CartLines = new List<Gun>();
        }
        public List<Gun> CartLines { get; set; }

        public decimal FinalPrice
        {
            get
            {
                decimal sum = 0;
                foreach (Gun gun in CartLines)
                {
                    sum += gun.Price;
                }
                return sum;
            }

        }       
    }
}
