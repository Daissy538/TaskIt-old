using System;
using System.Collections.Generic;
using TaskItApi.Entities;

namespace TaskItApiTest.Builders
{
    public class GroupBuilder
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Test name";
        public string Description { get; set; } = "Test description";
        public int IconID { get; set; } = 1;
        public Icon Icon { get; set; }
        public int ColorID { get; set; } = 1;
        public Color Color { get; set; }
        public ICollection<Subscription> Members { get; set; } = new List<Subscription>();

        public GroupBuilder WithId(int Id)
        {
            this.Id = Id;
            return this;
        }

        public GroupBuilder WithName(string name)
        {
            this.Name = name;
            return this;
        }

        public GroupBuilder WithDescription(string description)
        {
            this.Description = description;
            return this;
        }

        public GroupBuilder WithIconId(int iconId)
        {
            this.IconID = iconId;
            return this;
        }

        public GroupBuilder WithColorId(int colorId)
        {
            this.ColorID = colorId;
            return this;
        }

        public GroupBuilder AddMember(Subscription member)
        {
            this.Members.Add(member);
            return this;
        }

        public Group Build()
        {
            return new Group()
            {
                ID = Id,
                Name = Name,
                Description = Description,
                IconID = IconID,
                ColorID = ColorID,
                Color = Color,
                Icon = Icon,
                Members = Members
            };
        }
    }
}
