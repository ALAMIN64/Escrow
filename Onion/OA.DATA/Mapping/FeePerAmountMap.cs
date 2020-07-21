using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OA.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.DATA.Mapping
{
    public class FeePerAmountMap
    {
        public FeePerAmountMap(EntityTypeBuilder<FeePerAmount> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(t => t.Id);
        }
    }
}
