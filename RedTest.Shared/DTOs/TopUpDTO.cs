using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedTest.Shared.DTOs
{
    public class TopUpDTO
    {
        public int Id { get; set; }
        public int BeneficiaryId { get; set; }
        public uint Amount { get; set; }
    }
}
