using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OA.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.DATA.Mapping
{
    public class TransactionMap
    {
        public TransactionMap(EntityTypeBuilder<Transactions> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(t => t.Id);
        }
    }
}
