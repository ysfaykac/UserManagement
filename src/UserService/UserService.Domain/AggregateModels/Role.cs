using UserService.Domain.Abstract;

namespace UserService.Domain.AggregateModels;

public class Role:BaseEntity,IAggregateRoot
{
    public Role():base()
    {
        
    }
    public string Name { get; set; }

    public string Description { get; set; }

    public Role(string name,string description):this()
    {
        Name = name;
        Description = description;
    }

}