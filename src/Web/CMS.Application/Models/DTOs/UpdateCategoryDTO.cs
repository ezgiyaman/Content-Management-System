using CMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Application.Models.DTOs
{
    public class UpdateCategoryDTO
    {
        public int Id { get; set; }

        //sadece harflere izin verilir diyr kural, düzenli olması için .
        [RegularExpression(@"[a-zA-Z]+$", ErrorMessage = " Only letters are allowed")]
        public string Name { get; set; }

        public string Slug => Name.ToLower().Replace(" ", "_");

        public DateTime UpdateDate => DateTime.Now;
        public Status Status => Status.Modified;
    }
}
