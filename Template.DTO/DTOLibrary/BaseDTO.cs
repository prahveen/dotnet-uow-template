using System;

namespace Template.DTO.DTOLibrary
{
    public class BaseDTO : IBaseDTO 
    {
        public int ID { get; set; }
        public DateTime InsertedTime { get; set; }
        public string InsertedUser { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public string UpdatedUser { get; set; }
        public string IPAddress { get; set; }
        public bool DisUse { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
