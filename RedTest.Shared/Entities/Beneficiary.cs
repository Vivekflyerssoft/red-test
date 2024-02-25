using System.ComponentModel.DataAnnotations.Schema;

namespace RedTest.Shared.Entities
{
    public class Beneficiary
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required string NickName { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<TopUp>? TopUps { get; set; }
    }
}
