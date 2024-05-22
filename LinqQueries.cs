using System.ComponentModel.Design;

public class LinqQueries
{
    private List<Book> librosCollection = new List<Book>();
    public LinqQueries()
    {
        using(StreamReader reader = new StreamReader("books.json"))
        {
            string json  = reader.ReadToEnd();
            this.librosCollection = System.Text.Json.JsonSerializer.Deserialize<List<Book>>(json, new System.Text.Json.JsonSerializerOptions() {PropertyNameCaseInsensitive = true});
        }
    }

    public IEnumerable<Book> TodaLaColeccion()
    {
        return librosCollection;
    }

    //Reto operador WHERE 1
    //Utilizando el operador WHERE retorna los libros que fueron publicados después del año 2000
    public IEnumerable<Book> RetoWhere1()
    {
        //extension method
        //return librosCollection.Where(p=> p.PublishedDate.Year > 2000);

        //query expresion
        return from l in librosCollection where l.PublishedDate.Year > 2000 select l;
    }

    //Reto operador WHERE 2
    //Utilizando el operador WHERE retorna los libros que tengan más de 250 páginas y el título contenga las palabras "in Action".
    public IEnumerable<Book> RetoWhere2()
    {
        //extension method
        //return librosCollection.Where(p=> p.PageCount>250 && p.Title.Contains("in Action"));

        //query expresion
        return from l in librosCollection where l.PageCount>250 && l.Title.Contains("in Action") select l;
    }

    //Reto operador All
    //Utilizando el operador All verifica que todos los elementos de la colleccion tenga un valor en el campo Status
    public bool RetoAll()
    {
        return librosCollection.All(p=> p.Status != string.Empty);
    }

    //Reto operador Any
    //Utilizando el operador Any verifica si alguno de los libros fue publicado en 2005
    public bool RetoAny()
    {
        return librosCollection.Any(p=> p.PublishedDate.Year == 2005);
    }

    //Reto operador Contains
    //Utilizando el operador Contains retorna los elementos que pertenezcan a la categoria de Python
    public IEnumerable<Book> RetoContains()
    {
        //query method
        return librosCollection.Where(p=> p.Categories.Contains("Python"));

        //query expresion
        //return from l in librosCollection where l.Categories.Contains("Python") select l;
    }

    //Reto operador OrderBy
    //Utilizando el operador OrderBy retorna todos los libros que sean de la categoria Java ordenados por nombre
    public IEnumerable<Book> RetoOrderBy()
    {
        //query method
        return librosCollection.Where(p=> p.Categories.Contains("Java")).OrderBy(p=> p.Title);

        //query expresion
        //return from l in librosCollection where l.Categories.Contains("Java") orderby l.Title select l;
    }

    //Reto operador OrderByDescending
    //Utilizando el operador OrderByDescending retorna los libros que tengan más de 450 páginas, ordenados por número de páginas en forma descendente
    public IEnumerable<Book> RetoOrderByDescending()
    {
        //query method
        //return librosCollection.Where(p=> p.PageCount>450).OrderByDescending(p=> p.PageCount);

        //query expresion
        return from l in librosCollection where l.PageCount>450 orderby l.PageCount descending select l;
    }

    //Reto del operador Take
    //Utilizando el operador Take selecciona los primeros 3 libros con fecha de publicacion mas reiente que estén categorizados en Java
    public IEnumerable<Book> RetoTake()
    {
        //query method
        //return librosCollection.Where(p=> p.Categories.Contains("Java")).OrderByDescending(p=> p.PublishedDate).Take(3);

        //query expresion
        return (from l in librosCollection where l.Categories.Contains("Java") orderby l.PublishedDate descending select l).Take(3);
    }

    //Reto operador Skip
    //Utilizando el operador Skip selecciona el tercer y cuarto libro de los que tengan mas de 400 páginas
    public IEnumerable<Book> RetoSkip()
    {
        //query method
        //return librosCollection.Where(p=> p.PageCount>400).Take(4).Skip(2);

        //query expresion
        return (from l in librosCollection where l.PageCount>400 select l).Take(4).Skip(2);
    
    }

    //Reto operador seleccion dinamica
    //Utilizando el operador Select selecciona el título y el número de páginas de los primeros 3 libros de la colección
    public IEnumerable<Book> RetoSeleccionDinamica()
    {
        //query method
        //return librosCollection.Take(3).Select(p=> new Book {Title = p.Title, PageCount = p.PageCount});

        //query expresion
        return (from l in librosCollection select new Book {Title = l.Title, PageCount =  l.PageCount}).Take(3);
    }

    //Reto operador Count
    //Utilizando el operador Count, retorna el número de libros que tengan entre 200 y 500 páginas
    public int RetoCount()
    {
        //query method
        return librosCollection.Count(p=> p.PageCount >= 200 && p.PageCount <= 500);//se puede poner el filtro dentro del count

        //query expresion
        //return (from l in librosCollection where l.PageCount >= 200 && l.PageCount <= 500 select l).Count();
    }

    //Reto operador LongCount
    public long RetoLongCount()
    {
        //query method
        //return librosCollection.Where(p=> p.PageCount >= 200 && p.PageCount <= 500).Count();

        //query expresion
        return (from l in librosCollection where l.PageCount >= 200 && l.PageCount <= 500 select l).Count();
    }

    //Reto operador Min
    //Usando el operador Min, retorna la menor fecha de publicacion de la lista de libros
    public DateTime RetoMin()
    {
        //query method
        //return librosCollection.Min(p=> p.PublishedDate);

        //query expresion
        return (from l in librosCollection select l.PublishedDate).Min();
    }

    //Reto operador Max
    //Utilizando el operador Max, retorna la cantidad de páginas del libro con el mayor numero de paginas en la colección
    public int RetoMax()
    {
        //query method
        //return librosCollection.Max(p=> p.PageCount);

        //query expresion
        return (from l in librosCollection select l.PageCount).Max();
    }

    //Reto operador MinBy
    //Usando el operador MinBy retorna el libro que tenga la menor cantidad de páginas mayor que 0
    public Book RetoMinBy()
    {
        //query method
        //return librosCollection.Where(p=> p.PageCount>0).MinBy(p=> p.PageCount);

        //query expresion
        return (from l in librosCollection where l.PageCount>0 select l).MinBy(p=> p.PageCount);
    }

    //Reto operador MaxBy
    //Usando el operador MaxBy retorna el libro con la fecha de la publicacion mas reciente
    public Book RetoMaxBy()
    {
        //query Method
        return librosCollection.MaxBy(p=> p.PublishedDate);

        //query expresion
        //return (from l in librosCollection select l).MaxBy(p=> p.PublishedDate);
    }

    //Reto operador Sum
    //Usando el operador Sum, retorna la suma de la cantidad de páginas, de todos los libros que tengan entre 0 y 500
    public int RetoSum()
    {
        //query method
        //return librosCollection.Where(p=> p.PageCount >= 0 && p.PageCount <= 500).Sum(p=> p.PageCount);

        //query expresion
        return (from l in librosCollection where l.PageCount >= 0 && l.PageCount <= 500 select l).Sum(p=> p.PageCount);
    }

    //Reto operador Aggregate
    //Usando el operador Aggragate, retorna el título de los libros que tienen fecha de publicacion posterior a 2015
    public string RetoAggregate()
    {
        return librosCollection
        .Where(p=> p.PublishedDate.Year > 2015)
        .Aggregate("", (titulosLibros, next) =>
        {
            if(titulosLibros != string.Empty)
            {
                titulosLibros += " - " + next.Title;
            }
            else
            {
                titulosLibros += next.Title;
            }
            return titulosLibros;
        });
    }

    //Reto operador Average
    //Usando el operador Average retorna el promedio de caracteres que tienen los titulos de la coleccion
    public double RetoAverage()
    {
        //query method
        //return librosCollection.Average(p=> p.Title.Length);

        //query expresion
        return (from l in librosCollection select l).Average(p=> p.Title.Length);
    }

    //Reto operador GroupBy
    //Usando el operador GrouoBy, retorna todos los libros que fueron publicados a partir del 2000 agrupados por año
    public IEnumerable<IGrouping<int, Book>> RetoGroupBy()
    {
        //query method
        return librosCollection.Where(p=> p.PublishedDate.Year >= 2000).GroupBy(p=> p.PublishedDate.Year);

        //query expresion
        //return (from l in librosCollection where l.PublishedDate.Year >= 2000 select l).GroupBy(p=> p.PublishedDate.Year);
    }

    //Reto operado Lookup
    //Usando el operador Lookup retorna un diccionario usando Lookup que permita ocnsultar los libros de acuerdo a la letra con la que inicia el titulo del libro
    public ILookup<char, Book> RetoLookup()
    {
        //query method
        return librosCollection.ToLookup(p=> p.Title[0], p=> p);

        //query expresion
        //return (from l in librosCollection).ToLookup(p=> p.Title[0], p=> p);
    }

    //Reto operador Join
    //Obtener una coleccion que tenga todos los libros con mas de 500 páginas y otra que contenga los libros publicados despues del 2005
    //Utilizando la clausula Join, retorna los libros que esten en ambas colecciones
    public IEnumerable<Book> RetoJoin()
    {
        var librosDespuesDel2005 = librosCollection.Where(p=> p.PublishedDate.Year > 2005);
        var librosMas500Paginas = librosCollection.Where(p=> p.PageCount > 500);

        return librosDespuesDel2005.Join(librosMas500Paginas, p=> p.Title, x=> x.Title, (p,x) => p);
    }
}