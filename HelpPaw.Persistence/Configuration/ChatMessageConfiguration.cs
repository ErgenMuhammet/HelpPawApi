using HelpPawApi.Domain.Entities.Chat;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpPaw.Persistence.Configuration
{
    public class ChatMessageConfiguration : IEntityTypeConfiguration<ChatMessage>
    {
        public void Configure(EntityTypeBuilder<ChatMessage> builder)
        {
            builder.ToTable("ChatMessages");

            builder.HasOne(x => x.Sender).
                WithMany(y => y.SendMessages).
                  HasForeignKey(z => z.SenderId).
                      OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.Receiver).
                WithMany(u => u.ReceivedMessages).
                   HasForeignKey(c => c.ReceiverId).
                     OnDelete(DeleteBehavior.Restrict);
            
           
        }
    }
}
