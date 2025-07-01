using Microsoft.AspNetCore.Mvc;

namespace ROHForum.Data.Models
{
    public class PageModel
    {


        public List<PostsModel> allPosts {  get; set; }

        public IEnumerable<PostsModel> Items { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1; // Default to page 1

        public int PageSize { get; set; } = 5; // Number of items per page

        public int TotalPages => (int)Math.Ceiling((double)allPosts.Count / PageSize);

        public bool ShowPrevious => CurrentPage > 1;
        public bool ShowNext => CurrentPage < TotalPages;

        public void OnGet()
        {
            // Calculate which items to display for the current page
            var skip = (CurrentPage - 1) * PageSize;
            Items = allPosts.Skip(skip).Take(PageSize).ToList();
        }
    }
}
