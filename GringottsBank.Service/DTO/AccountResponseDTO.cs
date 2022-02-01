namespace GringottsBank.Service.DTO
{
    public class AccountResponseDTO
    {
        public int AccountID { get; set; }
        public string AccountNickName { get; set; }
        public int AccountType { get; set; }
        public string Nominee { get; set; }
        public double Balance { get; set; }
        public int CustomerID { get; set; }
    }
}
