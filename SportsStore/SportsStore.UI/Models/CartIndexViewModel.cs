namespace SportsStore.UI.Models
{
    using SportsStore.Domain.Entites;

    public class CartIndexViewModel
    {
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }
    }
}