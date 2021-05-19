using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestApp.Models
{
    public class Organization
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Display(Name = "Наименование")]
        public string Name { get; set; }

        [Display(Name = "БИН")]
        public string BIN { get; set; }

        [Display(Name = "Дата основания")]
        public DateTime DateOfFoundation { get; set; }

        [Display(Name = "КРП")]
        public List<string> KRP { get; set; }

        [Display(Name = "Деятельность")]
        public List<string> Activity { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Количество Сотрудников")]
        public int EmployeeCount { get; set; }

        public string PhotoImgUrl { get; set; }
        public string AddressImgUrl { get; set; }
        public string PhoneNumberImgUrl { get; set; }

        public bool HasPhotoImage()
        {
            return !String.IsNullOrWhiteSpace(PhotoImgUrl);
        }
        public bool HasAddressImage()
        {
            return !String.IsNullOrWhiteSpace(AddressImgUrl);
        }
        public bool HasPhoneImage()
        {
            return !String.IsNullOrWhiteSpace(PhoneNumberImgUrl);
        }
    }
}
