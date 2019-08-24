using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Template.Entities.Entities
{
    public class BaseEntity : IBaseEntity
    {
        public int ID { get; set; }
        public DateTime InsertedTime { get; set; }
        public string InsertedUser { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public string UpdatedUser { get; set; }
        public string IPAddress { get; set; }
        public bool DisUse { get; set; }
        [Timestamp]
        [ConcurrencyCheck]
        public byte[] RowVersion { get; set; }
    }
}
