namespace Models;
public class Order
{
	public int Id { get; set; }

	public int MenuId { get; set; }

	public string? MenuName { get; set; }

	public int Price { get; set; }

	public int Count { get; set; }
}