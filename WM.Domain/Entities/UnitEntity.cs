namespace WM.Domain.Entities
{
    public class UnitEntity : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public State State { get; set; }

        public List<AdmissionResEntity> AdmissionMovements { get; set; } = [];
        public List<ShippingResEntity> ShippingMovements { get; set; } = [];
        public List<BalanceEntity> Balances { get; set; } = [];
    }
}
