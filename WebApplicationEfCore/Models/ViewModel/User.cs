namespace WebApplicationEfCore.Models.ViewModel
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DepartmentId { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
