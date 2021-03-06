﻿using System;
using System.IO;
using System.ComponentModel.DataAnnotations;

namespace AuthSystem.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public string LatinName { get; set; }
        [Required]
        [StringLength(300)]
        public string Description { get; set; }
        public string Kind { get; set; }
        public string Type { get; set; }
        public string Water { get; set; }
        public string Light { get; set; }
        [DataType(DataType.Date)]
        public DateTime ProductDate { get; set; }
        [Required]
        public string Trade { get; set; }
        public byte[] Picture { get; set; }
        public byte[] PictureTwo { get; set; }
        public byte[] PictureThree { get; set; }
        public string Delivery { get; set; }
        public string Soil { get; set; }

        public string UserId { get; set; }
        public string PublisherName { get; set; }
        

        public Product()
        {
            ProductDate = DateTime.UtcNow;
            //Picture = File.ReadAllText(Directory.GetCurrentDirectory() + "/varbinoutput.txt");
        }

        //public byte[] getImage()
        //{
        //    string varbin = Picture;
        //    byte[] output = new byte[varbin.Length / 3];
        //    for (int i = 0; i < output.Length; i++)
        //    {
        //        int input = Convert.ToInt32(Convert.ToString(varbin[0]) + Convert.ToString(varbin[1]) + Convert.ToString(varbin[2]));
        //        output[i] = (byte)input;
        //        varbin = varbin.Remove(0, 3);
        //    }
        //    return output;
        //}
    }
}
