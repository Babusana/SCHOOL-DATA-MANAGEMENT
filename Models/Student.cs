using System.ComponentModel.DataAnnotations;

namespace APP.Models;

public partial class Student
{
	public enum Sex
	{
		Male = 1, Female = 2, Other = 3
	}
	public enum Grade
	{
		I = 1,
		II = 2,
		III = 3,
		IV = 4,
		V = 5,
		VI = 6,
		VII = 7,
		VIII = 8,
		IX = 9,
		X = 10
	}
	[Key]
	public int Id { get; set; }
	[Required]
	public string Name { get; set; } = null!;
	[Required]
	public string FatherName { get; set; } = null!;
	[Required]
	public Sex? Gender { get; set; }
	[Required]
	public int Age { get; set; }
	[Required]
	public Grade? Class { get; set; }
}
