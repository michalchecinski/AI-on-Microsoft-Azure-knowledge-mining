namespace TechnicalDocuIndexer.Functions.Model
{
    class OutputRecord
    {
        public string RecordId { get; set; }
        public OutputRecordContainer Data { get; set; }

        public class OutputRecordContainer
        {
            public OutputRecordData Entity { get; set; }
            public OutputRecordContainer(OutputRecordData Entity)
            {
                this.Entity = Entity;
            }
        }

        public class OutputRecordData
        {
            public WebPage WebPages { get; set; } = null;
            public Video Videos { get; set; } = null;
        }

    }
}
