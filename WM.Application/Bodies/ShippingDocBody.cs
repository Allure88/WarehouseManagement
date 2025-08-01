namespace WM.Application.Bodies
{
    public class ShippingDocBody
    {
        public string Number { get; set; }
        public ClientBody Client { get; set; }
        public DateTime Date { get; set; }
        public StateBody State {  get; set; }
    }
}
