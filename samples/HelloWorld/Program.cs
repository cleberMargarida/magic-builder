using MagicBuilder;
using System.Text.Json;

//Register at assembly level
[assembly: GenerateBuilder(typeof(Entity))]
[assembly: GenerateBuilder(typeof(Person))]
[assembly: GenerateBuilder(typeof(Contact))]
[assembly: GenerateBuilder(typeof(Address))]

/// Example usage
var person = Builder.Create<Person>()
                    .WithId(10)
                    .WithAge(21)
                    .WithName("name")
                    .WithAddress(
                        address => address
                            .WithStreet("street")
                            .WithCode("XYZ"))
                    .WithMarriedWith(
                        wife => wife
                            .WithName("wifeName")
                            .WithAge(21)
                            .WithAddress(
                                address => address
                                    .WithStreet("street2")
                                    .WithCode("ABC")))
                    .WithContact(
                        contact => contact
                            .WithEmail("foo@bar.com")
                            .WithPhone(55555555))
                    .Build();

Console.WriteLine(JsonSerializer.Serialize(person));


public class Entity
{
    public int Id { get; set; }
}

/// Supports Class
public class Person : Entity
{
    public string Name { get; set; }
    public int Age { get; set; }
    public Address Address { get; set; }
    public Person MarriedWith { get; set; }
    public Contact Contact { get; set; }
}

/// Supports Record
public record Contact(string Email, int Phone);

/// Supports Struct
public struct Address
{
    public string Code { get; set; }
    public string Street { get; set; }
}
