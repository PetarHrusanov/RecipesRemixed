namespace RecipesRemixed.Forum.Messages
{
    using System.Threading.Tasks;
    using MassTransit;
    using RecipesRemixed.Forum.Services.ForumUser;
    using RecipesRemixed.Messages.ForumUser;

    public class ForumUserCreatedConsumer : IConsumer<ForumUserCreatedMessage>
    {
        private readonly IForumUserService forumUser;

        public ForumUserCreatedConsumer(IForumUserService forumUser) 
            => this.forumUser = forumUser;

        public async Task Consume(ConsumeContext<ForumUserCreatedMessage> context) 
            => await this.forumUser.CreateByMessage(context.Message.UserId, context.Message.UserName);
    }
}
