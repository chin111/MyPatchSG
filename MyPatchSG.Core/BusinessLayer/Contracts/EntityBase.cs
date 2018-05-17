using SQLite;

namespace MyPatchSG.BL.Contracts
{
    public abstract class EntityBase : IEntity
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
    }
}
