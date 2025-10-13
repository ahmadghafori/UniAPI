namespace UniAPI
{
    public class UniFile
    {
        public required Stream File { get; set; }
        public required string FileName { get; set; }


        public StreamContent GetStreamContent
        {
            get
            {
                if (File is null)
                    return null;

                return new StreamContent(File);
            }
        }
    }
}
