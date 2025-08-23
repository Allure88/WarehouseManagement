
namespace WM.Domain.Models
{
    public class Unit
    {
        public string Name { get; set; } = string.Empty;
        public State State { get; set; }

        public List<AdmissionRes> AdmissionMovements { get; set; } = [];
        public List<ShippingRes> ShippingMovements { get; set; } = [];
        public List<Balance> Balances { get; set; } = [];
    }
}
