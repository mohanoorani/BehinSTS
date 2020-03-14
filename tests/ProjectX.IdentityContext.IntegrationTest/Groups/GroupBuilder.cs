using Faker;
using ProjectX.IdentityContext.Application.Dtos.Group;

namespace ProjectX.IdentityContext.IntegrationTest.Groups
{
    public class GroupBuilder
    {
        private readonly string description;
        
        private string name;

        public GroupBuilder()
        {
            name = Name.First();
            description = Lorem.Sentence();
        }

        public CreateGroupDto BuildCreateGroupDto()
        {
            return new CreateGroupDto {Name = name, Description = description};
        }

        public UpdateGroupDto BuildUpdateGroupDto(string currentName)
        {
            return new UpdateGroupDto {Name = currentName, NewName = name, Description = description};
        }

        public CloneGroupDto BuildCloneGroupDto(string cloneName)
        {
            return new CloneGroupDto {Name = name, CloneName = cloneName};
        }

        public GroupBuilder WithName(string name)
        {
            this.name = name;

            return this;
        }
    }
}