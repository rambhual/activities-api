using System;

namespace activity_model
{
    public class Activity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? Created_At { get; set; }
    }
}