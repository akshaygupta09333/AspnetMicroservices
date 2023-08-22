namespace Basket.API.Mapper
{
    public class BasketProfile
    {
		public BasketProfile()
		{
			CreateMap<BasketCheckout, BasketCheckoutEvent>().ReverseMap();
		}
    }
}