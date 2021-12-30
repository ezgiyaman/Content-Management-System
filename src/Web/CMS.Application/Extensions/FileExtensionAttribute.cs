using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;

namespace CMS.Application.Extensions
{
    public class FileExtensionAttribute : ValidationAttribute
    {
        //Validation Attribute sınıfından Isvalid fonksiyonunu override ettim.
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            IFormFile file = value as IFormFile; //Value object tipinde olduğu için  onu dönüştürdüm.

            //Kullanıcının girdiği resmin yolunu taşıma ;
 
            if (true)
            {
                var extension = Path.GetExtension(file.FileName);

                string[] extensions = { "jpg", "jpeg" };

                bool result = extensions.Any(x => extension.EndsWith(x));

                if (!result)
                {
                    return new ValidationResult(GetErrorMessage());

                }

            }

            return ValidationResult.Success;
        }

        private string GetErrorMessage()
        {
            return "Allowed extension are jpg and jpeg";
        }

    }
}
