using System;

namespace ReflectionSqlLibrary
{
    // Custom attribute to store column info
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnAttribute : Attribute
    {
        public string Name { get; }
        public string Type { get; }

        public ColumnAttribute(string name, string type)
        {
            Name = name;
            Type = type;
        }
    }

    public class Subject
    {
        [Column("Id", "INT")]
        public int Id { get; set; }

        [Column("Description", "NVARCHAR(100)")]
        public string Description { get; set; }

        [Column("Code", "NVARCHAR(50)")]
        public string Code { get; set; }
    }
}
