using ProjectX.Application.Dtos.Group;

namespace ProjectX.IntegrationTest.Groups
{
    public class GroupChildGroupBuilder
    {
        private string childGroupName;

        private string parentGroupName;

        public GroupChildGroupBuilder()
        {
            parentGroupName = DbMigrationConstants.Group;
            childGroupName = DbMigrationConstants.ChildGroup;
        }

        public GroupChildGroupDto Build()
        {
            return new GroupChildGroupDto { ParentGroupName= parentGroupName, ChildGroupName = childGroupName};
        }

        public GroupChildGroupBuilder WithChildGroupName(string childGroupName)
        {
            this.childGroupName = childGroupName;

            return this;
        }

        public GroupChildGroupBuilder WithParentGroupName(string parentGroupName)
        {
            this.parentGroupName = parentGroupName;

            return this;
        }
    }
}