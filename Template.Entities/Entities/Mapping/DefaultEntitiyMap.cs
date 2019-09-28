using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Entities.Entities.Mapping
{
   public class DefaultEntitiyMap
    {
        public DefaultEntitiyMap(EntityTypeBuilder<DefaultEntitiy> entityBuilder)
        {
            entityBuilder.HasKey(a => a.ID);
            entityBuilder.Property(a => a.Name).IsRequired();
            entityBuilder.Property(a => a.OldStatus);
            entityBuilder.Property(a => a.NewStatus);
            entityBuilder.Property(a => a.Action);
        }
    }
}
