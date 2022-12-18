namespace TaskItApi.Entities
{
	public class ColorBuilder
	{
		private int _iD = 1;
		private string _name = "Pink";
		private string _value = "5c6bc0";

		public ColorBuilder() { }

		public ColorBuilder WithID(int iD)
		{
			_iD = iD;
			return this;
		}

		public ColorBuilder WithName(string name)
		{
			_name = name;
			return this;
		}

		public ColorBuilder WithValue(string value)
		{
			_value = value;
			return this;
		}

		public Color Build()
		{
			return new Color
			{
				ID = _iD,
				Name = _name,
				Value = _value
			};
		}
	}
}
