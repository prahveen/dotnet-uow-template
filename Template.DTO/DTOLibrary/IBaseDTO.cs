using System;

namespace Template.DTO.DTOLibrary
{
    public class IBaseDTO
    {
        int ID { get; set; }
        string InsertedUser { get; set; }
        DateTime InsertedTime { get; set; }
        string UpdatedUser { get; set; }
        DateTime? UpdatedTime { get; set; }
        string IPAddress { get; set; }
        Boolean DisUse { get; set; }
        byte[] RowVersion { get; set; }
    }
}
