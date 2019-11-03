using System.Collections.Generic;

namespace Utilities
{
    public static class DataRecordExtensions
    {
        public static IEnumerable<DataRecord> ToDataRecords(this IEnumerable<object> records)
        {
            var dataRecords = new List<DataRecord>();

            foreach (var record in records)
            {
                var dataRecord = new DataRecord(record);

                dataRecords.Add(dataRecord);
            }

            return dataRecords;
        }
    }
}
