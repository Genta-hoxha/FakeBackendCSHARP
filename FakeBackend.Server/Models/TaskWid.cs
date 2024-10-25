namespace FakeBackend.Server.Models
{
    public class TaskWid
    {
        public string Title { get; set; }  
        public string Description { get; set; }
        //public DTag[] Tags { get; set; }  
        //public TagWid[] Tags { get; set; }
        public List<TagWid> Tags { get; set; }
        public bool? Completed { get; set; }
        public bool Deleted { get; internal set; }
    }
}
