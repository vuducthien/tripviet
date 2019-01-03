using System.ComponentModel;

namespace TripViet.Commons
{
    public enum BlogType
    {
        Story = 1,
        Schedule = 2
    }

    public enum UserType
    {
        [Description("Member")]
        Member = 1,
        [Description("Administrator")]
        Administrator = 2
    }
}
