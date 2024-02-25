using System.ComponentModel.DataAnnotations.Schema;

namespace RedTest.Shared.Entities
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required string Name { get; set; }
        public ICollection<Beneficiary>? Beneficiaries { get; set; }
    }
}
