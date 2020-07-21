using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OA.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.DATA.Mapping
{
    public class PinfoMap
    {
        public PinfoMap(EntityTypeBuilder<Pinfo> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(t => t.Id);
        }
    }
}
