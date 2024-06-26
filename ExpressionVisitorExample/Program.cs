using Bogus;
using ExpressionVisitorExample;
using ExpressionVisitorExample.Models;
using System.Linq.Expressions;

Faker<Employee> people = new Faker<Employee>()
    .RuleFor(p => p.Id, f => f.IndexFaker)
    .RuleFor(p => p.Name, f => f.Name.FirstName())
    .RuleFor(p => p.Surname, f => f.Name.LastName())
    .RuleFor(p => p.Age, f => f.Random.Number(18, 65));

IQueryable<Employee> generatedEmployees = people.Generate(1000)
                                                .AsQueryable()
                                                .Where(p => p.Name.StartsWith('F'))
                                                .OrderBy(p => p.Surname)
                                                .Take(50);
 
Expression queryExpression = generatedEmployees.Expression;

new ExpressionFormatter().Visit(queryExpression);

Console.WriteLine(new string('-', 80));

generatedEmployees.ToList().ForEach(Console.WriteLine);

Console.WriteLine("\nDone!");