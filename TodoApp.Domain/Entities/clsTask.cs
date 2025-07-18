namespace TodoApp.Domain.Entities
{
    // كيان المهمة يمثل مهمة To-Do داخل النظام
    public class clsTask
    {
        public int Id { get; set; }                
        public string Title { get; set; }          
        public string Description { get; set; }    
        public bool IsCompleted { get; set; }      
        public DateTime CreatedAt { get; set; }    
        public DateTime? DueDate { get; set; }
        public int? Priority { get; set; }
        public string Category { get; set; }       
        public string OwnerId { get; set; }        
    }
}
