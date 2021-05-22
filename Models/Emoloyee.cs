using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using lab1.Storage;

 namespace lab1.Models
 {
 public class Employee
 {
     public Guid Id { get; set; } = Guid.Empty;
      public string surname { get; set; }
      public string name { get; set; }
      public int age { get; set; }
      public bool sex { get; set; }
      public string position { get; set; } 
     

     public Employee(int Id, string Surname, string Name, int Age, bool Sex, string Position){
         id=Id;
          surname=Surname;
          name=Name;
          age=Age;
          sex=Sex;
          position=Position;
     }
public BaseModelValidationResult Validate()
        {
            var validationResult = new BaseModelValidationResult();

            if (string.IsNullOrWhiteSpace(name)) validationResult.Append($"Name cannot be empty");
            if (string.IsNullOrWhiteSpace(surname)) validationResult.Append($"Surname cannot be empty");
            if (!(0 < age && age< 100)) validationResult.Append($"GroupIndex  is out of range (0..100)");

            if (!string.IsNullOrEmpty(name) && !char.IsUpper(name.FirstOrDefault())) validationResult.Append($"Name {name} should start from capital letter");
            if (!string.IsNullOrEmpty(surname) && !char.IsUpper(surname.FirstOrDefault())) validationResult.Append($"Surname {surname} should start from capital letter");

            return validationResult;
        }

        public override string ToString()
        {
            return $"{name} {surname} {age} from {position}";
        }
 }
 }
