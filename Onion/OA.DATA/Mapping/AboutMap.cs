using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OA.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.DATA.Mapping
{
  public  class AboutMap
    {
        public AboutMap(EntityTypeBuilder<About> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(t => t.Id);
        }
    }
}
