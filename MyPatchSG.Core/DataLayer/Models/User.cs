using SQLite;
using MyPatchSG.BL.Contracts;

namespace MyPatchSG.DL.Models
{
    [Table("User")]
    public class User : IEntity
    {
        [PrimaryKey]
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}

