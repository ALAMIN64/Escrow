using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OA.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.DATA.Mapping
{
  public  class PaymentMap
    {
        public PaymentMap(EntityTypeBuilder<Payment> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(t => t.Id);
        }
    }
}
