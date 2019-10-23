using Microsoft.EntityFrameworkCore;
using TaskItApi.Entities;

namespace TaskItApi.Models
{
    public class DbSeeder
    {
        public static void SeedColors(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Color>().HasData(new Color {ID = 1, Name = "Pink", Value = "#ec407a"});
            modelBuilder.Entity<Color>().HasData(new Color {ID = 2, Name = "Orange", Value = "#ef5350" });
            modelBuilder.Entity<Color>().HasData(new Color {ID = 3, Name = "Purple", Value = "#ab47bc" });
            modelBuilder.Entity<Color>().HasData(new Color {ID = 4, Name = "Blue", Value = "#5c6bc0" });
        }

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
    }
}
