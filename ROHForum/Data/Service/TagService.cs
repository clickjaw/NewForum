using ROHForum.Data.Models;

namespace ROHForum.Data.Service
{
    public interface ITagService
    {
        public List<PostsModel> GetTagPosts(string tagName);
        void AddNewTag(TagModel tagModel);
        List<TagModel> GetAllTagPosts();
        List<PostsModel> GetTagsByUser(int userId);
        List<TagModel> GetTagsByStartingLetters(string search);
    }

    public class TagService : ITagService
    {
        private readonly DatabaseContext _dbContext;

        public TagService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<PostsModel> GetTagPosts(string tagName)
        {

            return _dbContext.Posts.Where(x => x.Tags == tagName).ToList();        
        }

        public List<TagModel> GetAllTagPosts()
        {

            return _dbContext.Tags.ToList();
        }

        public List<PostsModel> GetTagsByUser(int userId)
        {
            return _dbContext.Posts.Where(x=>x.UserId == userId).ToList();
        }

        public List<TagModel> GetTagsByStartingLetters(string search)
        {
            return _dbContext.Tags.Where(x=>x.TagName.StartsWith(search)).ToList();
        }

        public void AddNewTag(TagModel tagModel)
        {
            List<TagModel> allTags = GetAllTagPosts(); 
            int count = 0;
            foreach(var i in allTags)
            {
                if(i.TagName.ToLower() == tagModel.TagName.ToLower())
                {
                    count++;
                }
            }
            if (count == 0 || allTags.Count() == 0)
            {
                _dbContext.Tags.Add(tagModel);
                _dbContext.SaveChanges();
            }
        }



    }
}
