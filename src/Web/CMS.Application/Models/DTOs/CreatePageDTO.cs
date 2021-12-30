using CMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Application.Models.DTOs
{
    public class CreatePageDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Slug => Title.ToLower().Replace(" ", "_"); 
        public DateTime CreateDate => DateTime.Now;
        public Status Status => Status.Active;
    }
}
