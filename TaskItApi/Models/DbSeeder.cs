using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskItApi.Attributes;
using TaskItApi.Entities;
using TaskItApi.Enums;
using TaskItApi.Extentions;

namespace TaskItApi.Models
{
    public class DbSeeder
    {
        /// <summary>
        /// Seed group base colors
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void SeedColors(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Color>().HasData(new Color {ID = 1, Name = "Pink", Value = "#ec407a"});
            modelBuilder.Entity<Color>().HasData(new Color {ID = 2, Name = "Orange", Value = "#ef5350" });
            modelBuilder.Entity<Color>().HasData(new Color {ID = 3, Name = "Purple", Value = "#ab47bc" });
            modelBuilder.Entity<Color>().HasData(new Color {ID = 4, Name = "Blue", Value = "#5c6bc0" });
        }

        /// <summary>
        /// Seed group base icons
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void SeedIcons(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Icon>().HasData(new Icon {ID = 1, Name = "House", Value = "house" });
            modelBuilder.Entity<Icon>().HasData(new Icon {ID = 2, Name = "Work", Value = "work" });
            modelBuilder.Entity<Icon>().HasData(new Icon {ID = 3, Name = "Sport", Value = "directions_run" });
            modelBuilder.Entity<Icon>().HasData(new Icon {ID = 4, Name = "Education", Value = "school" });
            modelBuilder.Entity<Icon>().HasData(new Icon {ID = 5, Name = "Game", Value = "headset_mic" });
            modelBuilder.Entity<Icon>().HasData(new Icon {ID = 6, Name = "Music", Value = "music_note" });
            modelBuilder.Entity<Icon>().HasData(new Icon {ID = 7, Name = "Nature", Value = "nature_people" });
            modelBuilder.Entity<Icon>().HasData(new Icon {ID = 8, Name = "Voluntary work", Value = "loyalty" });
            modelBuilder.Entity<Icon>().HasData(new Icon {ID = 9, Name = "Animal", Value = "pets" });
            modelBuilder.Entity<Icon>().HasData(new Icon {ID = 10, Name = "Art", Value = "color_lens" });
        }

        /// <summary>
        /// Seed task status options
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void SeedTaskStatuses(ModelBuilder modelBuilder)
        {
            IList<TaskStatuses> taskStatusesList = Enum.GetValues(typeof(TaskStatuses)).Cast<TaskStatuses>().ToList();

            for (int i = 0; i < taskStatusesList.Count(); i++)
            {
                StringValueAttribute stringValueAttribute = taskStatusesList[i].GetStringValueAttribute();                
                modelBuilder.Entity<TaskStatus>().HasData(new TaskStatus { ID = i + 1, Status = stringValueAttribute.Value });
            };            
        }
    }
}
