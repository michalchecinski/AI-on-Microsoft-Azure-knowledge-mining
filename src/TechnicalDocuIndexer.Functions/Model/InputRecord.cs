namespace TechnicalDocuIndexer.Functions.Model
{
    class InputRecord
    {
        public string RecordId { get; set; }
        public InputRecordData Data { get; set; }

        public class InputRecordData
        {
            public string Name { get; set; }
        }
    }
}
