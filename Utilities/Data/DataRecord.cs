using System.Collections.Generic;

namespace Utilities
{
    public class DataRecord 
    {
        public ICollection<DataField> Fields { get; private set; } = new List<DataField>();

        public DataRecord(object record)
        {
            var typeAccessor = record.GetTypeAccessor();

            var dataRecords = new List<DataRecord>();

            foreach (var propertyAccessor in typeAccessor.PropertyAccessors)
            {
                var dataField = new DataField
                {
                    Name = propertyAccessor.Key,
                    Value = propertyAccessor.Value.GetValue(record)
                };

                AddField(dataField);
            }
        }

        public void AddField(DataField field)
        {
            Fields.Add(field);
        }
    }
}