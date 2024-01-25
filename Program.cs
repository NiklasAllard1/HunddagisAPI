var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

List<Dog> dogs = new List<Dog>
{
    new Dog { Id = 1, ras = "Labrador", namn = "Kalle" },
    new Dog { Id = 2, ras = "Schäfer", namn = "Pillan" },
    new Dog { Id = 3, ras = "Pudel", namn = "Molly" },
    new Dog { Id = 4, ras = "Golden Retriever", namn = "Doris" },
    new Dog { Id = 5, ras = "Cavapoo", namn = "Sigge" },
    new Dog { Id = 6, ras = "Mops", namn = "Beethoven" },
    new Dog { Id = 7, ras = "Maltipoo", namn = "Briney Ears" },
    new Dog { Id = 8, ras = "Schnauzer", namn = "Fur-Dinand" },
    new Dog { Id = 9, ras = "Fransk bulldog", namn = "Chewbacca" }
};

app.MapGet("/", () => "Välkommen till hunddagiset");

app.MapGet("/getalldogs", () =>
{
    var dogInfo = dogs.Select(d => $"ID: {d.Id}, Hundras: {d.ras} Namn: {d.namn}");
    return "De hundarna som är incheckade för tillfället är:\n" + string.Join("\n", dogInfo);
});


app.MapGet("/getdog/{dogType}", (string dogType) =>
{
    Dog dog = null;

    if (int.TryParse(dogType, out int id))
    {
        dog = dogs.FirstOrDefault(d => d.Id == id);
    }

    if (dog == null)
    {
        dog = dogs.FirstOrDefault(d => d.ras.Equals(dogType, StringComparison.OrdinalIgnoreCase) ||
                                         d.namn.Equals(dogType, StringComparison.OrdinalIgnoreCase));
    }

    if (dog != null)
        return $"Hunden {dog.namn} har ID {dog.Id} och är en {dog.ras}";
    else
        return "Hund ej hittad";
});


app.MapGet("/adddog", (string dog) =>
{
    dogs.Add(new Dog { Id = dogs.Count + 1, ras = dog });
    return $"Hund tillagd: {dog}";
});

app.MapGet("/deletedog/{dogInfo}", (string dogInfo) =>
{
    Dog foundDog = null;

   
    if (int.TryParse(dogInfo, out int id))
    {
        foundDog = dogs.FirstOrDefault(d => d.Id == id);
    }

    
    if (foundDog == null)
    {
        foundDog = dogs.FirstOrDefault(d => d.ras.Equals(dogInfo, StringComparison.OrdinalIgnoreCase) ||
                                            d.namn.Equals(dogInfo, StringComparison.OrdinalIgnoreCase));
    }

  
    if (foundDog != null)
    {
        dogs.Remove(foundDog);
        return $"Hunden {foundDog.namn} är borttagen";
    }
    else
    {
        return $"Hunden med informationen '{dogInfo}' ej hittad";
    }
});


app.Run();

public class Dog
{
    public int Id { get; set; }
    public string ras { get; set; }
    public string namn { get; set; }
}
