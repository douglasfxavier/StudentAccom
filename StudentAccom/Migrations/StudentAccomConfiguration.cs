namespace StudentAccom.Migrations {
    using System.Data.Entity.Migrations;


    internal sealed class StudentAccomConfiguration : DbMigrationsConfiguration<StudentAccom.DAL.StudentAccomContext> {
        public StudentAccomConfiguration() {
            AutomaticMigrationsEnabled = true;
        }
        
    }
}
