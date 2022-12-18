
namespace TaskItApi.Entities
{
	public class IconBuilder
	{
		private int _iD;
		private string _name = "Natuur";
		private string _value = "nature_people";

		public IconBuilder() { }

		public IconBuilder WithID(int iD)
		{
			_iD = iD;
			return this;
		}

		public IconBuilder WithName(string name)
		{
			_name = name;
			return this;
		}

		public IconBuilder WithValue(string value)
		{
			_value = value;
			return this;
		}

		public Icon Build()
		{
			return new Icon
			{
				ID = _iD,
				Name = _name,
				Value = _value
			};
		}
	}
}
