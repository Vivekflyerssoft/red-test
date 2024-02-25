using System.ComponentModel.DataAnnotations.Schema;

namespace RedTest.Shared.Entities
{
    public class TopUp
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int BeneficiaryId { get; set; }
        public Beneficiary Beneficiary { get; set; }
        public uint Amount { get; set; }
        public required DateTime DateCreated { get; set; }
    }
}
