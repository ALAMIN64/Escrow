using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OA.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.DATA.Mapping
{
   public class DeliveryTypeMap
    {
        public DeliveryTypeMap(EntityTypeBuilder<DeliveryType> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(t => t.Id);
            entityTypeBuilder.Property(t => t.DeliveryTypeName).IsRequired();
        }
    }
}
